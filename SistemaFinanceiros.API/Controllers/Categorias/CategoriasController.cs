using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SistemaFinanceiros.Aplicacao.Categorias.Servicos.Interfaces;
using SistemaFinanceiros.DataTransfer.Categorias.Request;
using SistemaFinanceiros.DataTransfer.Categorias.Response;
using SistemaFinanceiros.Dominio.util;

namespace SistemaFinanceiros.API.Controllers.Categorias
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriasAppServico categoriasAppServico;

       public CategoriasController(ICategoriasAppServico categoriasAppServico)
       {
        this.categoriasAppServico = categoriasAppServico;
       }

          /// <summary>
    /// Recupera uma categoria por Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<CategoriaResponse> Recuperar(int id)
        {
            var response = categoriasAppServico.Recuperar(id);

            if (response == null)
                return NotFound();

            return Ok(response);
        }

          /// <summary>
    /// Lista as categorias com paginação
    /// </summary>  
    /// <param name="request"></param>
    /// <returns></returns>
       [HttpGet]
        public ActionResult<PaginacaoConsulta<CategoriaResponse>> Listar([FromQuery] CategoriaListarRequest request)
        {    var response = categoriasAppServico.Listar(request);
            return Ok(response);
        }

         /// <summary>
    /// Lista apenas os nomes das categorias
    /// </summary>
    /// <returns></returns>
        [HttpGet("ListarNomesCategorias")]
        public ActionResult<IList<CategoriaNomeResponse>> ListarNomesCategoria()
        {
            var response = categoriasAppServico.ListarNomesCategoria();
            return Ok(response);
        }


            /// <summary>
    /// Adiciona uma nova categoria
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
        [HttpPost]
        public ActionResult<CategoriaResponse> Inserir([FromBody] CategoriaInserirRequest request)
        {
            var retorno = categoriasAppServico.Inserir(request);
            return Ok(retorno);
        }


        /// <summary>
    /// Edita uma categoria por Id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns></returns>
        [HttpPut("{id}")]
        public ActionResult Editar(int id, [FromBody] CategoriaEditarRequest request)
        {

            categoriasAppServico.Editar(id,  request);
            return Ok();
        }


               /// <summary>
    /// Deleta uma categoria por Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public ActionResult Excluir(int id)
        {
            categoriasAppServico.Excluir(id);
            return Ok();
        }


        /// <summary>
    /// Adiciona um arquivo excel dentro do banco
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
        [HttpPost]
        [Route("excel")]
        public ActionResult UploadExcel(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                categoriasAppServico.UploadExcel(file);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

    }
}