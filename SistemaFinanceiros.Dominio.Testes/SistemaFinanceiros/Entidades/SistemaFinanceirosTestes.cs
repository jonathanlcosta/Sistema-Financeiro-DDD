using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FizzWare.NBuilder;
using FluentAssertions;
using SistemaFinanceiros.Dominio.Execoes;
using SistemaFinanceiros.Dominio.SistemaFinanceiros.Entidades;
using Xunit;

namespace SistemaFinanceiros.Dominio.Testes.SistemaFinanceiros.Entidades
{
    public class SistemaFinanceirosTestes
    {
        private readonly SistemaFinanceiro sut;
        public SistemaFinanceirosTestes()
        {
             sut = Builder<SistemaFinanceiro>.CreateNew().Build();
        }

        public class Construtor
        {
            [Fact]
            public void Quando_ParametrosForemValidos_Espero_ObjetoIntegro()
            {
                string nome = "Sistema";
                SistemaFinanceiro sistema = new(nome);
                sistema.Nome.Should().Be(nome);
            }

        }

        public class SetNomeMetodo : SistemaFinanceirosTestes
    {
            [Theory]
            [InlineData(null)]
            [InlineData(" ")]
            [InlineData("")]
            public void Quando_AtributoForNuloOuEspacoEmBranco_Espero_AtributoObrigatorioExcecao(string nome)
            {
                
                sut.Invoking(x => x.SetNome(nome)).Should().Throw<AtributoObrigatorioExcecao>();
            }

            [Fact]
            public void Quando_NomeForValido_Espero_PropriedadePreenchida()
            {
                sut.SetNome("Empresa");
                sut.Nome.Should().Be("Empresa");
            }
    
        }
    }
}