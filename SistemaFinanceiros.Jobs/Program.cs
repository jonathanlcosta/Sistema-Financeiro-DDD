using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Matchers;
using Quartz.Spi;
using SistemaFinanceiros.Aplicacao.Usuarios.Profiles;
using SistemaFinanceiros.Aplicacao.Usuarios.Servicos;
using SistemaFinanceiros.Dominio.Usuarios.Servicos;
using SistemaFinanceiros.Infra.Usuarios;
using SistemaFinanceiros.Infra.Usuarios.Mapeamentos;
using SistemaFinanceiros.Jobs.Despesas;
using SistemaFinanceiros.Jobs.Factorys;
using SistemaFinanceiros.Jobs.Listeners;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("MySql");

builder.Services.AddSingleton<ISessionFactory>(factory =>
{
    return Fluently.Configure()
        .Database(MySQLConfiguration.Standard.ConnectionString(connectionString))
        .Mappings(x => x.FluentMappings.AddFromAssemblyOf<UsuariosMap>())
        .BuildSessionFactory();
});

builder.Services.AddScoped<NHibernate.ISession>(factory =>
{
    var sessionFactory = factory.GetService<ISessionFactory>();
    return sessionFactory.OpenSession();
});

builder.Services.AddScoped<ITransaction>(factory =>
{
    var session = factory.GetService<NHibernate.ISession>();
    return session.BeginTransaction();
});

builder.Services.AddAuthorization();

builder.Services.AddControllers();

builder.Services.AddSingleton<IJobFactory, ScheduledJobFactory>();
builder.Services.AddSingleton<IJobListener, LogsJobListener>();
builder.Services.AddTransient<DespesasJob>();

builder.Services.AddAutoMapper(typeof(UsuariosProfile));

builder.Services.Scan(scan => scan
    .FromAssemblyOf<UsuariosAppServico>()
        .AddClasses()
            .AsImplementedInterfaces()
                .WithScopedLifetime());

builder.Services.Scan(scan => scan
    .FromAssemblyOf<UsuariosServico>()
    .AddClasses()
        .AsImplementedInterfaces()
            .WithScopedLifetime());

builder.Services.Scan(scan => scan
    .FromAssemblyOf<UsuariosRepositorio>()
    .AddClasses()
        .AsImplementedInterfaces()
            .WithScopedLifetime());

var schedulerFactory = new StdSchedulerFactory();
var scheduler = schedulerFactory.GetScheduler().GetAwaiter().GetResult();
scheduler.JobFactory = builder.Services.BuildServiceProvider().GetService<IJobFactory>();

scheduler.ListenerManager.AddJobListener(builder.Services.BuildServiceProvider().GetService<IJobListener>(), GroupMatcher<JobKey>.AnyGroup());

var despesas = JobBuilder.Create<DespesasJob>()
    .WithIdentity("DespesasJob", "Despesas")
    .WithDescription("Relatório de despesas atrasadas")
    .StoreDurably()
    .UsingJobData("ConnectionString", connectionString)
    .Build();

scheduler.ScheduleJob(despesas, TriggerBuilder.Create().WithCronSchedule("0 * * ? * *").Build());

scheduler.Start();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
