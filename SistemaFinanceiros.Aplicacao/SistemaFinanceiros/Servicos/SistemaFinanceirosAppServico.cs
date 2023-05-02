using AutoMapper;
using NHibernate;
using SistemaFinanceiros.Aplicacao.SistemaFinanceiros.Servicos.Interfaces;
using SistemaFinanceiros.Aplicacao.Transacoes.Interfaces;
using SistemaFinanceiros.DataTransfer.SistemaFinanceiros.Request;
using SistemaFinanceiros.DataTransfer.SistemaFinanceiros.Response;
using SistemaFinanceiros.Dominio.SistemaFinanceiros.Entidades;
using SistemaFinanceiros.Dominio.SistemaFinanceiros.Repositorios;
using SistemaFinanceiros.Dominio.SistemaFinanceiros.Servicos.Interfaces;
using SistemaFinanceiros.Dominio.util;

namespace SistemaFinanceiros.Aplicacao.SistemaFinanceiros.Servicos
{
    public class SistemaFinanceirosAppServico : ISistemaFinanceirosAppServico
    {
        private readonly ISistemaFinanceirosServico sistemaFinanceirosServico;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly ISistemaFinanceirosRepositorio sistemaFinanceirosRepositorio;
        public SistemaFinanceirosAppServico(ISistemaFinanceirosServico sistemaFinanceirosServico,IMapper mapper,
        IUnitOfWork unitOfWork, ISistemaFinanceirosRepositorio sistemaFinanceirosRepositorio)
        {
            this.sistemaFinanceirosServico = sistemaFinanceirosServico;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.sistemaFinanceirosRepositorio = sistemaFinanceirosRepositorio;
        }
        public SistemaFinanceiroResponse Editar(int id, SistemaFinanceiroEditarRequest sistemaFinanceiroEditarRequest)
        {
           
            try
            {
                unitOfWork.BeginTransaction();
                var sistemaFinanceiro = sistemaFinanceirosServico.Editar(id, sistemaFinanceiroEditarRequest.Nome,
            sistemaFinanceiroEditarRequest.Mes, sistemaFinanceiroEditarRequest.Ano, sistemaFinanceiroEditarRequest.DiaFechamento,
            sistemaFinanceiroEditarRequest.GerarCopiaDespesa, sistemaFinanceiroEditarRequest.MesCopia, 
            sistemaFinanceiroEditarRequest.AnoCopia
            );
                unitOfWork.Commit();
                return mapper.Map<SistemaFinanceiroResponse>(sistemaFinanceiro);;
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
                var sistemaFinanceiro = sistemaFinanceirosServico.Validar(id);
                sistemaFinanceirosRepositorio.Excluir(sistemaFinanceiro);
                unitOfWork.Commit();
            }
            catch
            {
               unitOfWork.Rollback();
                throw;
            }
        }

        public SistemaFinanceiroResponse Inserir(SistemaFinanceiroInserirRequest sistemaFinanceiroInserirRequest)
        {
            var sistemaFinanceiro = sistemaFinanceirosServico.Instanciar(sistemaFinanceiroInserirRequest.Nome,
            sistemaFinanceiroInserirRequest.Mes, sistemaFinanceiroInserirRequest.Ano, sistemaFinanceiroInserirRequest.DiaFechamento,
            sistemaFinanceiroInserirRequest.GerarCopiaDespesa, sistemaFinanceiroInserirRequest.MesCopia,
            sistemaFinanceiroInserirRequest.AnoCopia);
            try
            {
                unitOfWork.BeginTransaction();
                sistemaFinanceiro = sistemaFinanceirosServico.Inserir(sistemaFinanceiro);
                unitOfWork.Commit();
                return mapper.Map<SistemaFinanceiroResponse>(sistemaFinanceiro);
            }
            catch
            {
                unitOfWork.Rollback();
                throw;
            }
        }

        public PaginacaoConsulta<SistemaFinanceiroResponse> Listar(int? pagina, int quantidade, SistemaFinanceiroListarRequest sistemaFinanceiroListarRequest)
        {
            if (pagina.Value <= 0) throw new Exception("Pagina não especificada");
            IQueryable<SistemaFinanceiro> query = sistemaFinanceirosRepositorio.Query();
            if(sistemaFinanceiroListarRequest is null)
            throw new Exception();
            if (!string.IsNullOrEmpty(sistemaFinanceiroListarRequest.Nome))
                query = query.Where(p => p.Nome.Contains(sistemaFinanceiroListarRequest.Nome));
            PaginacaoConsulta<SistemaFinanceiro> sistemaFinanceiros = sistemaFinanceirosRepositorio.Listar(query, pagina, quantidade);
            PaginacaoConsulta<SistemaFinanceiroResponse> response;
            response = mapper.Map<PaginacaoConsulta<SistemaFinanceiroResponse>>(sistemaFinanceiros);
            return response;
        }

        public SistemaFinanceiroResponse Recuperar(int id)
        {
            var sistemaFinanceiro = sistemaFinanceirosServico.Validar(id);
            var response = mapper.Map<SistemaFinanceiroResponse>(sistemaFinanceiro);
            return response;
        }
    }
}