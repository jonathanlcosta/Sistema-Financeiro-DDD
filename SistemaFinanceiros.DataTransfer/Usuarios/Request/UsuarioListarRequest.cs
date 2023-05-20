using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaFinanceiros.Dominio.util.Filtros;
using SistemaFinanceiros.Dominio.util.Filtros.Enumeradores;

namespace SistemaFinanceiros.DataTransfer.Usuarios.Request
{
    public class UsuarioListarRequest : PaginacaoFiltro
    {
        public string CPF { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public UsuarioListarRequest() : base(cpOrd:"Nome", tpOrd: TipoOrdenacaoEnum.Asc)
        {
            
        }
    }
}