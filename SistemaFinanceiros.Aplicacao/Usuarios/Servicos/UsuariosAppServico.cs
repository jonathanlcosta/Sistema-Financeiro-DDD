using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using NHibernate;
using SistemaFinanceiros.Aplicacao.Usuarios.Servicos.Interfaces;
using SistemaFinanceiros.DataTransfer.Usuarios.Request;
using SistemaFinanceiros.DataTransfer.Usuarios.Response;
using SistemaFinanceiros.Dominio.SistemaFinanceiros.Servicos.Interfaces;
using SistemaFinanceiros.Dominio.Usuarios.Entidades;
using SistemaFinanceiros.Dominio.Usuarios.Repositorios;
using SistemaFinanceiros.Dominio.Usuarios.Servicos.Interfaces;
using SistemaFinanceiros.Dominio.util;

namespace SistemaFinanceiros.Aplicacao.Usuarios.Servicos
{
    public class UsuariosAppServico : IUsuariosAppServico
    {
        private readonly ISistemaFinanceirosServico sistemaFinanceirosServico;
        private readonly IUsuariosServico usuariosServico;
        private readonly IUsuariosRepositorio usuariosRepositorio;
        private readonly ISession session;
        private readonly IMapper mapper;
        
        public UsuariosAppServico(ISistemaFinanceirosServico sistemaFinanceirosServico, IUsuariosServico usuariosServico,
        IUsuariosRepositorio usuariosRepositorio, ISession session,  IMapper mapper)
        {
            this.sistemaFinanceirosServico = sistemaFinanceirosServico;
            this.usuariosRepositorio = usuariosRepositorio;
            this.usuariosServico = usuariosServico;
            this.mapper = mapper;
            this.session = session;
        }
        public UsuarioResponse Editar(int id, UsuarioEditarRequest usuarioEditarRequest)
        {
           var usuario = mapper.Map<Usuario>(usuarioEditarRequest);
           usuario = usuariosServico.Editar(id, usuarioEditarRequest.CPF, usuarioEditarRequest.Nome, usuarioEditarRequest.Email, 
           usuarioEditarRequest.Administrador, usuarioEditarRequest.SistemaAtual, usuarioEditarRequest.idSistemaFinanceiro);
           var transacao = session.BeginTransaction();
            try
            {
                usuario = usuariosRepositorio.Editar(usuario);
                if(transacao.IsActive)
                    transacao.Commit();
                return mapper.Map<UsuarioResponse>(usuario);;
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
                var usuario = usuariosServico.Validar(id);
                usuariosRepositorio.Excluir(usuario);
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

        public PaginacaoConsulta<UsuarioResponse> Listar(int? pagina, int quantidade, UsuarioListarRequest usuarioListarRequest)
        {
            if (pagina.Value <= 0) throw new Exception("Pagina não especificada");

            IQueryable<Usuario> query = usuariosRepositorio.Query();
            if (usuarioListarRequest is null)
                throw new Exception();

            if (!string.IsNullOrEmpty(usuarioListarRequest.Nome))
                query = query.Where(p => p.Nome.Contains(usuarioListarRequest.Nome));
            if (!string.IsNullOrEmpty(usuarioListarRequest.CPF))
                query = query.Where(p => p.CPF.Contains(usuarioListarRequest.CPF));
            if (!string.IsNullOrEmpty(usuarioListarRequest.Email))
                query = query.Where(p => p.Email.Contains(usuarioListarRequest.Email));

                if (usuarioListarRequest.idSistemaFinanceiro != 0)
            {
                query = query.Where(x => x.SistemaFinanceiro!.Id == usuarioListarRequest.idSistemaFinanceiro);
            }

            PaginacaoConsulta<Usuario> usuarios = usuariosRepositorio.Listar(query, pagina, quantidade);
            PaginacaoConsulta<UsuarioResponse> response;
            response = mapper.Map<PaginacaoConsulta<UsuarioResponse>>(usuarios);
            return response;
        }

        public UsuarioResponse Recuperar(int id)
        {
           var usuario = usuariosServico.Validar(id);
            var response = mapper.Map<UsuarioResponse>(usuario);
            return response;
        }
    }
}