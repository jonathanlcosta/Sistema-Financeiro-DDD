using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaFinanceiros.Dominio.util.Filtros;
using SistemaFinanceiros.Dominio.util.Filtros.Enumeradores;

namespace SistemaFinanceiros.DataTransfer.SistemaFinanceiros.Request
{
    public class SistemaFinanceiroListarRequest : PaginacaoFiltro
    {
        public string Nome { get; set; }
        public SistemaFinanceiroListarRequest() : base(cpOrd:"Nome", tpOrd: TipoOrdenacaoEnum.Asc)
        {
            
        }
    }
}