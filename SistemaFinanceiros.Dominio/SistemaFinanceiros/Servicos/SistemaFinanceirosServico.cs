using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaFinanceiros.Dominio.SistemaFinanceiros.Entidades;
using SistemaFinanceiros.Dominio.SistemaFinanceiros.Repositorios;
using SistemaFinanceiros.Dominio.SistemaFinanceiros.Servicos.Comandos;
using SistemaFinanceiros.Dominio.SistemaFinanceiros.Servicos.Interfaces;

namespace SistemaFinanceiros.Dominio.SistemaFinanceiros.Servicos
{
    public class SistemaFinanceirosServico : ISistemaFinanceirosServico
    {
        private readonly ISistemaFinanceirosRepositorio sistemaFinanceirosRepositorio;
        public SistemaFinanceirosServico(ISistemaFinanceirosRepositorio sistemaFinanceirosRepositorio)
        {
            this.sistemaFinanceirosRepositorio = sistemaFinanceirosRepositorio;
        }
        public SistemaFinanceiro Editar(int id, SistemaFinanceiroComando comando)
        {
            SistemaFinanceiro sistemaFinanceiro = Validar(id);
            sistemaFinanceiro.SetNome(comando.Nome);
            sistemaFinanceiro = sistemaFinanceirosRepositorio.Editar(sistemaFinanceiro);
            return sistemaFinanceiro;
        }

        public SistemaFinanceiro Inserir(SistemaFinanceiroComando comando)
        {
        SistemaFinanceiro sistemaFinanceiro = new SistemaFinanceiro(comando.Nome);
        var sistemaFinanceiroResponse = sistemaFinanceirosRepositorio.Inserir(sistemaFinanceiro);
        return sistemaFinanceiroResponse;
        }

        public SistemaFinanceiro Validar(int id)
        {
            var sistemaFinanceiroResponse = this.sistemaFinanceirosRepositorio.Recuperar(id);
            if(sistemaFinanceiroResponse is null)
            {
                 throw new ArgumentException("Sistema Financeiro não encontrado");
            }
            return sistemaFinanceiroResponse;
        }
    }
}