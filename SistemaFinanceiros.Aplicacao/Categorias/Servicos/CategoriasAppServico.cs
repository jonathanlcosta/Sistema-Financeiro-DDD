using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using NHibernate;
using SistemaFinanceiros.Aplicacao.Categorias.Servicos.Interfaces;
using SistemaFinanceiros.DataTransfer.Categorias.Request;
using SistemaFinanceiros.DataTransfer.Categorias.Response;
using SistemaFinanceiros.Dominio.Categorias.Entidades;
using SistemaFinanceiros.Dominio.Categorias.Repositorios;
using SistemaFinanceiros.Dominio.Categorias.Servicos.Interfaces;
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
        private readonly ISession session;
        public CategoriasAppServico(ICategoriasServico categoriasServico, ICategoriasRepositorio categoriasRepositorio,
         ISistemaFinanceirosServico sistemaFinanceirosServico, IMapper mapper, ISession session)
        {
            this.categoriasRepositorio = categoriasRepositorio;
            this.categoriasServico = categoriasServico;
            this.sistemaFinanceirosServico = sistemaFinanceirosServico;
            this.session = session;
            this.mapper = mapper;
        }
        public CategoriaResponse Editar(int id, CategoriaEditarRequest categoriaEditarRequest)
        {
            var transacao = session.BeginTransaction();
            try
            {
                var categoria = categoriasServico.Editar(id, categoriaEditarRequest.Nome, categoriaEditarRequest.idSistemaFinanceiro);
                if(transacao.IsActive)
                    transacao.Commit();
                return mapper.Map<CategoriaResponse>(categoria);;
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
                var categoria = categoriasServico.Validar(id);
                categoriasRepositorio.Excluir(categoria);
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

        public CategoriaResponse Inserir(CategoriaInserirRequest categoriaInserirRequest)
        {
            var categoria = categoriasServico.Instanciar(categoriaInserirRequest.Nome, categoriaInserirRequest.idSistemaFinanceiro);
             var transacao = session.BeginTransaction();
            try
            {
                categoria = categoriasServico.Inserir(categoria);
                if(transacao.IsActive)
                    transacao.Commit();
                return mapper.Map<CategoriaResponse>(categoria);
            }
            catch
            {
                if(transacao.IsActive)
                    transacao.Rollback();
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

        public CategoriaResponse Recuperar(int id)
        {
            var categoria = categoriasServico.Validar(id);
            var response = mapper.Map<CategoriaResponse>(categoria);
            return response;
        }
    }
}