using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NHibernate;
using SistemaFinanceiros.Dominio.SistemaFinanceiros.Entidades;
using SistemaFinanceiros.Dominio.SistemaFinanceiros.Repositorios;
using SistemaFinanceiros.Dominio.SistemaFinanceiros.Repositorios.Filtros;
using SistemaFinanceiros.Infra.Genericos;

namespace SistemaFinanceiros.Infra.SistemaFinanceiros
{
    public class SistemaFinanceirosRepositorio : GenericoRepositorio<SistemaFinanceiro>, ISistemaFinanceirosRepositorio
    {
       public SistemaFinanceirosRepositorio(ISession session) : base (session)
       {
        
       }

        public IQueryable<SistemaFinanceiro> Filtrar(SistemaFinanceiroListarFiltro filtro)
        {
            IQueryable<SistemaFinanceiro> query = Query();

            if (!string.IsNullOrWhiteSpace(filtro.Nome))
            {
                 query = query.Where(d => d.Nome.Contains(filtro.Nome));
            }

            return query;
        }
    }
}