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
using SistemaFinanceiros.Dominio.Usuarios.Repositorios.Filtros;
using SistemaFinanceiros.Dominio.Usuarios.Servicos.Comandos;
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
        public UsuarioResponse Editar(int id, UsuarioEditarRequest request)
        {
            UsuarioEditarComando comando = mapper.Map<UsuarioEditarComando>(request);
            try
            {
                unitOfWork.BeginTransaction();
                Usuario usuario = usuariosServico.Editar(id, comando);
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
                Usuario usuario = usuariosServico.Validar(id);
                usuariosRepositorio.Excluir(usuario);
                unitOfWork.Commit();
            }
            catch
            {
                unitOfWork.Rollback();
                throw;
            }
        }

        public PaginacaoConsulta<UsuarioResponse> Listar(UsuarioListarRequest request)
        {
          

            UsuarioListarFiltro filtro = mapper.Map<UsuarioListarFiltro>(request);
            IQueryable<Usuario> query = usuariosRepositorio.Filtrar(filtro);


            PaginacaoConsulta<Usuario> usuarios = usuariosRepositorio.Listar(query, request.Qt, request.Pg, request.CpOrd, request.TpOrd);
            PaginacaoConsulta<UsuarioResponse> response;
            response = mapper.Map<PaginacaoConsulta<UsuarioResponse>>(usuarios);
            return response;
        }

        public UsuarioResponse Recuperar(int id)
        {
           Usuario usuario = usuariosServico.Validar(id);
            var response = mapper.Map<UsuarioResponse>(usuario);
            return response;
        }
    }
}