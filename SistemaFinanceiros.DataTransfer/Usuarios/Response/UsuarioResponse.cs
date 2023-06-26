using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaFinanceiros.DataTransfer.SistemaFinanceiros.Response;

namespace SistemaFinanceiros.DataTransfer.Usuarios.Response
{
    public class UsuarioResponse
    {
        public int Id { get; set; }
        public string CPF { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public bool Administrador { get; set; }
        public bool SistemaAtual { get; set; }
        public SistemaFinanceiroResponse SistemaFinanceiro { get; set; }
    }
}