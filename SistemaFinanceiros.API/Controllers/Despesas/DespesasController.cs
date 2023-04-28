using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SistemaFinanceiros.Aplicacao.Despesas.Servicos.Interfaces;
using SistemaFinanceiros.DataTransfer.Despesas.Request;
using SistemaFinanceiros.DataTransfer.Despesas.Response;

namespace SistemaFinanceiros.API.Controllers.Despesas
{
    [ApiController]
    [Route("api/[controller]")]
    public class DespesasController : ControllerBase
    {
        private readonly IDespesasAppServico despesasAppServico;
        public DespesasController(IDespesasAppServico despesasAppServico)
        {
            this.despesasAppServico = despesasAppServico;
        }


        [HttpGet("{emailUsuarioUm}")]
        public ActionResult<IList<DespesaResponse>> ListarDespesasUsuario(string emailUsuario)
        {
            var response = despesasAppServico.ListarDespesasUsuario(emailUsuario);
            return Ok(response);      
            
        }

        [HttpGet("{emailUsuario}")]
        public ActionResult<IList<DespesaResponse>> ListarDespesasUsuarioNaoPagasMesesAnterior(string emailUsuario)
        {
            var response = despesasAppServico.ListarDespesasUsuarioNaoPagasMesesAnterior(emailUsuario);
            return Ok(response);      
        }

        [HttpGet("{id}")]
        public ActionResult<DespesaResponse> Recuperar(int id)
        {
            var response = despesasAppServico.Recuperar(id);

            if (response == null)
                return NotFound();

            return Ok(response);
        }

        [HttpPost]
        public ActionResult<DespesaResponse> Inserir([FromBody] DespesaInserirRequest request)
        {
            var retorno = despesasAppServico.Inserir(request);
            return Ok(retorno);
        }


    }
}