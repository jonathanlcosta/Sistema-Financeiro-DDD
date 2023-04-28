using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaFinanceiros.Dominio.SistemaFinanceiros.Entidades;
using SistemaFinanceiros.Dominio.SistemaFinanceiros.Servicos.Interfaces;
using SistemaFinanceiros.Dominio.Usuarios.Entidades;
using SistemaFinanceiros.Dominio.Usuarios.Repositorios;
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
        public Usuario Editar(int id, string cpf, string nome, string email, bool administrador, bool sistemaAtual, int idSistemaFinanceiro)
        {
           var sistemaFinanceiro = sistemaFinanceirosServico.Validar(idSistemaFinanceiro);
           var usuario = Validar(id);
           usuario.SetCpf(cpf);
           usuario.SetEmail(email);
           usuario.SetAdministrador(administrador);
           usuario.SetSistemaAtual(sistemaAtual);
           usuario.SetSistemaFinanceiro(sistemaFinanceiro);
           usuario.SetNome(nome);
           usuario = usuariosRepositorio.Editar(usuario);
           return usuario;
        }

        public Usuario Inserir(Usuario usuario)
        {
            var response = usuariosRepositorio.Inserir(usuario);
            return response;
        }

        public Usuario Instanciar(string cpf, string nome, string email, string senha, bool administrador, bool sistemaAtual, int idSistemaFinanceiro)
        {
            SistemaFinanceiro sistemaFinanceiro = sistemaFinanceirosServico.Validar(idSistemaFinanceiro);
            var usuario = new Usuario(cpf, nome , email, senha, administrador, sistemaAtual, sistemaFinanceiro);
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