using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaFinanceiros.DataTransfer.Categorias.Request
{
    public class CategoriaListarRequest
    {
        public string Nome { get; set; }
        public string NomeSistema{ get; set; }
    }
}