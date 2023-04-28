using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaFinanceiros.DataTransfer.SistemaFinanceiros.Response
{
    public class SistemaFinanceiroResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Mes { get; set; }
        public int Ano { get; set; }
        public int DiaFechamento { get; set; }
        public bool GerarCopiaDespesa { get; set; }
        public int MesCopia { get; set; }
        public int AnoCopia { get; set; }
    }
}