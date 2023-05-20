using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SistemaFinanceiros.Aplicacao.SistemaFinanceiros.Servicos.Interfaces;
using SistemaFinanceiros.DataTransfer.SistemaFinanceiros.Request;
using SistemaFinanceiros.DataTransfer.SistemaFinanceiros.Response;
using SistemaFinanceiros.Dominio.util;

namespace SistemaFinanceiros.API.Controllers.SistemaFinanceiros
{
    [ApiController]
    [Route("api/[controller]")]
    public class SistemaFinanceirosController : ControllerBase
    {
        private readonly ISistemaFinanceirosAppServico sistemaFinanceirosAppServico;

       public SistemaFinanceirosController(ISistemaFinanceirosAppServico sistemaFinanceirosAppServico)
       {
        this.sistemaFinanceirosAppServico = sistemaFinanceirosAppServico;
       }

        [HttpGet("{id}")]
        public ActionResult<SistemaFinanceiroResponse> Recuperar(int id)
        {
            var response = sistemaFinanceirosAppServico.Recuperar(id);

            if (response == null)
                return NotFound();

            return Ok(response);
        }

       [HttpGet]
        public ActionResult<PaginacaoConsulta<SistemaFinanceiroResponse>> Listar([FromQuery] SistemaFinanceiroListarRequest sistemaFinanceiroListarRequest)
        {    var response = sistemaFinanceirosAppServico.Listar(sistemaFinanceiroListarRequest);
            return Ok(response);
        }

        [HttpPost]
        public ActionResult<SistemaFinanceiroResponse> Inserir([FromBody] SistemaFinanceiroInserirRequest request)
        {
            var retorno = sistemaFinanceirosAppServico.Inserir(request);
            return Ok(retorno);
        }

        [HttpPut("{id}")]
        public ActionResult Editar(int id, [FromBody] SistemaFinanceiroEditarRequest sistemaFinanceiroEditarRequest)
        {

            sistemaFinanceirosAppServico.Editar(id,  sistemaFinanceiroEditarRequest);
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult Excluir(int id)
        {
            sistemaFinanceirosAppServico.Excluir(id);
            return Ok();
        }
    }
}