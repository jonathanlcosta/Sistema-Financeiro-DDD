using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaFinanceiros.DataTransfer.Usuarios.Request
{
    public class UsuarioInserirRequest
    {
        public string CPF { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
         public string Senha { get; set; }
        public bool Administrador { get; set; }
        public bool SistemaAtual { get; set; }
        public int idSistemaFinanceiro { get; set; }
    }
}