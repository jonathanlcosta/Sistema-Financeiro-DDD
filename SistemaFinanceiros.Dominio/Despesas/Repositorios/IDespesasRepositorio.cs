using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaFinanceiros.Dominio.Despesas.Entidades;
using SistemaFinanceiros.Dominio.Despesas.Repositorios.Filtros;
using SistemaFinanceiros.Dominio.Genericos;

namespace SistemaFinanceiros.Dominio.Despesas.Repositorios
{
    public interface IDespesasRepositorio : IGenericoRepositorio<Despesa>
    {
        IList<Despesa> ListarDespesasUsuarioNaoPagasMesesAnterior(string email);
        IList<Despesa> ListarDespesasUsuario(string email);
        IQueryable<Despesa> Filtrar(DespesaListarFiltro filtro);
         IQueryable<Despesa> FiltrarDespesasAtrasadas(DespesaListarFiltro filtro);


    }
}