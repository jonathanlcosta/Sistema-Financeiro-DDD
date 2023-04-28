using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SistemaFinanceiros.Aplicacao.Usuarios.Servicos.Interfaces;
using SistemaFinanceiros.DataTransfer.Usuarios.Request;
using SistemaFinanceiros.DataTransfer.Usuarios.Response;
using SistemaFinanceiros.Dominio.util;

namespace SistemaFinanceiros.API.Controllers.Usuarios
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuariosAppServico usuariosAppServico;

       public UsuariosController(IUsuariosAppServico usuariosAppServico)
       {
        this.usuariosAppServico = usuariosAppServico;
       }

        [HttpGet("{id}")]
        public ActionResult<UsuarioResponse> Recuperar(int id)
        {
            var response = usuariosAppServico.Recuperar(id);

            if (response == null)
                return NotFound();

            return Ok(response);
        }

       [HttpGet]
        public ActionResult<PaginacaoConsulta<UsuarioResponse>> Listar(int pagina, int quantidade, [FromQuery] UsuarioListarRequest usuarioListarRequest)
        {    var response = usuariosAppServico.Listar(pagina, quantidade, usuarioListarRequest);
            return Ok(response);
        }


        [HttpPut("{id}")]
        public ActionResult Editar(int id, [FromBody] UsuarioEditarRequest usuarioEditarRequest)
        {

            usuariosAppServico.Editar(id,  usuarioEditarRequest);
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult Excluir(int id)
        {
            usuariosAppServico.Excluir(id);
            return Ok();
        }
    }
}