using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaFinanceiros.DataTransfer.Categorias.Request
{
    public class CategoriaInserirRequest
    {
        public string Nome { get; set; }
        public int idSistemaFinanceiro { get; set; }
    }
}