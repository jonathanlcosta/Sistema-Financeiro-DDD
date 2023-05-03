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

        [HttpGet("{id}")]
        public ActionResult<CategoriaResponse> Recuperar(int id)
        {
            var response = categoriasAppServico.Recuperar(id);

            if (response == null)
                return NotFound();

            return Ok(response);
        }

       [HttpGet]
        public ActionResult<PaginacaoConsulta<CategoriaResponse>> Listar(int pagina, int quantidade, [FromQuery] CategoriaListarRequest categoriaListarRequest)
        {    var response = categoriasAppServico.Listar(pagina, quantidade, categoriaListarRequest);
            return Ok(response);
        }

        [HttpPost]
        public ActionResult<CategoriaResponse> Inserir([FromBody] CategoriaInserirRequest request)
        {
            var retorno = categoriasAppServico.Inserir(request);
            return Ok(retorno);
        }

        [HttpPut("{id}")]
        public ActionResult Editar(int id, [FromBody] CategoriaEditarRequest categoriaEditarRequest)
        {

            categoriasAppServico.Editar(id,  categoriaEditarRequest);
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult Excluir(int id)
        {
            categoriasAppServico.Excluir(id);
            return Ok();
        }

        [HttpGet]
        [Route("ExportarExcel")]
        public FileResult ExportarExcel()
        {
           var response = categoriasAppServico.ExportarCategoriasExcel();
            var fileContent = response.Content.ReadAsByteArrayAsync().Result;
            var contentType = response.Content.Headers.ContentType.MediaType;
            var fileName = response.Content.Headers.ContentDisposition.FileName;

            return File(fileContent, contentType, fileName);
        }

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

        [HttpGet]
        [Route("excel")]
        public ActionResult ListarExcel()
        {
            var planilha = categoriasAppServico.ListarExcel();

            if (planilha != null)
            {
                FileStreamResult result = new FileStreamResult(planilha, "application/octet-stream")
                {
                    FileDownloadName = "Categorias.xlsx"
                };
                return result;
            }
            else
                return NotFound();
        }

    }
}