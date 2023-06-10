using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FizzWare.NBuilder;
using FluentAssertions;
using NSubstitute;
using SistemaFinanceiros.Dominio.Categorias.Entidades;
using SistemaFinanceiros.Dominio.Categorias.Servicos.Interfaces;
using SistemaFinanceiros.Dominio.Despesas.Entidades;
using SistemaFinanceiros.Dominio.Despesas.Repositorios;
using SistemaFinanceiros.Dominio.Despesas.Servicos;
using SistemaFinanceiros.Dominio.Despesas.Servicos.Comandos;
using SistemaFinanceiros.Dominio.Execoes;
using SistemaFinanceiros.Dominio.Usuarios.Entidades;
using SistemaFinanceiros.Dominio.Usuarios.Servicos.Interfaces;
using Xunit;

namespace SistemaFinanceiros.Dominio.Testes.Despesas.Servicos
{
    public class DespesasServicoTestes
    {
        private readonly DespesasServico sut;
        private readonly IDespesasRepositorio despesasRepositorio;
        private readonly IUsuariosServico usuariosServico;
        private readonly ICategoriasServico categoriasServico;
        private readonly Despesa despesaValido;
        private readonly Usuario usuarioValido;
        private readonly Categoria categoriaValido;
        public DespesasServicoTestes()
        {
            despesasRepositorio = Substitute.For<IDespesasRepositorio>();
            usuarioValido = Builder<Usuario>.CreateNew().Build();
            categoriaValido = Builder<Categoria>.CreateNew().Build();
            usuariosServico = Substitute.For<IUsuariosServico>();
            categoriasServico = Substitute.For<ICategoriasServico>();
            despesaValido = Builder<Despesa>.CreateNew().Build();
            sut = new(despesasRepositorio, categoriasServico, usuariosServico);
        }

        public class EditarDespesa : DespesasServicoTestes
        {
            [Fact]
            public void Quando_DadosDespesaForemValidos_Espero_ObjetoEditado()
            {
                DespesaComando comando = Builder<DespesaComando>.CreateNew().
                Build();
                despesasRepositorio.Recuperar(1).Returns(despesaValido);

                Despesa resultado = sut.Editar(1, comando);
                resultado.Nome.Should().Be(comando.Nome);
                resultado.TipoDespesa.Should().Be(comando.TipoDespesa);
                resultado.DespesaAtrasada.Should().Be(comando.DespesaAtrasada);
                resultado.Valor.Should().Be(comando.Valor);
                resultado.DataVencimento.Should().Be(comando.DataVencimento);
                resultado.Pago.Should().Be(comando.Pago);
                despesasRepositorio.Editar(resultado).Returns(despesaValido);
            }

        }

        public class ValidarMetodo : DespesasServicoTestes
        {
            [Fact]
            public void Dado_DespesaNaoEncontrado_Espero_Excecao()
            {
                despesasRepositorio.Recuperar(2).Returns(x => null);


                sut.Invoking(x => x.Validar(2)).Should().Throw<RegraDeNegocioExcecao>();
            }

            [Fact]
            public void Dado_DespesaForEncontrado_Espero_DespesaValido()
            {
                despesasRepositorio.Recuperar(1).Returns(despesaValido);
                sut.Validar(1).Should().BeSameAs(despesaValido);
            }
        }

        public class InserirMetodo: DespesasServicoTestes
        {
            [Fact]
            public void Quando_DadosDespesaForemValidos_Espero_ObjetoInserido()
            {
                DespesaComando comando = Builder<DespesaComando>.CreateNew().
                Build();

                usuariosServico.Validar(comando.IdUsuario).Returns(usuarioValido);
                categoriasServico.Validar(comando.IdCategoria).Returns(categoriaValido);

                Despesa resultado = sut.Inserir(comando);
                resultado.Nome.Should().Be(comando.Nome);
                resultado.TipoDespesa.Should().Be(comando.TipoDespesa);
                resultado.DespesaAtrasada.Should().Be(comando.DespesaAtrasada);
                resultado.Valor.Should().Be(comando.Valor);
                resultado.DataVencimento.Should().Be(comando.DataVencimento);
                resultado.Pago.Should().Be(comando.Pago);
                despesasRepositorio.Inserir(resultado).Returns(despesaValido);
            }
        }

        public class InstanciarMetodo: DespesasServicoTestes
        {
            [Fact]
            public void Quando_DadosDespesaForemValidos_Espero_ObjetoInstanciado()
            {
                DespesaComando comando = Builder<DespesaComando>.CreateNew().
                Build();

                Despesa resultado = sut.Instanciar(comando);
                resultado.Nome.Should().Be(comando.Nome);
                resultado.TipoDespesa.Should().Be(comando.TipoDespesa);
                resultado.DespesaAtrasada.Should().Be(comando.DespesaAtrasada);
                resultado.Valor.Should().Be(comando.Valor);
                resultado.DataVencimento.Should().Be(comando.DataVencimento);
                resultado.Pago.Should().Be(comando.Pago);

            }
        }

    }
}