using Microsoft.AspNetCore.Mvc;
using SistemaFinanceiros.Aplicacao.Despesas.Servicos.Interfaces;
using SistemaFinanceiros.DataTransfer.Despesas.Request;
using SistemaFinanceiros.DataTransfer.Despesas.Response;
using SistemaFinanceiros.Dominio.util;

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

        [HttpGet("despesas/despesasUsuario")]
        public ActionResult<IList<DespesaResponse>> ListarDespesasUsuario(string emailUsuario)
        {
            var response = despesasAppServico.ListarDespesasUsuario(emailUsuario);
            return Ok(response);      
            
        }

       [HttpGet("despesas/usuario-nao-pagas-anterior")]
        public ActionResult<IList<DespesaResponse>> ListarDespesasUsuarioNaoPagasMesesAnterior(string emailUsuario)
        {
            var response = despesasAppServico.ListarDespesasUsuarioNaoPagasMesesAnterior(emailUsuario);
            return Ok(response);      
        }

        [HttpGet("despesas/usuario-nao-pagas-atras")]
        public ActionResult<IList<DespesaResponse>> ListarDespesasUsuarioNaoPagasMesesAtras(string emailUsuario)
        {
            var response = despesasAppServico.ListarDespesasUsuarioNaoPagasMesesAtras(emailUsuario);
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

        [HttpGet("CarregaGraficos")]
        public object CarregaGraficos(string emailUsuario)
        {
            return  despesasAppServico.CarregaGraficos(emailUsuario);
        }

        [HttpPost]
        public ActionResult<DespesaResponse> Inserir([FromBody] DespesaInserirRequest request)
        {
            var retorno = despesasAppServico.Inserir(request);
            return Ok(retorno);
        }

        [HttpGet()]
        public ActionResult<PaginacaoConsulta<DespesaResponse>> ListarDespesas([FromQuery] DespesaListarRequest despesaListarRequest)
        {    var response = despesasAppServico.Listar(despesaListarRequest);
            return Ok(response);
        }

        [HttpGet("ListarDespesasNaoPagasMesesAtras")]
        public ActionResult<PaginacaoConsulta<DespesaResponse>> ListarDespesasNaoPagasMesesAtras(int pagina, int quantidade,[FromQuery] DespesaListarRequest despesaListarRequest)
        {    var response = despesasAppServico.ListarDespesas(pagina, quantidade, despesaListarRequest);
            return Ok(response);
        }
    }
}