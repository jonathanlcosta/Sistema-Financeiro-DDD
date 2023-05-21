using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FizzWare.NBuilder;
using FluentAssertions;
using SistemaFinanceiros.Dominio.Categorias.Entidades;
using SistemaFinanceiros.Dominio.Despesas.Entidades;
using SistemaFinanceiros.Dominio.Despesas.Enumeradores;
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
    }
}