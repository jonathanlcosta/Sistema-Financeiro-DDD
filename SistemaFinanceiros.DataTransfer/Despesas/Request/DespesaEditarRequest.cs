using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaFinanceiros.Dominio.Despesas.Enumeradores;

namespace SistemaFinanceiros.DataTransfer.Despesas.Request
{
    public class DespesaEditarRequest
    {
         public string Nome { get; set; }
        public decimal Valor { get; set; }
        public int IdUsuario { get; set; }
        public EnumTipoDespesa TipoDespesa { get; set; }
        public DateTime DataVencimento { get; set; }
        public bool Pago { get; set; }
        public bool DespesaAtrasada { get; set; }
        public int IdCategoria { get; set; }
    }
}