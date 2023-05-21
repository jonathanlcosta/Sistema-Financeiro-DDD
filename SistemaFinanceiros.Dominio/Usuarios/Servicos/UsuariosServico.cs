using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaFinanceiros.Dominio.SistemaFinanceiros.Entidades;
using SistemaFinanceiros.Dominio.SistemaFinanceiros.Servicos.Interfaces;
using SistemaFinanceiros.Dominio.Usuarios.Entidades;
using SistemaFinanceiros.Dominio.Usuarios.Repositorios;
using SistemaFinanceiros.Dominio.Usuarios.Servicos.Comandos;
using SistemaFinanceiros.Dominio.Usuarios.Servicos.Interfaces;

namespace SistemaFinanceiros.Dominio.Usuarios.Servicos
{
    public class UsuariosServico : IUsuariosServico
    {
        private readonly IUsuariosRepositorio usuariosRepositorio;
        private readonly ISistemaFinanceirosServico sistemaFinanceirosServico;
        public UsuariosServico(IUsuariosRepositorio usuariosRepositorio, ISistemaFinanceirosServico sistemaFinanceirosServico)
        {
            this.usuariosRepositorio = usuariosRepositorio;
            this.sistemaFinanceirosServico = sistemaFinanceirosServico;
        }
        public Usuario Editar(int id, UsuarioEditarComando comando)
        {
           SistemaFinanceiro sistemaFinanceiro = sistemaFinanceirosServico.Validar(comando.idSistemaFinanceiro);
           Usuario usuario = Validar(id);
           usuario.SetCpf(comando.CPF);
           usuario.SetEmail(comando.Email);
           usuario.SetAdministrador(comando.Administrador);
           usuario.SetSistemaAtual(comando.SistemaAtual);
           usuario.SetSistemaFinanceiro(sistemaFinanceiro);
           usuario.SetNome(comando.Nome);
           usuario = usuariosRepositorio.Editar(usuario);
           return usuario;
        }

        public Usuario Validar(int id)
        {
           var usuarioResponse = this.usuariosRepositorio.Recuperar(id);
            if(usuarioResponse is null)
            {
                 throw new Exception("Usuario não encontrado");
            }
            return usuarioResponse;
        }
    }
}