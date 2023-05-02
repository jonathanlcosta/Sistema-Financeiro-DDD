using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IronPdf;
using NHibernate;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using SistemaFinanceiros.Aplicacao.Categorias.Servicos.Interfaces;
using SistemaFinanceiros.Aplicacao.TemplatesTexto.Servicos.Interfaces;
using SistemaFinanceiros.Aplicacao.Transacoes.Interfaces;
using SistemaFinanceiros.DataTransfer.Categorias.Request;
using SistemaFinanceiros.DataTransfer.Categorias.Response;
using SistemaFinanceiros.Dominio.Categorias.Entidades;
using SistemaFinanceiros.Dominio.Categorias.Repositorios;
using SistemaFinanceiros.Dominio.Categorias.Servicos.Interfaces;
using SistemaFinanceiros.Dominio.SistemaFinanceiros.Entidades;
using SistemaFinanceiros.Dominio.SistemaFinanceiros.Servicos.Interfaces;
using SistemaFinanceiros.Dominio.util;
using static IronPdf.PdfPrintOptions;

namespace SistemaFinanceiros.Aplicacao.Categorias.Servicos
{
    public class CategoriasAppServico : ICategoriasAppServico
    {
        private readonly ICategoriasServico categoriasServico;
        private readonly ICategoriasRepositorio categoriasRepositorio;
        private readonly ISistemaFinanceirosServico sistemaFinanceirosServico;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IGeradorTemplateTexto geradorTemplateTexto;
        public CategoriasAppServico(ICategoriasServico categoriasServico, ICategoriasRepositorio categoriasRepositorio,
         ISistemaFinanceirosServico sistemaFinanceirosServico, IMapper mapper, IUnitOfWork unitOfWork, IGeradorTemplateTextoFactory geradorTemplateTextoFactory)
        {
            this.categoriasRepositorio = categoriasRepositorio;
            this.categoriasServico = categoriasServico;
            this.sistemaFinanceirosServico = sistemaFinanceirosServico;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.geradorTemplateTexto = geradorTemplateTextoFactory.Recuperar("Categorias/Templates/categorias-template-pdf.html");
        }
        public CategoriaResponse Editar(int id, CategoriaEditarRequest categoriaEditarRequest)
        {
            try
            {
                unitOfWork.BeginTransaction();
                var categoria = categoriasServico.Editar(id, categoriaEditarRequest.Nome, categoriaEditarRequest.idSistemaFinanceiro);
                unitOfWork.Commit();
                return mapper.Map<CategoriaResponse>(categoria);;
            }
            catch
            {
                unitOfWork.Rollback();
                throw;
            }
        }

        public void Excluir(int id)
        {
            try
            {
                unitOfWork.BeginTransaction();
                var categoria = categoriasServico.Validar(id);
                categoriasRepositorio.Excluir(categoria);
                unitOfWork.Commit();
            }
            catch
            {
               unitOfWork.Rollback();
                throw;
            }
        }

        public CategoriaResponse Inserir(CategoriaInserirRequest categoriaInserirRequest)
        {
            var categoria = categoriasServico.Instanciar(categoriaInserirRequest.Nome, categoriaInserirRequest.idSistemaFinanceiro);
            try
            {
                unitOfWork.BeginTransaction();
                categoria = categoriasServico.Inserir(categoria);
                unitOfWork.Commit();
                return mapper.Map<CategoriaResponse>(categoria);
            }
            catch
            {
                unitOfWork.Rollback();
                throw;
            }
        }

        public PaginacaoConsulta<CategoriaResponse> Listar(int? pagina, int quantidade, CategoriaListarRequest categoriaListarRequest)
        {
          if (pagina.Value <= 0) throw new Exception("Pagina não especificada");
            IQueryable<Categoria> query = categoriasRepositorio.Query();
            if(categoriaListarRequest is null)
            throw new Exception();
            if (!string.IsNullOrEmpty(categoriaListarRequest.Nome))
                query = query.Where(p => p.Nome.Contains(categoriaListarRequest.Nome));
            PaginacaoConsulta<Categoria> categorias = categoriasRepositorio.Listar(query, pagina, quantidade);
            PaginacaoConsulta<CategoriaResponse> response;
            response = mapper.Map<PaginacaoConsulta<CategoriaResponse>>(categorias);
            return response;
        }

        public string ListarHtml()
        {
            IList<Categoria> categorias = categoriasRepositorio.Query().ToList();

            return geradorTemplateTexto.Executar(new { Usuario = "Jos� das Couves", Categorias = categorias });
        }

        public Stream ListarPdf()
        {
            var html = ListarHtml();

            if (string.IsNullOrWhiteSpace(html))
                return null;

            var opcoesImpressao = new PdfPrintOptions()
            {
                MarginTop = 15,
                MarginBottom = 15,
                MarginLeft = 15,
                MarginRight = 15,
                PaperOrientation = PdfPaperOrientation.Portrait
            };

            var pdf = HtmlToPdf.StaticRenderHtmlAsPdf(html, PrintOptions: opcoesImpressao);

            pdf.Stream.Position = 0;
            return pdf.Stream;
        }

        public CategoriaResponse Recuperar(int id)
        {
            var categoria = categoriasServico.Validar(id);
            var response = mapper.Map<CategoriaResponse>(categoria);
            return response;
        }

        public void UploadExcel(Stream arquivo)
        {
             try
            {
                unitOfWork.BeginTransaction();

                var planilha = new XSSFWorkbook(arquivo);
                var folha = planilha.GetSheetAt(0);
                List<Categoria> categorias = new List<Categoria>();

                for (int row = 1; row <= folha.LastRowNum; row++)
                {
                    if (folha.GetRow(row) != null)
                    {
                        IRow linha = folha.GetRow(row);
                        string nome = linha.GetCell(0).StringCellValue;
                        string sistemaFinanceiro = linha.GetCell(1).StringCellValue;
                        SistemaFinanceiro sistema = new SistemaFinanceiro();
                        Categoria categoria = new Categoria(nome, sistema);
                        categorias.Add(categoria);
                    }
                }

                categoriasRepositorio.Inserir(categorias);
                unitOfWork.Commit();
            }
            catch
            {
                unitOfWork.Rollback();
                throw;
            }
        }
    }
}