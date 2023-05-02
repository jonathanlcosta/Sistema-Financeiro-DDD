using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaFinanceiros.DataTransfer.Categorias.Request;
using SistemaFinanceiros.DataTransfer.Categorias.Response;
using SistemaFinanceiros.Dominio.util;

namespace SistemaFinanceiros.Aplicacao.Categorias.Servicos.Interfaces
{
    public interface ICategoriasAppServico
    {
        PaginacaoConsulta<CategoriaResponse> Listar(int? pagina, int quantidade, CategoriaListarRequest categoriaListarRequest);
        CategoriaResponse Recuperar(int id);
        CategoriaResponse Inserir(CategoriaInserirRequest categoriaInserirRequest);
        CategoriaResponse Editar(int id, CategoriaEditarRequest categoriaEditarRequest);
        void Excluir(int id); 
        string ListarHtml();
        Stream ListarPdf();
        void UploadExcel(Stream arquivo);

    }
}