using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaFinanceiros.Dominio.Usuarios.Entidades;

namespace SistemaFinanceiros.Dominio.Autenticacoes.Servicos.Interfaces
{
    public interface IAutenticacoesServico
    {
        Usuario ValidarCadastro(string email, string senha);

        Usuario ValidarLogin(Usuario usuario, String senha);

        String GerarToken(Usuario usuario);
    }
}