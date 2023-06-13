using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FizzWare.NBuilder;
using FluentAssertions;
using NSubstitute;
using SistemaFinanceiros.Dominio.Execoes;
using SistemaFinanceiros.Dominio.SistemaFinanceiros.Entidades;
using SistemaFinanceiros.Dominio.SistemaFinanceiros.Servicos.Interfaces;
using SistemaFinanceiros.Dominio.Usuarios.Entidades;
using SistemaFinanceiros.Dominio.Usuarios.Repositorios;
using SistemaFinanceiros.Dominio.Usuarios.Servicos;
using SistemaFinanceiros.Dominio.Usuarios.Servicos.Comandos;
using Xunit;

namespace SistemaFinanceiros.Dominio.Testes.Usuarios.Servicos
{
    public class UsuariosServicoTestes
    {
        private readonly UsuariosServico sut;
        private readonly IUsuariosRepositorio usuariosRepositorio;
        private readonly Usuario usuarioValido;
        private readonly ISistemaFinanceirosServico sistemaFinanceirosServico;

        public UsuariosServicoTestes()
        {
            usuariosRepositorio = Substitute.For<IUsuariosRepositorio>();
            sistemaFinanceirosServico = Substitute.For<ISistemaFinanceirosServico>();
            usuarioValido = Builder<Usuario>.CreateNew().Build();
            sut = new(usuariosRepositorio, sistemaFinanceirosServico);
        }

        public class ValidarMetodo : UsuariosServicoTestes
        {
            [Fact]
            public void Dado_UsuarioNaoEncontrado_Espero_RegraDeNegocioExcecao()
            {
                usuariosRepositorio.Recuperar(2).Returns(x => null);


                sut.Invoking(x => x.Validar(2)).Should().Throw<RegraDeNegocioExcecao>();
            }

            [Fact]
            public void Dado_UsuarioForEncontrado_Espero_UsuarioValido()
            {
                usuariosRepositorio.Recuperar(1).Returns(usuarioValido);
                sut.Validar(1).Should().BeSameAs(usuarioValido);
            }
        }

        public class EditarDespesa : UsuariosServicoTestes
        {
            [Fact]
            public void Quando_DadosUsuarioForemValidos_Espero_ObjetoEditado()
            {
                UsuarioEditarComando comando = Builder<UsuarioEditarComando>.CreateNew().With(x => x.CPF, "09876543212").
                With(x => x.Email, "leonardo@gmail.com").
                Build();
                usuariosRepositorio.Recuperar(1).Returns(usuarioValido);
                SistemaFinanceiro sistemaFinanceiro = sistemaFinanceirosServico.Validar(comando.idSistemaFinanceiro);

                Usuario resultado = sut.Editar(1, comando);
                resultado.Nome.Should().Be(comando.Nome);
                resultado.Email.Should().Be(comando.Email);
                resultado.CPF.Should().Be(comando.CPF);
                resultado.Administrador.Should().Be(comando.Administrador);
                resultado.SistemaFinanceiro.Should().BeSameAs(sistemaFinanceiro);
                resultado.SistemaAtual.Should().Be(comando.SistemaAtual);
                usuariosRepositorio.Editar(resultado).Returns(usuarioValido);
            }

        }

    }
}