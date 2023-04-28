using AutoMapper;
using NHibernate;
using SistemaFinanceiros.Aplicacao.SistemaFinanceiros.Servicos.Interfaces;
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
        private readonly ISession session;
        private readonly ISistemaFinanceirosRepositorio sistemaFinanceirosRepositorio;
        public SistemaFinanceirosAppServico(ISistemaFinanceirosServico sistemaFinanceirosServico,IMapper mapper,
        ISession session, ISistemaFinanceirosRepositorio sistemaFinanceirosRepositorio)
        {
            this.sistemaFinanceirosServico = sistemaFinanceirosServico;
            this.mapper = mapper;
            this.session = session;
            this.sistemaFinanceirosRepositorio = sistemaFinanceirosRepositorio;
        }
        public SistemaFinanceiroResponse Editar(int id, SistemaFinanceiroEditarRequest sistemaFinanceiroEditarRequest)
        {
           
             var transacao = session.BeginTransaction();
            try
            {
                var sistemaFinanceiro = sistemaFinanceirosServico.Editar(id, sistemaFinanceiroEditarRequest.Nome,
            sistemaFinanceiroEditarRequest.Mes, sistemaFinanceiroEditarRequest.Ano, sistemaFinanceiroEditarRequest.DiaFechamento,
            sistemaFinanceiroEditarRequest.GerarCopiaDespesa, sistemaFinanceiroEditarRequest.MesCopia, 
            sistemaFinanceiroEditarRequest.AnoCopia
            );
                if(transacao.IsActive)
                    transacao.Commit();
                return mapper.Map<SistemaFinanceiroResponse>(sistemaFinanceiro);;
            }
            catch
            {
                if(transacao.IsActive)
                    transacao.Rollback();
                throw;
            }
        }

        public void Excluir(int id)
        {
             var transacao = session.BeginTransaction();
            try
            {
                var sistemaFinanceiro = sistemaFinanceirosServico.Validar(id);
                sistemaFinanceirosRepositorio.Excluir(sistemaFinanceiro);
                if(transacao.IsActive)
                    transacao.Commit();
            }
            catch
            {
                if(transacao.IsActive)
                    transacao.Rollback();
                throw;
            }
        }

        public SistemaFinanceiroResponse Inserir(SistemaFinanceiroInserirRequest sistemaFinanceiroInserirRequest)
        {
            var sistemaFinanceiro = sistemaFinanceirosServico.Instanciar(sistemaFinanceiroInserirRequest.Nome,
            sistemaFinanceiroInserirRequest.Mes, sistemaFinanceiroInserirRequest.Ano, sistemaFinanceiroInserirRequest.DiaFechamento,
            sistemaFinanceiroInserirRequest.GerarCopiaDespesa, sistemaFinanceiroInserirRequest.MesCopia,
            sistemaFinanceiroInserirRequest.AnoCopia);
             var transacao = session.BeginTransaction();
            try
            {
                sistemaFinanceiro = sistemaFinanceirosServico.Inserir(sistemaFinanceiro);
                if(transacao.IsActive)
                    transacao.Commit();
                return mapper.Map<SistemaFinanceiroResponse>(sistemaFinanceiro);
            }
            catch
            {
                if(transacao.IsActive)
                    transacao.Rollback();
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