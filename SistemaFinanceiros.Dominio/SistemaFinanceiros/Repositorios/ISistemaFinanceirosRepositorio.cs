using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaFinanceiros.Dominio.Genericos;
using SistemaFinanceiros.Dominio.SistemaFinanceiros.Entidades;
using SistemaFinanceiros.Dominio.SistemaFinanceiros.Repositorios.Filtros;

namespace SistemaFinanceiros.Dominio.SistemaFinanceiros.Repositorios
{
    public interface ISistemaFinanceirosRepositorio : IGenericoRepositorio<SistemaFinanceiro>
    {
        IQueryable<SistemaFinanceiro> Filtrar(SistemaFinanceiroListarFiltro filtro);
    }
}