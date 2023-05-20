using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaFinanceiros.Dominio.util.Filtros;
using SistemaFinanceiros.Dominio.util.Filtros.Enumeradores;

namespace SistemaFinanceiros.DataTransfer.Categorias.Request
{
    public class CategoriaListarRequest : PaginacaoFiltro
    {
        public string Nome { get; set; }
        public string NomeSistema{ get; set; }

         public CategoriaListarRequest() : base(cpOrd:"Nome", tpOrd: TipoOrdenacaoEnum.Asc)
        {
        }

    }
}