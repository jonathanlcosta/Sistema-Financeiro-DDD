using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FizzWare.NBuilder;
using FluentAssertions;
using SistemaFinanceiros.Dominio.Execoes;
using SistemaFinanceiros.Dominio.SistemaFinanceiros.Entidades;
using SistemaFinanceiros.Dominio.Usuarios.Entidades;
using Xunit;

namespace SistemaFinanceiros.Dominio.Testes.Usuarios.Entidades
{
    public class UsuariosTestes
    {
        private readonly Usuario sut;
        public UsuariosTestes()
        {
             sut = Builder<Usuario>.CreateNew().Build();
        }

        public class Construtor
        {
            [Fact]
            public void Quando_ParametrosForemValidos_Espero_ObjetoIntegro()
            {
                string nome = "Leonardo";
                string cpf = "18976543494";
                string email = "leonardo@gmail.com";
                string senha = "Aleatorio@123";
                SistemaFinanceiro sistema = Builder<SistemaFinanceiro>.CreateNew().Build();
                
                bool adminsitrador = true;
                bool sistemaAtual = false;
                Usuario usuario = new(cpf, nome, email, senha, adminsitrador, sistemaAtual, sistema);
                usuario.Nome.Should().Be(nome);
                usuario.CPF.Should().Be(cpf);
                usuario.Email.Should().Be(email);
                usuario.Senha.Should().Be(senha);
                usuario.SistemaFinanceiro.Should().BeSameAs(sistema);
                usuario.SistemaAtual.Should().Be(sistemaAtual);
                usuario.Administrador.Should().Be(adminsitrador);
            }

            [Fact]
            public void Quando_ParametrosForemValidosParaLogin_Espero_ObjetoInstanciado()
            {
                string email = "leonardo@gmail.com";
                string senha = "Aleatorio@123";
                Usuario usuario = new(email, senha);
                usuario.Email.Should().Be(email);
                usuario.Senha.Should().Be(senha);
            }

        }

        public class SetCPFMetodo : UsuariosTestes
        {
            [Theory]
            [InlineData("")]
            [InlineData(null)]
            [InlineData("  ")]
            public void Dado_CpfNuloOuEspacoEmBranco_Espero_AtributoObrigatorioExcecao(string cpf)
            {
                sut.Invoking(x => x.SetCpf(cpf)).Should().Throw<AtributoObrigatorioExcecao>();
            }

            [Fact]
            public void Quando_CpfForMaiorQue11Caracteres_Espero_TamanhoDeAtributoExcecao()
            {
                sut.Invoking(x => x.SetCpf(new string('*', 12))).Should().Throw<TamanhoDeAtributoInvalidoExcecao>();
            }

            [Fact]
            public void Quando_CpfForValido_Espero_PropriedadePreenchida()
            {
                sut.SetCpf("18976543494");
                sut.CPF.Should().Be("18976543494");
            }
        }

        public class SetNomeMetodo: UsuariosTestes
        {
            [Theory]
            [InlineData("")]
            [InlineData(null)]
            [InlineData("  ")]
            public void Dado_NomeNuloOuEspacoEmBranco_Espero_AtributoObrigatorioExcecao(string nome)
            {
                sut.Invoking(x => x.SetNome(nome)).Should().Throw<AtributoObrigatorioExcecao>();
            }

            [Fact]
            public void Quando_NomeForMaiorQue100Caracteres_Espero_TamanhoDeAtributoExcecao()
            {
                sut.Invoking(x => x.SetNome(new string('*', 101))).Should().Throw<TamanhoDeAtributoInvalidoExcecao>();
            }

            [Fact]
            public void Quando_NomeForValido_Espero_PropriedadePreenchida()
            {
                sut.SetNome("Eduardo");
                sut.Nome.Should().Be("Eduardo");
            }
        }

        public class SetEmailMetodo: UsuariosTestes
        {
            [Theory]
            [InlineData("email")]
            [InlineData("")]
            public void Dado_EmailInvalidoSemArrobaOuPontoComOuEmBranco_Espero_AtributoInvalidoExcecao(string email)
            {
                sut.Invoking(x => x.SetEmail(email)).Should().Throw<AtributoInvalidoExcecao>();
            }

            [Fact]
            public void Dado_EmailValido_Espero_PropriedadePreenchida()
            {
                string email = "leonardo@gmail.com";
                sut.SetEmail(email);
                sut.Email.Should().Be(email);
            }
        }

        public class SetSenhaMetodo: UsuariosTestes
        {
            [Theory]
            [InlineData("")]
            [InlineData(null)]
            [InlineData("  ")]
            public void Dado_SenhaNuloOuEspacoEmBranco_Espero_AtributoObrigatorioExcecao(string senha)
            {
                sut.Invoking(x => x.SetSenha(senha)).Should().Throw<AtributoObrigatorioExcecao>();
            }

            [Fact]
            public void Quando_SenhaForMaior20Caracteres_Espero_TamanhoDeAtributoExcecao()
            {
                sut.Invoking(x => x.SetSenha(new string('*', 21))).Should().Throw<TamanhoDeAtributoInvalidoExcecao>();
            }

            [Fact]
            public void Quando_SenhaForMenorQue6Caracteres_Espero_TamanhoDeAtributoExcecao()
            {
                sut.Invoking(x => x.SetSenha(new string('*', 5))).Should().Throw<TamanhoDeAtributoInvalidoExcecao>();
            }
            
            [Theory]
            [InlineData("aleatorio")]
            [InlineData("ALEATORIO")]
            public void Quando_SenhaForSomenteComLetrasMinusculasOuMaiusculasESemCaracteresEspeciaisESemNumero_Espero_AtributoInvalidoExcecao(string senha)
            {
                sut.Invoking(x => x.SetSenha(senha)).Should().Throw<AtributoInvalidoExcecao>();
            }

            [Fact]
            public void Dado_SenhaValida_Espero_PropriedadePreenchida()
            {
                string senha = "Aleatorio@123";
                sut.SetSenha(senha);
                sut.Senha.Should().Be(senha);
            }

        }

        public class SetSistemaMetodo : UsuariosTestes
        {
            [Fact]
            public void Quando_SistemaForNulo_Espero_AtributoObrigatorioExcecao()
            {
                sut.Invoking(x => x.SetSistemaFinanceiro(null)).Should().Throw<AtributoObrigatorioExcecao>();
            }

            [Fact]
            public void Quando_SistemaForValido_Espero_PropriedadePreenchida()
            {
                SistemaFinanceiro sistema = Builder<SistemaFinanceiro>.CreateNew().Build();
                sut.SetSistemaFinanceiro(sistema);
                sut.SistemaFinanceiro.Should().BeSameAs(sistema);
            }

        }

        public class SetSenhaHashMetodo : UsuariosTestes
        {
            [Fact]
            public void Quando_SenhaHashForValida_Espero_PropriedadePreenchida()
            {
                string senha = "Aleatorio@123";
                sut.SetSenhaHash(senha);
                sut.Senha.Should().Be(senha);
            }        

        }

        public class SetAdministradorMetodo: UsuariosTestes
        {
            [Fact]
            public void Dado_Administrador_Espero_PropriedadePreenchida()
            {
                bool administrador = true;
                sut.SetAdministrador(administrador);
                sut.Administrador.Should().Be(administrador);
            }
        }

        public class SetSistemaAtualMetodo: UsuariosTestes
        {
            [Fact]
            public void Dado_SistemaAtual_Espero_PropriedadePreenchida()
            {
                bool sistemaAtual = true;
                sut.SetSistemaAtual(sistemaAtual);
                sut.SistemaAtual.Should().Be(sistemaAtual);
            }
        }

    }
}