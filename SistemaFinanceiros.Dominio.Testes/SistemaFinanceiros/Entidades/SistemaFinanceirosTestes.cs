using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FizzWare.NBuilder;
using FluentAssertions;
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
    }
}