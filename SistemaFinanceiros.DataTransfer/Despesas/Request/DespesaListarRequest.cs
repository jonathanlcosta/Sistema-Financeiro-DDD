using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaFinanceiros.Dominio.Despesas.Enumeradores;

namespace SistemaFinanceiros.DataTransfer.Despesas.Request
{
    public class DespesaListarRequest
    {
        public string emailUsuario { get; set; }
    }
}