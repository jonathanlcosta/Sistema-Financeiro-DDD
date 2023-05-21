using System.Net;
using System.Net.Http.Headers;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using SistemaFinanceiros.Aplicacao.Categorias.Servicos.Interfaces;
using SistemaFinanceiros.Aplicacao.Transacoes.Interfaces;
using SistemaFinanceiros.DataTransfer.Categorias.Request;
using SistemaFinanceiros.DataTransfer.Categorias.Response;
using SistemaFinanceiros.Dominio.Categorias.Entidades;
using SistemaFinanceiros.Dominio.Categorias.Repositorios;
using SistemaFinanceiros.Dominio.Categorias.Repositorios.Filtros;
using SistemaFinanceiros.Dominio.Categorias.Servicos.Comandos;
using SistemaFinanceiros.Dominio.Categorias.Servicos.Interfaces;
using SistemaFinanceiros.Dominio.SistemaFinanceiros.Entidades;
using SistemaFinanceiros.Dominio.SistemaFinanceiros.Servicos.Interfaces;
using SistemaFinanceiros.Dominio.util;

namespace SistemaFinanceiros.Aplicacao.Categorias.Servicos
{
    public class CategoriasAppServico : ICategoriasAppServico
    {
        private readonly ICategoriasServico categoriasServico;
        private readonly ICategoriasRepositorio categoriasRepositorio;
        private readonly ISistemaFinanceirosServico sistemaFinanceirosServico;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        public CategoriasAppServico(ICategoriasServico categoriasServico, ICategoriasRepositorio categoriasRepositorio,
         ISistemaFinanceirosServico sistemaFinanceirosServico, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.categoriasRepositorio = categoriasRepositorio;
            this.categoriasServico = categoriasServico;
            this.sistemaFinanceirosServico = sistemaFinanceirosServico;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public CategoriaResponse Editar(int id, CategoriaEditarRequest request)
        {
            CategoriaComando comando = mapper.Map<CategoriaComando>(request);
            try
            {
                unitOfWork.BeginTransaction();
                Categoria categoria = categoriasServico.Editar(id, comando);
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
                Categoria categoria = categoriasServico.Validar(id);
                categoriasRepositorio.Excluir(categoria);
                unitOfWork.Commit();
            }
            catch
            {
               unitOfWork.Rollback();
                throw;
            }
        }

        public CategoriaResponse Inserir(CategoriaInserirRequest request)
        {
            CategoriaComando comando = mapper.Map<CategoriaComando>(request);
            try
            {
                unitOfWork.BeginTransaction();
                Categoria categoria = categoriasServico.Inserir(comando);
                unitOfWork.Commit();
                return mapper.Map<CategoriaResponse>(categoria);
            }
            catch
            {
                unitOfWork.Rollback();
                throw;
            }
        }

        public PaginacaoConsulta<CategoriaResponse> Listar(CategoriaListarRequest request)
        {
            CategoriaListarFiltro filtro = mapper.Map<CategoriaListarFiltro>(request);
            IQueryable<Categoria> query = categoriasRepositorio.Filtrar(filtro);

            PaginacaoConsulta<Categoria> categorias = categoriasRepositorio.Listar(query, request.Qt, request.Pg, request.CpOrd, request.TpOrd);
            PaginacaoConsulta<CategoriaResponse> response;
            response = mapper.Map<PaginacaoConsulta<CategoriaResponse>>(categorias);
            return response;
        }

        public CategoriaResponse Recuperar(int id)
        {
            Categoria categoria = categoriasServico.Validar(id);
            var response = mapper.Map<CategoriaResponse>(categoria);
            return response;
        }

        public void UploadExcel(IFormFile file)
{
    try
    {
        unitOfWork.BeginTransaction();

        var planilha = new XSSFWorkbook(file.OpenReadStream());
        var folha = planilha.GetSheetAt(0);
        List<Categoria> categorias = new List<Categoria>();

        for (int row = 1; row <= folha.LastRowNum; row++)
        {
            if (folha.GetRow(row) != null)
            {
                IRow linha = folha.GetRow(row);
                string nome = linha.GetCell(0).StringCellValue;
                Categoria categoria = new Categoria(nome);
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



        public HttpResponseMessage ExportarCategoriasExcel()
        {
            
                IList<Categoria> categorias = categoriasRepositorio.QueryList().ToList();

                var workbook = new HSSFWorkbook();
                var sheet = workbook.CreateSheet("Categorias");

                var row = sheet.CreateRow(0);
                row.CreateCell(0).SetCellValue("ID");
                row.CreateCell(1).SetCellValue("Nome");
                row.CreateCell(1).SetCellValue("Nome Sistema Financeiro");

                for (int i = 0; i < categorias.Count; i++)
                {
                    var categoria = categorias[i];
                    row = sheet.CreateRow(i + 1);
                    row.CreateCell(0).SetCellValue(categoria.Id);
                    row.CreateCell(1).SetCellValue(categoria.Nome);
                    row.CreateCell(2).SetCellValue(categoria.SistemaFinanceiro.Nome);
                }

                var memoryStream = new MemoryStream();
                workbook.Write(memoryStream);

                var response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new ByteArrayContent(memoryStream.ToArray());
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                response.Content.Headers.ContentDisposition.FileName = "categorias.xlsx";
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.ms-excel");
                return response;
            }
        
        public Stream ListarExcel()
        {
            try
            {
                IList<Categoria> categorias = categoriasRepositorio.Query().ToList();

                IWorkbook planilha = new XSSFWorkbook();
                ISheet folha = planilha.CreateSheet("Categorias");

                IRow cabecalho = folha.CreateRow(0);
                cabecalho.CreateCell(0).SetCellValue("Nome Categoria");
                cabecalho.CreateCell(1).SetCellValue("Nome Sistema Financeiro");
                cabecalho.CreateCell(2).SetCellValue("Dia do fechamento do sistema");

                var cellStyle = planilha.CreateCellStyle();
                cellStyle.BorderLeft = BorderStyle.Medium;
                cellStyle.BorderRight = BorderStyle.Medium;
                cellStyle.BorderTop = BorderStyle.Medium;
                cellStyle.BorderBottom = BorderStyle.Medium;

                foreach (Categoria categoria in categorias)
                {
                    int indiceDeLinha = 1;
                    IRow linha = folha.CreateRow(indiceDeLinha);
                    linha.CreateCell(0).SetCellValue(categoria.Nome);
                    linha.GetCell(0).CellStyle = cellStyle;

                    linha.CreateCell(1).SetCellValue(categoria.SistemaFinanceiro.Nome);
                    linha.GetCell(1).CellStyle = cellStyle;

                    linha.CreateCell(2).SetCellValue(categoria.SistemaFinanceiro.DiaFechamento);
                    linha.GetCell(2).CellStyle = cellStyle;
                }

                var fontStyleCabecalho = planilha.CreateFont();
                fontStyleCabecalho.FontHeightInPoints = 16;
                fontStyleCabecalho.Boldweight = (short)FontBoldWeight.Bold;
                fontStyleCabecalho.Color = HSSFColor.White.Index;

                var cellStyleCabecalho = planilha.CreateCellStyle();
                cellStyleCabecalho.FillForegroundColor = HSSFColor.DarkBlue.Index;
                cellStyleCabecalho.FillPattern = FillPattern.SolidForeground;
                cellStyleCabecalho.SetFont(fontStyleCabecalho);

                for (int coluna = 0; coluna <= cabecalho.LastCellNum - 1; coluna++)
                {
                    cabecalho.GetCell(coluna).CellStyle = cellStyleCabecalho;
                    folha.AutoSizeColumn(coluna);
                }

                var memoryStream = new MemoryStream();
                planilha.Write(memoryStream);
                var bytes = memoryStream.ToArray();
                return new MemoryStream(bytes);
            }
            catch
            {
                unitOfWork.Rollback();

                throw;
            }
        }

        public IList<CategoriaNomeResponse> ListarNomesCategoria()
        {
             IList<Categoria> categorias = categoriasRepositorio.ListarNomesCategoria();
            var response = mapper.Map<IList<CategoriaNomeResponse>>(categorias);
            return response;
        }
    }
    }
