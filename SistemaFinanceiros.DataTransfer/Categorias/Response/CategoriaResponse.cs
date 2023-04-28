using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaFinanceiros.DataTransfer.Categorias.Response
{
    public class CategoriaResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int idSistemaFinanceiro { get; set; }
    }
}