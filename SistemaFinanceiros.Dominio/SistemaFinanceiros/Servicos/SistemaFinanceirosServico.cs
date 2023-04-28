using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaFinanceiros.Dominio.SistemaFinanceiros.Entidades;
using SistemaFinanceiros.Dominio.SistemaFinanceiros.Repositorios;
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
        public SistemaFinanceiro Editar(int id, string nome, int mes, int ano, int diafechamento, bool gerarCopiaDespesa, int mesCopia, int anoCopia)
        {
            var sistemaFinanceiro = Validar(id);
             if(!string.IsNullOrWhiteSpace(nome) && sistemaFinanceiro.Nome != nome) sistemaFinanceiro.SetNome(nome);
            sistemaFinanceiro.SetAno(ano);
            sistemaFinanceiro.SetAnoCopia(anoCopia);
            sistemaFinanceiro.SetDiaFechamento(diafechamento);
            sistemaFinanceiro.SetGerarCopiaDespesa(gerarCopiaDespesa);
            sistemaFinanceiro.SetMes(mes);
            sistemaFinanceiro.SetMesCopia(mesCopia);
            
            sistemaFinanceiro = sistemaFinanceirosRepositorio.Editar(sistemaFinanceiro);
            return sistemaFinanceiro;
        }

        public SistemaFinanceiro Inserir(SistemaFinanceiro sistemaFinanceiro)
        {
        var sistemaFinanceiroResponse = sistemaFinanceirosRepositorio.Inserir(sistemaFinanceiro);
        return sistemaFinanceiroResponse;
        }

        public SistemaFinanceiro Instanciar(string nome, int mes, int ano, int diafechamento, bool gerarCopiaDespesa, int mesCopia, int anoCopia)
        {
            var sistemaFinanceiroResponse = new SistemaFinanceiro(nome, mes, ano, diafechamento, gerarCopiaDespesa, mesCopia, anoCopia);
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