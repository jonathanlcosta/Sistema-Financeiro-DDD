using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FizzWare.NBuilder;
using FluentAssertions;
using SistemaFinanceiros.Dominio.Categorias.Entidades;
using SistemaFinanceiros.Dominio.Despesas.Entidades;
using SistemaFinanceiros.Dominio.Despesas.Enumeradores;
using SistemaFinanceiros.Dominio.Execoes;
using SistemaFinanceiros.Dominio.Usuarios.Entidades;
using Xunit;

namespace SistemaFinanceiros.Dominio.Testes.Despesas.Entidades
{
    public class DespesaTestes
    {
        private readonly Despesa sut;
        public DespesaTestes()
        {
             sut = Builder<Despesa>.CreateNew().Build();
        }

        public class Construtor
        {
            [Fact]
            public void Quando_ParametrosForemValidos_Espero_ObjetoIntegro()
            {
                DateTime dataVencimento = DateTime.UtcNow;
                Usuario usuario = Builder<Usuario>.CreateNew().Build();
                Categoria categoria = Builder<Categoria>.CreateNew().Build();
                Despesa despesa = new Despesa("Despesa Teste", 1000, EnumTipoDespesa.Contas, dataVencimento,
                 false, true, categoria, usuario);

                despesa.Nome.Should().Be("Despesa Teste");
                despesa.Categoria.Should().Be(categoria);
                despesa.Valor.Should().Be(1000);
                despesa.TipoDespesa.Should().Be(EnumTipoDespesa.Contas);
                despesa.DataVencimento.Should().Be(dataVencimento);
                despesa.Pago.Should().Be(false);
                despesa.DespesaAtrasada.Should().Be(true);
                despesa.Usuario.Should().Be(usuario);

            }
        }

        public class SetNomeMetodo : DespesaTestes
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
            public void Quando_NomeForMaiorQue100Caracteres_Espero_TamanhoDeAtributoExcecao()
            {
                sut.Invoking(x => x.SetNome(new string('*', 101))).Should().Throw<TamanhoDeAtributoInvalidoExcecao>();
            }

            [Fact]
            public void Quando_NomeForValido_Espero_PropriedadePreenchida()
            {
                sut.SetNome("Empresa");
                sut.Nome.Should().Be("Empresa");
            }
    
        }

        public class SetValorMetodo : DespesaTestes
        {
            [Theory]
            [InlineData(-1)]
            [InlineData(0)]
            public void Quando_ValorForMenorQueZeroOuIgualAZero_Espero_AtributoObrigatorioExcecao(decimal valor)
            {
                 sut.Invoking(x => x.SetValor(valor)).Should().Throw<AtributoObrigatorioExcecao>();
            }

            [Fact]
            public void Quando_ValorForValido_Espero_PropriedadePreenchida()
            {
                sut.SetValor(2);
                sut.Valor.Should().Be(2);
            }
        }

         public class SetCategoriaMetodo : DespesaTestes
        {
            [Fact]
            public void Quando_CategoriaForNulo_Espero_AtributoObrigatorioExcecao()
            {
                sut.Invoking(x => x.SetCategoria(null)).Should().Throw<AtributoObrigatorioExcecao>();
            }

            [Fact]
            public void Quando_CategoriaForValido_Espero_PropriedadePreenchida()
            {
                 Categoria categoria = Builder<Categoria>.CreateNew().Build();
                sut.SetCategoria(categoria);
                sut.Categoria.Should().BeSameAs(categoria);
            }
        }

        public class SetUsuarioMetodo : DespesaTestes
        {
            [Fact]
            public void Quando_UsuarioForNulo_Espero_AtributoObrigatorioExcecao()
            {
                sut.Invoking(x => x.SetUsuario(null)).Should().Throw<AtributoObrigatorioExcecao>();
            }

            [Fact]
            public void Quando_UsuarioForValido_Espero_PropriedadePreenchida()
            {
                 Usuario usuario = Builder<Usuario>.CreateNew().Build();
                sut.SetUsuario(usuario);
                sut.Usuario.Should().BeSameAs(usuario);
            }
        }

        public class SetDataMetodo: DespesaTestes
        {
            [Fact]
            public void Quando_DataEstiverComMinimoValor_Espero_AtributoObrigatorioExcecao()
            {
                 DateTime data = DateTime.MinValue;
                sut.Invoking(x => x.SetDataVencimento(data)).Should().Throw<AtributoObrigatorioExcecao>();
            }

            [Fact]
            public void Quando_DataForValida_Espero_PropriedadePreenchida()
            {
                DateTime data = DateTime.Now;
                sut.SetDataVencimento(data);
                sut.DataVencimento.Should().Be(data);
            }
        }

        public class SetPagoMetodo: DespesaTestes
        {
            [Fact]
            public void Quando_PropriedadePagoEstiverVerdadeiro_Espero_PropriedadePagoPreenchidaEDataPagamentoPreenchidaComADataDeHoje()
            {   
                DateTime data = DateTime.UtcNow;
                bool pago = true;
                sut.SetPago(pago);
                sut.Pago.Should().Be(pago);
                sut.DataPagamento.Date.Should().Be(data.Date);

            }
        }

        public class SetTipoDespesaMetodo: DespesaTestes
        {
            [Fact]
            public void Dado_TipoDespesaValido_Espero_PropriedadePreenchida()
            {
                EnumTipoDespesa tipoDespesa = EnumTipoDespesa.Investimento;
                sut.SetTipoDespesa(tipoDespesa);
                sut.TipoDespesa.Should().Be(tipoDespesa);
            }
        }

        public class SetDespesaAtrasada: DespesaTestes
        {
            [Fact]
            public void Dado_DespesaAtrasadaValido_Espero_PropriedadePreenchida()
            {
                bool despesaAtrasada = true;
                sut.SetDespesaAtrasada(despesaAtrasada);
                sut.DespesaAtrasada.Should().Be(despesaAtrasada);
            }
        }
    }
}