using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FizzWare.NBuilder;
using FluentAssertions;
using NSubstitute;
using SistemaFinanceiros.Dominio.Categorias.Entidades;
using SistemaFinanceiros.Dominio.Categorias.Repositorios;
using SistemaFinanceiros.Dominio.Categorias.Servicos;
using SistemaFinanceiros.Dominio.Categorias.Servicos.Comandos;
using SistemaFinanceiros.Dominio.Execoes;
using SistemaFinanceiros.Dominio.SistemaFinanceiros.Entidades;
using SistemaFinanceiros.Dominio.SistemaFinanceiros.Servicos.Interfaces;
using Xunit;

namespace SistemaFinanceiros.Dominio.Testes.Categorias.Servicos
{
    public class CategoriasServicoTestes
    {
         private readonly CategoriasServico sut;
        private readonly ICategoriasRepositorio categoriasRepositorio;
        private readonly ISistemaFinanceirosServico sistemaFinanceirosServico;
        private readonly Categoria categoriaValido;
        private readonly SistemaFinanceiro sistemaValido;
        public CategoriasServicoTestes()
        {
            categoriasRepositorio = Substitute.For<ICategoriasRepositorio>();
            categoriaValido = Builder<Categoria>.CreateNew().Build();
            sistemaValido = Builder<SistemaFinanceiro>.CreateNew().Build();
            sistemaFinanceirosServico = Substitute.For<ISistemaFinanceirosServico>();
            sut = new CategoriasServico(categoriasRepositorio, sistemaFinanceirosServico);
        }

        public class ValidarMetodo : CategoriasServicoTestes
        {
            [Fact]
            public void Dado_CategoriaNaoEncontrado_Espero_Excecao()
            {
                categoriasRepositorio.Recuperar(1).Returns(x => null);
                sut.Invoking(x => x.Validar(1)).Should().Throw<RegraDeNegocioExcecao>();
            }

            [Fact]
            public void Dado_CategoriaForEncontrado_Espero_CategoriaValido()
            {
                categoriasRepositorio.Recuperar(1).Returns(categoriaValido);
                sut.Validar(1).Should().BeSameAs(categoriaValido);
            }
        }

        public class InserirMetodo : CategoriasServicoTestes
        {
            [Fact]
            public void Dado_CategoriaValido_Espero_CategoriaInserido()
            {
                CategoriaComando comando = Builder<CategoriaComando>.CreateNew()
                .With(x => x.Nome, "Empresa").With(x => x.IdSistemaFinanceiro, 1).Build();
                
                Categoria resultado = sut.Inserir(comando);
                categoriasRepositorio.Inserir(resultado).Returns(categoriaValido);

                resultado.Should().BeOfType<Categoria>();
                resultado.Nome.Should().Be(comando.Nome);
                resultado.Should().NotBeNull();
            }
        }

        public class EditarMetodo: CategoriasServicoTestes
        {
            [Fact]
            public void Dado_ParametrosParaEditarCategoria_Espero_CategoriaEditado()
            {
               CategoriaComando comando = Builder<CategoriaComando>.CreateNew()
                .With(x => x.Nome, "Empresa").With(x => x.IdSistemaFinanceiro, 1).Build();

                categoriasRepositorio.Recuperar(1).Returns(categoriaValido);

                Categoria resultado = sut.Editar(1, comando);

                resultado.Should().NotBeNull();
                resultado.Nome.Should().Be(comando.Nome);
            }
        }

         public class InstanciarMetodo : CategoriasServicoTestes
        {
            [Fact]
            public void Quando_DadosCategoriaForemValidos_Espero_ObjetoInstanciado()
            {
                CategoriaComando comando = Builder<CategoriaComando>.CreateNew()
                    .Build();

                Categoria resultado = sut.Instanciar(comando);

                resultado.Nome.Should().Be(comando.Nome);
            }

        }


    }
}