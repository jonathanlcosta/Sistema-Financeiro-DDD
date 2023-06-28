using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using NHibernate;
using SistemaFinanceiros.Dominio.util;

namespace SistemaFinanceiros.Infra.Genericos
{
    public class RepositorioDapper<T> where T : class
    {
        private readonly ISession session;
        public RepositorioDapper(ISession session)
        {
            this.session = session;
        }

        public PaginacaoConsulta<T> Listar(string query, DynamicParameters parametros, int pagina, int quantidade)
        {
            var resultado = new PaginacaoConsulta<T>();
            var tasks = new List<Task>
    {
        new Task(() => resultado.Total = session.Connection.Query<int>($"SELECT COUNT(1) FROM ({query}) AS Total", parametros).Single()),
        new Task(() => resultado.Registros = session.Connection.Query<T>(GerarQueryPaginacao(query, pagina, quantidade), parametros).ToList())
    };
            Parallel.ForEach(tasks, q => q.RunSynchronously());
            Task.WaitAll(tasks.ToArray());
            return resultado;

        }

        public string GerarQueryPaginacao(string query, int pg, int qt)
        {
            var offset = (pg - 1) * qt;
            if (pg == 1)
                return string.Format(@"{0} LIMIT {1}", query, qt);
            else
                return string.Format(@"{0} LIMIT {1}, {2}", query, offset, qt);
        }
    }
}