using AutoMapper;
using NHibernate;
using SistemaFinanceiros.Aplicacao.Despesas.Servicos.Interfaces;
using SistemaFinanceiros.DataTransfer.Despesas.Request;
using SistemaFinanceiros.DataTransfer.Despesas.Response;
using SistemaFinanceiros.Dominio.Categorias.Entidades;
using SistemaFinanceiros.Dominio.Categorias.Servicos.Interfaces;
using SistemaFinanceiros.Dominio.Despesas.Entidades;
using SistemaFinanceiros.Dominio.Despesas.Repositorios;
using SistemaFinanceiros.Dominio.Despesas.Servicos.Interfaces;
using SistemaFinanceiros.Dominio.SistemaFinanceiros.Entidades;
using SistemaFinanceiros.Dominio.Usuarios.Entidades;
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
        private readonly ISession session;
        public DespesasAppServico(IDespesasRepositorio despesasRepositorio, IDespesasServico despesasServico, ICategoriasServico categoriasServico,
        IMapper mapper, ISession session, IDespesasConsultasRepositorio despesasConsultasRepositorio)
        {
            this.categoriasServico = categoriasServico;
            this.despesasServico = despesasServico;
            this.despesasRepositorio = despesasRepositorio;
            this.mapper = mapper;
            this.session = session;
            this.despesasConsultasRepositorio = despesasConsultasRepositorio;
        }
        public DespesaResponse Editar(int id, DespesaEditarRequest despesaEditarRequest)
        {
            var transacao = session.BeginTransaction();
            try
            {
                var despesa = despesasServico.Editar(id, despesaEditarRequest.Nome, despesaEditarRequest.Valor, despesaEditarRequest.Mes, 
           despesaEditarRequest.Ano, despesaEditarRequest.TipoDespesa, despesaEditarRequest.DataCadastro,
           despesaEditarRequest.DataAlteracao, despesaEditarRequest.DataVencimento, despesaEditarRequest.Pago,
           despesaEditarRequest.DespesaAtrasada, despesaEditarRequest.idCategoria);
                if(transacao.IsActive)
                    transacao.Commit();
                return mapper.Map<DespesaResponse>(despesa);;
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
                var despesa = despesasServico.Validar(id);
                despesasRepositorio.Excluir(despesa);
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

        public DespesaResponse Inserir(DespesaInserirRequest despesaInserirRequest)
        {
             var despesa = despesasServico.Instanciar(despesaInserirRequest.Nome, despesaInserirRequest.Valor, despesaInserirRequest.Mes, 
           despesaInserirRequest.Ano, despesaInserirRequest.TipoDespesa, despesaInserirRequest.DataCadastro,
           despesaInserirRequest.DataAlteracao, despesaInserirRequest.DataVencimento, despesaInserirRequest.Pago,
           despesaInserirRequest.DespesaAtrasada, despesaInserirRequest.idCategoria, despesaInserirRequest.IdUsuario);
             var transacao = session.BeginTransaction();
            try
            {
                despesa = despesasServico.Inserir(despesa);
                if(transacao.IsActive)
                    transacao.Commit();
                return mapper.Map<DespesaResponse>(despesa);
            }
            catch
            {
                if(transacao.IsActive)
                    transacao.Rollback();
                throw;
            }
        }

        public PaginacaoConsulta<DespesaResponse> Listar(int? pagina, int quantidade, DespesaListarRequest despesaListarRequest)
        {
            throw new NotImplementedException();
        }

        public IList<DespesaResponse> ListarDespesasUsuario(string emailUsuario)
        {
            IList<Despesa> despesa = despesasRepositorio.Query().Join(session.Query<Categoria>(), 
                  d => d.Id,      
                  c => c.Id,     
                  (d, c) => new { Despesa = d, Categoria = c }) 
            .Join(session.Query<SistemaFinanceiro>(), 
                  dc => dc.Categoria.Id, 
                  s => s.Id,
                  (dc, s) => new { DespesaCategoria = dc, SistemaFinanceiro = s }) 
            .Join(session.Query<Usuario>(), 
                  dcs => dcs.DespesaCategoria.Despesa.Id, 
                  u => u.Id, 
                  (dcs, u) => new { DespesaCategoriaSistemaFinanceiro = dcs, UsuarioSistemaFinanceiro = u }) 
            .Where(s => s.DespesaCategoriaSistemaFinanceiro.DespesaCategoria.Despesa.Usuario.Email == emailUsuario && s.DespesaCategoriaSistemaFinanceiro.SistemaFinanceiro.Mes == s.DespesaCategoriaSistemaFinanceiro.DespesaCategoria.Despesa.Mes && s.DespesaCategoriaSistemaFinanceiro.SistemaFinanceiro.Ano == s.DespesaCategoriaSistemaFinanceiro.DespesaCategoria.Despesa.Ano)
            .Select(s => s.DespesaCategoriaSistemaFinanceiro.DespesaCategoria.Despesa)
            .ToList();

            var response = mapper.Map<IList<DespesaResponse>>(despesa);
            return response;
        }

        public PaginacaoConsulta<DespesaResponse> ListarDespesasUsuarioNaoPagasMesesAnterior(string emailUsuario)
        {

        var despesas = despesasConsultasRepositorio.ListarDespesasUsuarioNaoPagasMesesAnterior(1, 100, emailUsuario);
        var response = mapper.Map<PaginacaoConsulta<DespesaResponse>>(despesas);
         return response;

        }

        public DespesaResponse Recuperar(int id)
        {
            var despesa = despesasServico.Validar(id);
            var response = mapper.Map<DespesaResponse>(despesa);
            return response;
        }
    }
}