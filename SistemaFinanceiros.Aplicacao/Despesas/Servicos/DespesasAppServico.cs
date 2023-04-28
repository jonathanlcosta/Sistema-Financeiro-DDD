using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
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
        private readonly IMapper mapper;
        private readonly ISession session;
        public DespesasAppServico(IDespesasRepositorio despesasRepositorio, IDespesasServico despesasServico, ICategoriasServico categoriasServico,
        IMapper mapper, ISession session)
        {
            this.categoriasServico = categoriasServico;
            this.despesasServico = despesasServico;
            this.despesasRepositorio = despesasRepositorio;
            this.mapper = mapper;
            this.session = session;
        }
        public DespesaResponse Editar(int id, DespesaEditarRequest despesaEditarRequest)
        {
            var despesa = mapper.Map<Despesa>(despesaEditarRequest);
           despesa = despesasServico.Editar(id, despesaEditarRequest.Nome, despesaEditarRequest.Valor, despesaEditarRequest.Mes, 
           despesaEditarRequest.Ano, despesaEditarRequest.TipoDespesa, despesaEditarRequest.DataCadastro,
           despesaEditarRequest.DataAlteracao, despesaEditarRequest.DataVencimento, despesaEditarRequest.Pago,
           despesaEditarRequest.DespesaAtrasada, despesaEditarRequest.idCategoria);
            var transacao = session.BeginTransaction();
            try
            {
                despesa = despesasRepositorio.Editar(despesa);
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
           despesaInserirRequest.DespesaAtrasada, despesaInserirRequest.idCategoria);
             var transacao = session.BeginTransaction();
            try
            {
                despesa = despesasRepositorio.Inserir(despesa);
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
            IList<Despesa> despesa = despesasRepositorio.Query().Join(session.Query<Categoria>(), // Segunda sequência a ser unida
                  d => d.Id,        // Chave da primeira sequência
                  c => c.Id,        // Chave da segunda sequência
                  (d, c) => new { Despesa = d, Categoria = c }) // Objeto resultante
            .Join(session.Query<SistemaFinanceiro>(), // Terceira sequência a ser unida
                  dc => dc.Categoria.Id, // Chave da primeira sequência
                  s => s.Id, // Chave da terceira sequência
                  (dc, s) => new { DespesaCategoria = dc, SistemaFinanceiro = s }) // Objeto resultante
            .Join(session.Query<Usuario>(), // Quarta sequência a ser unida
                  dcs => dcs.DespesaCategoria.Despesa.Id, // Chave da primeira sequência
                  u => u.Id, // Chave da quarta sequência
                  (dcs, u) => new { DespesaCategoriaSistemaFinanceiro = dcs, UsuarioSistemaFinanceiro = u }) // Objeto resultante
            .Where(s => s.DespesaCategoriaSistemaFinanceiro.DespesaCategoria.Despesa.Categoria.SistemaFinanceiro.Usuario.Email == emailUsuario && s.DespesaCategoriaSistemaFinanceiro.SistemaFinanceiro.Mes == s.DespesaCategoriaSistemaFinanceiro.DespesaCategoria.Despesa.Mes && s.DespesaCategoriaSistemaFinanceiro.SistemaFinanceiro.Ano == s.DespesaCategoriaSistemaFinanceiro.DespesaCategoria.Despesa.Ano)
            .Select(s => s.DespesaCategoriaSistemaFinanceiro.DespesaCategoria.Despesa)
            .ToList();

            var response = mapper.Map<IList<DespesaResponse>>(despesa);
            return response;
        }

        public IList<DespesaResponse> ListarDespesasUsuarioNaoPagasMesesAnterior(string emailUsuario)
        {
    //         var criteria = session.CreateCriteria<Despesa>()
    //         .CreateAlias("CATEGORIAS", "c")
    //         .CreateAlias("c.SISTEMAFINANCEIROS", "s")
    //         .CreateAlias("s.USUARIOS", "us")
    //         .Add(Restrictions.Eq("us.email", emailUsuario))
    //         .Add(Restrictions.Lt("Mes", DateTime.Now.Month))
    //         .Add(Restrictions.Not(Restrictions.Eq("Pago", true)))
    //         .SetResultTransformer(Transformers.DistinctRootEntity);
    //    var despesa = criteria.List<Despesa>();
    //     var response = mapper.Map<IList<DespesaResponse>>(despesa);
    //         return response;

    IList<Despesa> despesas = despesasRepositorio.ListarDespesasUsuarioNaoPagasMesesAnterior(emailUsuario);
    var response = mapper.Map<IList<DespesaResponse>>(despesas);
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