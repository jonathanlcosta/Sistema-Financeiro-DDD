using AutoMapper;
using SistemaFinanceiros.Aplicacao.SistemaFinanceiros.Servicos.Interfaces;
using SistemaFinanceiros.Aplicacao.Transacoes.Interfaces;
using SistemaFinanceiros.DataTransfer.SistemaFinanceiros.Request;
using SistemaFinanceiros.DataTransfer.SistemaFinanceiros.Response;
using SistemaFinanceiros.Dominio.SistemaFinanceiros.Entidades;
using SistemaFinanceiros.Dominio.SistemaFinanceiros.Repositorios;
using SistemaFinanceiros.Dominio.SistemaFinanceiros.Servicos.Comandos;
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
        public SistemaFinanceiroResponse Editar(int id, SistemaFinanceiroEditarRequest request)
        {
           var comando = mapper.Map<SistemaFinanceiroComando>(request);
            try
            {
                unitOfWork.BeginTransaction();
                var sistemaFinanceiro = sistemaFinanceirosServico.Editar(id, comando
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

        public SistemaFinanceiroResponse Inserir(SistemaFinanceiroInserirRequest request)
        {
            var comando = mapper.Map<SistemaFinanceiroComando>(request);
            try
            {
                unitOfWork.BeginTransaction();
                SistemaFinanceiro sistemaFinanceiro = sistemaFinanceirosServico.Inserir(comando);
                unitOfWork.Commit();
                return mapper.Map<SistemaFinanceiroResponse>(sistemaFinanceiro);
            }
            catch
            {
                unitOfWork.Rollback();
                throw;
            }
        }

        public PaginacaoConsulta<SistemaFinanceiroResponse> Listar(SistemaFinanceiroListarRequest request)
        {
            IQueryable<SistemaFinanceiro> query = sistemaFinanceirosRepositorio.Query();
            if (!string.IsNullOrEmpty(request.Nome))
                query = query.Where(p => p.Nome.Contains(request.Nome));
            PaginacaoConsulta<SistemaFinanceiro> sistemaFinanceiros = sistemaFinanceirosRepositorio.Listar(query, request.Pg, request.Qt, request.CpOrd, request.TpOrd);
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