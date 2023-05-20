using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using NHibernate;
using SistemaFinanceiros.Aplicacao.Transacoes.Interfaces;
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
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        
        public UsuariosAppServico(ISistemaFinanceirosServico sistemaFinanceirosServico, IUsuariosServico usuariosServico,
        IUsuariosRepositorio usuariosRepositorio, IUnitOfWork unitOfWork,  IMapper mapper)
        {
            this.sistemaFinanceirosServico = sistemaFinanceirosServico;
            this.usuariosRepositorio = usuariosRepositorio;
            this.usuariosServico = usuariosServico;
            this.mapper = mapper;
           this.unitOfWork = unitOfWork;
        }
        public UsuarioResponse Editar(int id, UsuarioEditarRequest usuarioEditarRequest)
        {
            try
            {
                unitOfWork.BeginTransaction();
                var usuario = usuariosServico.Editar(id, usuarioEditarRequest.CPF, usuarioEditarRequest.Nome, usuarioEditarRequest.Email, 
           usuarioEditarRequest.Administrador, usuarioEditarRequest.SistemaAtual, usuarioEditarRequest.idSistemaFinanceiro);
                unitOfWork.Commit();
                return mapper.Map<UsuarioResponse>(usuario);;
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
                var usuario = usuariosServico.Validar(id);
                usuariosRepositorio.Excluir(usuario);
                unitOfWork.Commit();
            }
            catch
            {
                unitOfWork.Rollback();
                throw;
            }
        }

        public PaginacaoConsulta<UsuarioResponse> Listar(int? pagina, int quantidade, UsuarioListarRequest request)
        {
            if (pagina.Value <= 0) throw new Exception("Pagina não especificada");

            IQueryable<Usuario> query = usuariosRepositorio.Query();
            if (request is null)
                throw new Exception();

            if (!string.IsNullOrEmpty(request.Nome))
                query = query.Where(p => p.Nome.Contains(request.Nome));
            if (!string.IsNullOrEmpty(request.CPF))
                query = query.Where(p => p.CPF.Contains(request.CPF));
            if (!string.IsNullOrEmpty(request.Email))
                query = query.Where(p => p.Email.Contains(request.Email));

                if (request.idSistemaFinanceiro != 0)
            {
                query = query.Where(x => x.SistemaFinanceiro!.Id == request.idSistemaFinanceiro);
            }

            PaginacaoConsulta<Usuario> usuarios = usuariosRepositorio.Listar(query, request.Pg, request.Qt, request.CpOrd, request.TpOrd);
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