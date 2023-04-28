using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SistemaFinanceiros.Aplicacao.Autenticacoes.Servicos.Interfaces;
using SistemaFinanceiros.DataTransfer.Autenticacoes.Request;
using SistemaFinanceiros.DataTransfer.Autenticacoes.Response;

namespace SistemaFinanceiros.API.Controllers.Autenticacoes
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IAutenticacoesAppServico autenticacoesAppServico;

        public AutenticacaoController(IAutenticacoesAppServico autenticacoesAppServico)
        {
            this.autenticacoesAppServico = autenticacoesAppServico;
        }

        [HttpPost("logar")]
        public ActionResult<LoginResponse> Logar([FromBody]LoginRequest loginRequest)
        {
            
            var response = autenticacoesAppServico.Logar(loginRequest);

            return Ok(response);
        }

         [HttpPost("cadastro")]
         public ActionResult<CadastroResponse> Cadastrar([FromBody]CadastroRequest cadastroRequest)
         {
             var response = autenticacoesAppServico.Cadastrar(cadastroRequest);

             return Ok(response);
         }
    }
}