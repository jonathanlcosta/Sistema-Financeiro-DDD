using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaFinanceiros.Dominio.Despesas.Enumeradores;
using SistemaFinanceiros.Dominio.util.Filtros;
using SistemaFinanceiros.Dominio.util.Filtros.Enumeradores;

namespace SistemaFinanceiros.DataTransfer.Despesas.Request
{
    public class DespesaListarRequest : PaginacaoFiltro
    {
         public string Nome { get; set; }
        public string emailUsuario { get; set; }
        public DespesaListarRequest() : base(cpOrd:"Nome", tpOrd: TipoOrdenacaoEnum.Asc)
        {
            
        }
    }
}