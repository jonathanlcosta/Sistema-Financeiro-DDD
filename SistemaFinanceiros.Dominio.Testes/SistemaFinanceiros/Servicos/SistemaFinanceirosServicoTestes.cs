using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FizzWare.NBuilder;
using FluentAssertions;
using NSubstitute;
using SistemaFinanceiros.Dominio.Execoes;
using SistemaFinanceiros.Dominio.SistemaFinanceiros.Entidades;
using SistemaFinanceiros.Dominio.SistemaFinanceiros.Repositorios;
using SistemaFinanceiros.Dominio.SistemaFinanceiros.Servicos;
using SistemaFinanceiros.Dominio.SistemaFinanceiros.Servicos.Comandos;
using Xunit;

namespace SistemaFinanceiros.Dominio.Testes.SistemaFinanceiros.Servicos
{
    public class SistemaFinanceirosServicoTestes
    {
        private readonly SistemaFinanceirosServico sut;
        private readonly ISistemaFinanceirosRepositorio sistemaFinanceirosRepositorio;
        private readonly SistemaFinanceiro sistemaValido;

        public SistemaFinanceirosServicoTestes()
        {
            sistemaFinanceirosRepositorio = Substitute.For<ISistemaFinanceirosRepositorio>();
            sistemaValido = Builder<SistemaFinanceiro>.CreateNew().Build();
            sut = new(sistemaFinanceirosRepositorio);
        }

        public class ValidarMetodo : SistemaFinanceirosServicoTestes
        {
            [Fact]
            public void Dado_SistemaNaoEncontrado_Espero_RegraDeNegocioExcecao()
            {
                sistemaFinanceirosRepositorio.Recuperar(2).Returns(x => null);


                sut.Invoking(x => x.Validar(2)).Should().Throw<RegraDeNegocioExcecao>();
            }

            [Fact]
            public void Dado_SistemaForEncontrado_Espero_SistemaValido()
            {
                sistemaFinanceirosRepositorio.Recuperar(1).Returns(sistemaValido);
                sut.Validar(1).Should().BeSameAs(sistemaValido);
            }
        }

        public class InserirMetodo: SistemaFinanceirosServicoTestes
        {
            [Fact]
            public void Quando_DadosSistemaForemValidos_Espero_ObjetoInserido()
            {
                SistemaFinanceiroComando comando = Builder<SistemaFinanceiroComando>.CreateNew().
                Build();
                SistemaFinanceiro resultado = sut.Inserir(comando);
                resultado.Nome.Should().Be(comando.Nome);
                sistemaFinanceirosRepositorio.Inserir(resultado).Returns(sistemaValido);
            }
        }

        public class InstanciarMetodo: SistemaFinanceirosServicoTestes
        {
            [Fact]
            public void Quando_DadosSistemaFinanceiroForemValidos_Espero_ObjetoInstanciado()
            {
                SistemaFinanceiroComando comando = Builder<SistemaFinanceiroComando>.CreateNew().
                Build();

                SistemaFinanceiro resultado = sut.Instanciar(comando);
                resultado.Nome.Should().Be(comando.Nome);
            }
        }

        public class EditarDespesa : SistemaFinanceirosServicoTestes
        {
            [Fact]
            public void Quando_DadosSistemaFinanceiroForemValidos_Espero_ObjetoEditado()
            {
                SistemaFinanceiroComando comando = Builder<SistemaFinanceiroComando>.CreateNew().
                Build();
                sistemaFinanceirosRepositorio.Recuperar(1).Returns(sistemaValido);
                SistemaFinanceiro resultado = sut.Editar(1, comando);
                resultado.Nome.Should().Be(comando.Nome);
                sistemaFinanceirosRepositorio.Editar(resultado).Returns(sistemaValido);
            }

        }

    }
}