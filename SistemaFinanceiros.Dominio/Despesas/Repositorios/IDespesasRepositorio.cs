using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaFinanceiros.Dominio.Despesas.Entidades;
using SistemaFinanceiros.Dominio.Genericos;

namespace SistemaFinanceiros.Dominio.Despesas.Repositorios
{
    public interface IDespesasRepositorio : IGenericoRepositorio<Despesa>
    {
        IList<Despesa> ListarDespesasUsuarioNaoPagasMesesAnterior(string emailUsuario);
    }
}