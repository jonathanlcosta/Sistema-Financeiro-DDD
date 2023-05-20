using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FizzWare.NBuilder;
using FluentAssertions;
using SistemaFinanceiros.Dominio.Categorias.Entidades;
using SistemaFinanceiros.Dominio.SistemaFinanceiros.Entidades;
using Xunit;

namespace SistemaFinanceiros.Dominio.Testes.Categorias.Entidades
{
    public class CategoriaTestes
    {
        private readonly Categoria sut;
        public CategoriaTestes()
        {
            sut = Builder<Categoria>.CreateNew().Build();
        }

        public class Construtor
        {
            [Fact]
            public void Quando_ParametrosForemValidos_Espero_ObjetoIntegro()
            {
                SistemaFinanceiro sistemaFinanceiro = Builder<SistemaFinanceiro>.CreateNew().Build();
                Categoria categoria = new Categoria("Empresa", sistemaFinanceiro);

                categoria.Nome.Should().Be("Empresa");
                categoria.SistemaFinanceiro.Should().Be(sistemaFinanceiro);

            }
        }

    public class SetNomeMetodo : CategoriaTestes
    {
            [Theory]
            [InlineData(null)]
            [InlineData(" ")]
            [InlineData("")]
            public void Quando_AtributoForNuloOuEspacoEmBranco_Espero_Excecao(string nome)
            {
                
                sut.Invoking(x => x.SetNome(nome)).Should().Throw<ArgumentException>();
            }

            [Fact]
            public void Quando_NomeForMaiorQue100Caracteres_Espero_Excecao()
            {
                sut.Invoking(x => x.SetNome(new string('*', 101))).Should().Throw<ArgumentException>();
            }

            [Fact]
            public void Quando_NomeForValido_Espero_PropriedadePreenchida()
            {
                sut.SetNome("Empresa");
                sut.Nome.Should().Be("Empresa");
            }
    
        }

        public class SetSistemaMetodo : CategoriaTestes
        {
            [Fact]
            public void Quando_SistemaForNulo_Espero_Excecao()
            {
                sut.Invoking(x => x.SetSistema(null)).Should().Throw<ArgumentException>();
            }

            [Fact]
            public void Quando_SistemaForValido_Espero_PropriedadePreenchida()
            {
                 SistemaFinanceiro sistemaFinanceiro = Builder<SistemaFinanceiro>.CreateNew().Build();

                sut.SetSistema(sistemaFinanceiro);
                sut.SistemaFinanceiro.Should().BeSameAs(sistemaFinanceiro);
            }
        }
    }
}