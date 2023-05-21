using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SistemaFinanceiros.DataTransfer.Categorias.Request;
using SistemaFinanceiros.DataTransfer.Categorias.Response;
using SistemaFinanceiros.Dominio.util;

namespace SistemaFinanceiros.Aplicacao.Categorias.Servicos.Interfaces
{
    public interface ICategoriasAppServico
    {
        PaginacaoConsulta<CategoriaResponse> Listar(CategoriaListarRequest request);
        CategoriaResponse Recuperar(int id);
        CategoriaResponse Inserir(CategoriaInserirRequest categoriaInserirRequest);
        CategoriaResponse Editar(int id, CategoriaEditarRequest categoriaEditarRequest);
        void Excluir(int id); 
        void UploadExcel(IFormFile file);
        HttpResponseMessage ExportarCategoriasExcel();
        Stream ListarExcel();

        IList<CategoriaNomeResponse> ListarNomesCategoria();

    }
}