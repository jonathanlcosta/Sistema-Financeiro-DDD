using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaFinanceiros.Dominio.Despesas.Enumeradores;

namespace SistemaFinanceiros.DataTransfer.Despesas.Request
{
    public class DespesaInserirRequest
    {
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public int Mes { get; set; }
        public int Ano { get; set; }
        public EnumTipoDespesa TipoDespesa { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataAlteracao { get; set; }
        public DateTime DataPagamento { get; set; }
        public DateTime DataVencimento { get; set; }
        public bool Pago { get; set; }
        public bool DespesaAtrasada { get; set; }
        public int idCategoria { get; set; }
    }
}