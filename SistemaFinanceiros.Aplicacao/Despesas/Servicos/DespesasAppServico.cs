using AutoMapper;
using NHibernate;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using SistemaFinanceiros.Aplicacao.Despesas.Servicos.Interfaces;
using SistemaFinanceiros.Aplicacao.Transacoes.Interfaces;
using SistemaFinanceiros.DataTransfer.Despesas.Request;
using SistemaFinanceiros.DataTransfer.Despesas.Response;
using SistemaFinanceiros.Dominio.Categorias.Servicos.Interfaces;
using SistemaFinanceiros.Dominio.Despesas.Entidades;
using SistemaFinanceiros.Dominio.Despesas.Repositorios;
using SistemaFinanceiros.Dominio.Despesas.Repositorios.Consultas;
using SistemaFinanceiros.Dominio.Despesas.Repositorios.Filtros;
using SistemaFinanceiros.Dominio.Despesas.Servicos.Comandos;
using SistemaFinanceiros.Dominio.Despesas.Servicos.Interfaces;
using SistemaFinanceiros.Dominio.util;

namespace SistemaFinanceiros.Aplicacao.Despesas.Servicos
{
    public class DespesasAppServico : IDespesasAppServico
    {
        private readonly IDespesasRepositorio despesasRepositorio;
        private readonly IDespesasServico despesasServico;
        private readonly ICategoriasServico categoriasServico;
        private readonly IDespesasConsultasRepositorio despesasConsultasRepositorio;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        public DespesasAppServico(IDespesasRepositorio despesasRepositorio, IDespesasServico despesasServico, ICategoriasServico categoriasServico,
        IMapper mapper,  IUnitOfWork unitOfWork, IDespesasConsultasRepositorio despesasConsultasRepositorio)
        {
            this.categoriasServico = categoriasServico;
            this.despesasServico = despesasServico;
            this.despesasRepositorio = despesasRepositorio;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.despesasConsultasRepositorio = despesasConsultasRepositorio;
        }

        public object CarregaGraficos(string email)
        {
            return despesasServico.CarregaGraficos(email);
        }

        public DespesaResponse Editar(int id, DespesaEditarRequest request)
        {
             DespesaComando comando = mapper.Map<DespesaComando>(request);
            try
            {
                unitOfWork.BeginTransaction();
                Despesa despesa = despesasServico.Editar(id, comando);
                unitOfWork.Commit();
                return mapper.Map<DespesaResponse>(despesa);;
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
                Despesa despesa = despesasServico.Validar(id);
                despesasRepositorio.Excluir(despesa);
                unitOfWork.Commit();
            }
            catch
            {
                unitOfWork.Rollback();
                throw;
            }
        }

        public DespesaResponse Inserir(DespesaInserirRequest request)
        {
            DespesaComando comando = mapper.Map<DespesaComando>(request);
             
            try
            {
                unitOfWork.BeginTransaction();
                Despesa despesa = despesasServico.Inserir(comando);
                unitOfWork.Commit();
                return mapper.Map<DespesaResponse>(despesa);
            }
            catch
            {
               unitOfWork.Rollback();
                throw;
            }
        }

        public PaginacaoConsulta<DespesaResponse> Listar(DespesaListarRequest request)
        {
            DespesaListarFiltro filtro = mapper.Map<DespesaListarFiltro>(request);
            IQueryable<Despesa> query = despesasRepositorio.Filtrar(filtro);
            PaginacaoConsulta<Despesa> despesas = despesasRepositorio.Listar(query, request.Qt, request.Pg, request.CpOrd, request.TpOrd);
            PaginacaoConsulta<DespesaResponse> response;
            response = mapper.Map<PaginacaoConsulta<DespesaResponse>>(despesas);
            return response;
        }

        public PaginacaoConsulta<DespesaResponse> ListarDespesas(DespesaListarRequest request)
        {

            DespesaListarFiltro filtro = mapper.Map<DespesaListarFiltro>(request);
            IQueryable<Despesa> query = despesasRepositorio.FiltrarDespesasAtrasadas(filtro);

            PaginacaoConsulta<Despesa> despesas = despesasRepositorio.Listar(query, request.Qt, request.Pg, request.CpOrd, request.TpOrd);
            PaginacaoConsulta<DespesaResponse> response = mapper.Map<PaginacaoConsulta<DespesaResponse>>(despesas);

            return response;
        }


        public IList<DespesaResponse> ListarDespesasUsuario(string emailUsuario)
        {

            IList<Despesa> despesas = despesasRepositorio.ListarDespesasUsuario(emailUsuario);
            var response = mapper.Map<IList<DespesaResponse>>(despesas);
            return response;
        }

        public PaginacaoConsulta<DespesaConsultaResponse> ListarDespesasUsuarioNaoPagasMesesAnteriorDapper()
        {

        var despesas = despesasConsultasRepositorio.ListarDespesasUsuarioNaoPagasMesesAnterior(1, 100);
        var response = mapper.Map<PaginacaoConsulta<DespesaConsultaResponse>>(despesas);
         return response;

        }

        public IList<DespesaResponse> ListarDespesasUsuarioNaoPagasMesesAtras(string email)
        {
            IList<Despesa> despesas = despesasRepositorio.ListarDespesasUsuarioNaoPagasMesesAnterior(email);
            var response = mapper.Map<IList<DespesaResponse>>(despesas);
            return response;
        }

        public DespesaResponse Recuperar(int id)
        {
            Despesa despesa = despesasServico.Validar(id);
            var response = mapper.Map<DespesaResponse>(despesa);
            return response;
        }

        public IList<DespesasResumo> Consulta()
        {
            var query = despesasRepositorio.Query();
            return query
            .Select(x => new DespesasResumo
            {
                NomeDespesa = x.Nome,
                NomeSistema = x.Categoria.SistemaFinanceiro.Nome,
                NomeCategoria = x.Categoria.Nome,
                NomeUsuario = x.Usuario.Nome
            }).ToList();
        }

        public Stream ExportarExcel(DespesaListarRequest request)
        {
            IWorkbook planilha = new XSSFWorkbook();
            ISheet folha = planilha.CreateSheet("Despesas");
            IRow cabecalho = folha.CreateRow(0);

            cabecalho.CreateCell(0).SetCellValue("NomeDespesa");
            cabecalho.CreateCell(1).SetCellValue("NomeCategoria");
            cabecalho.CreateCell(2).SetCellValue("NomeSistema");
            cabecalho.CreateCell(3).SetCellValue("NomeUsuario");

            var filtro = mapper.Map<DespesaListarFiltro>(request);

            IQueryable<Despesa> query = despesasRepositorio.Filtrar(filtro);

            var queryProjecao = query.Select(x => new
            {
                NomeDespesa = x.Nome,
                NomeSistema = x.Categoria.SistemaFinanceiro.Nome,
                NomeCategoria = x.Categoria.Nome,
                NomeUsuario = x.Usuario.Nome
            });

            int indiceDeLinha = 1;

            foreach (var despesa in queryProjecao)
            {
                IRow linha = folha.CreateRow(indiceDeLinha);
                linha.CreateCell(0).SetCellValue(despesa.NomeDespesa);
                linha.CreateCell(1).SetCellValue(despesa.NomeSistema);
                linha.CreateCell(2).SetCellValue(despesa.NomeCategoria);
                linha.CreateCell(3).SetCellValue(despesa.NomeUsuario);
                indiceDeLinha++;
            }

            for (int coluna = 0; coluna <= cabecalho.LastCellNum - 1; coluna++)
            {
                folha.AutoSizeColumn(coluna);
            }

            var memoryStream = new MemoryStream();
            planilha.Write(memoryStream, false);
            var bytes = memoryStream.ToArray();
            File.WriteAllBytes($@"C:\\Users\nickc\Downloads_{DateTime.Now:ddMMyyyyHHmmss}.xlsx", bytes);

            return new MemoryStream(bytes);
        }
    }
}