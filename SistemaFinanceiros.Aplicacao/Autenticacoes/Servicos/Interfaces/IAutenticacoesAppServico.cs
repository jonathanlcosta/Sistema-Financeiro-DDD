using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaFinanceiros.DataTransfer.Autenticacoes.Request;
using SistemaFinanceiros.DataTransfer.Autenticacoes.Response;

namespace SistemaFinanceiros.Aplicacao.Autenticacoes.Servicos.Interfaces
{
    public interface IAutenticacoesAppServico
    {
        LoginResponse Logar(LoginRequest loginRequest);
        CadastroResponse Cadastrar(CadastroRequest cadastroRequest);
    }
}