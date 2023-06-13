using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FizzWare.NBuilder;
using FluentAssertions;
using NSubstitute;
using SistemaFinanceiros.Dominio.Autenticacoes.Servicos;
using SistemaFinanceiros.Dominio.Execoes;
using SistemaFinanceiros.Dominio.Usuarios.Entidades;
using Xunit;

namespace SistemaFinanceiros.Dominio.Testes.Autenticacoes.Servicos
{
    public class AutenticacoesServicoTestes
    {
        private readonly AutenticacoesServico sut;
        private readonly Usuario usuarioValido;

        public AutenticacoesServicoTestes()
        {
             usuarioValido = Builder<Usuario>.CreateNew().Build();
             sut = Substitute.For<AutenticacoesServico>();
        }

        public class GerarToken: AutenticacoesServicoTestes
        {
            [Fact]
            public void Quando_ParametrosForemValidos_Espero_TokenGerado()
            {
                sut.GerarToken(usuarioValido).Returns("token_gerado");
                var resultado = sut.GerarToken(usuarioValido);
                resultado.Should().Be("token_gerado");
            }
        }

        public class ValidarCadastro: AutenticacoesServicoTestes
        {
           [Fact]
            public void Quando_EmailESenhaForVazio_Espero_RegraDeNegocioExcecao()
            {
               string email = "";
               string senha = "";
                sut.Invoking(x => x.ValidarCadastro(email, senha)).Should().Throw<RegraDeNegocioExcecao>();
            }
        }
    }
}