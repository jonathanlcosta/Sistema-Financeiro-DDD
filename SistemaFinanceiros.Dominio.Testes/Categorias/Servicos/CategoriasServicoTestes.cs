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
                categoriasRepositorio.Recuperar(Arg.Any<int>()).Returns(x => null);
                sut.Invoking(x => x.Validar(1)).Should().Throw<Exception>();
            }

            [Fact]
            public void Dado_CategoriaForEncontrado_Espero_CategoriaValido()
            {
                categoriasRepositorio.Recuperar(Arg.Any<int>()).Returns(categoriaValido);
                sut.Validar(1).Should().BeSameAs(categoriaValido);
            }
        }

        public class InserirMetodo : CategoriasServicoTestes
        {
            [Fact]
            public void Dado_CategoriaValido_Espero_CategoriaInserido()
            {
                categoriasRepositorio.Inserir(Arg.Any<Categoria>()).Returns(categoriaValido);
                CategoriaComando comando = new CategoriaComando();
                comando.Nome = "Empresa";
                comando.IdSistemaFinanceiro = 1;
                Categoria categoria = sut.Inserir(comando);

                categoria.Should().BeOfType<Categoria>();
                categoria.Should().Be(categoriaValido);
            }
        }

        public class EditarMetodo: CategoriasServicoTestes
        {
            [Fact]
            public void Dado_ParametrosParaEditarCategoria_Espero_CategoriaEditado()
            {
                CategoriaComando comando = new CategoriaComando();
                comando.Nome = "Empresa";
                comando.IdSistemaFinanceiro = 1;
                categoriasRepositorio.Recuperar(Arg.Any<int>()).Returns(categoriaValido);
                sistemaFinanceirosServico.Validar(Arg.Any<int>()).Returns(sistemaValido);

                Categoria categoria = sut.Editar(1, comando);

                categoria.Should().NotBeNull();
                categoriaValido.Nome.Should().Be("Empresa");
                categoriaValido.SistemaFinanceiro.Should().BeSameAs(sistemaValido);
                categoriasRepositorio.Received(1).Recuperar(1);
                sistemaFinanceirosServico.Received(1).Validar(comando.IdSistemaFinanceiro);
                categoriasRepositorio.Received(1).Editar(categoriaValido);
            }
        }

    }
}