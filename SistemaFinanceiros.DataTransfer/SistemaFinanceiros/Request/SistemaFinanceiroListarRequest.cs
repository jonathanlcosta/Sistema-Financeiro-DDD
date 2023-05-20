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
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Mes { get; set; }
        public int Ano { get; set; }
        public int DiaFechamento { get; set; }
        public bool GerarCopiaDespesa { get; set; }
        public int MesCopia { get; set; }
        public int AnoCopia { get; set; }

        public SistemaFinanceiroListarRequest() : base(cpOrd:"Nome", tpOrd: TipoOrdenacaoEnum.Asc)
        {
            
        }
    }
}