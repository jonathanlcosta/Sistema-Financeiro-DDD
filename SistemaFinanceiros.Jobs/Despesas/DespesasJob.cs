using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Quartz;
using Serilog.Context;
using SistemaFinanceiros.Aplicacao.Despesas.Servicos.Interfaces;
using SistemaFinanceiros.DataTransfer.Despesas.Request;

namespace SistemaFinanceiros.Jobs.Despesas
{
    public class DespesasJob : IJob
    {
        
    private readonly ILogger<DespesasJob> logger;
    private readonly IDespesasAppServico despesasAppServico;

        public DespesasJob(ILogger<DespesasJob> logger, IDespesasAppServico despesasAppServico)
        {
            this.logger = logger;
            this.despesasAppServico = despesasAppServico;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            using (LogContext.PushProperty("TransactionId", context.FireInstanceId))
            using (LogContext.PushProperty("Job", context.JobDetail.JobType.FullName))
            {

                try
                {
                    var quantidadeDespesasAtrasadas = despesasAppServico.ListarDespesas(new DespesaListarRequest()).Total;

                    this.logger.LogInformation("Temos {quantidadeDespesasAtrasadas} Despesas atrasadas!", quantidadeDespesasAtrasadas);

                }
                catch (Exception ex)
                {
                    logger.LogError(ex, ex.Message);
                }

                await Task.CompletedTask;
            }
        }
    }
}