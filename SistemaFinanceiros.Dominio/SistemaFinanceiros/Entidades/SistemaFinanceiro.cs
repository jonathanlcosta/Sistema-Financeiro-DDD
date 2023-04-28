using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaFinanceiros.Dominio.Usuarios.Entidades;

namespace SistemaFinanceiros.Dominio.SistemaFinanceiros.Entidades
{
    public class SistemaFinanceiro
    {
        public virtual int Id { get; protected set; }
        public virtual string Nome { get; protected set; }
        public virtual int Mes { get; protected set; }
        public virtual int Ano { get; protected set; }
        public virtual Usuario Usuario { get; set; }
        public virtual int DiaFechamento { get; protected set; }
        public virtual bool GerarCopiaDespesa { get; protected set; }
        public virtual int MesCopia { get; protected set; }
        public virtual int AnoCopia { get; protected set; }

        public SistemaFinanceiro(string nome, int mes, int ano, int diafechamento, bool gerarCopiaDespesa, int mesCopia, int anoCopia)
        {
            SetNome(nome);
            SetAno(ano);
            SetDiaFechamento(diafechamento);
            SetGerarCopiaDespesa(gerarCopiaDespesa);
            SetMes(mes);
            SetMesCopia(mesCopia);
            SetAnoCopia(anoCopia);
        }

        protected SistemaFinanceiro()
        {
            
        }
        public virtual void SetNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new ArgumentException("O nome é obrigatorio");
            }
            Nome = nome;
        }

        public virtual void SetAno(int ano)
        {
            var data = DateTime.Now;
            ano = data.Year;
            this.Ano = ano;
        }

        public virtual void SetDiaFechamento(int diafechamento)
        {
            diafechamento = 1;
            this.DiaFechamento = diafechamento;
        }

        public virtual void SetMes(int mes)
        {
            var data = DateTime.Now;
            mes = data.Month;
            Mes = mes;
        }

        public virtual void SetAnoCopia(int anoCopia)
        {
            var data = DateTime.Now;
            anoCopia = data.Year;
            AnoCopia = anoCopia;
        }

        public virtual void SetMesCopia(int mesCopia)
        {
            var data = DateTime.Now;
            mesCopia = data.Month;
            MesCopia = mesCopia;
        }

        public virtual void SetGerarCopiaDespesa(bool gerarCopiaDespesa)
        {
            gerarCopiaDespesa = true;
            GerarCopiaDespesa = gerarCopiaDespesa;
        }
    }
}