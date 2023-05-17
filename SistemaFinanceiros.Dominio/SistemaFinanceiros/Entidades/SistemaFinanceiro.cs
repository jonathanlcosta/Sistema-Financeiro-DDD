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

        public SistemaFinanceiro(string nome)
        {
            SetNome(nome);
            SetAno();
            SetDiaFechamento();
            SetGerarCopiaDespesa();
            SetMes();
            SetMesCopia();
            SetAnoCopia();
        }

        public SistemaFinanceiro()
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

        public virtual void SetAno()
        {
            var data = DateTime.Now;
            var ano = data.Year;
            this.Ano = ano;
        }

        public virtual void SetDiaFechamento()
        {
            var diafechamento = 1;
            DiaFechamento = diafechamento;
        }

        public virtual void SetMes()
        {
            var data = DateTime.Now;
            var mes = data.Month;
            Mes = mes;
        }

        public virtual void SetAnoCopia()
        {
            var data = DateTime.Now;
            var anoCopia = data.Year;
            AnoCopia = anoCopia;
        }

        public virtual void SetMesCopia()
        {
            var data = DateTime.Now;
            var mesCopia = data.Month;
            MesCopia = mesCopia;
        }

        public virtual void SetGerarCopiaDespesa()
        {
            var gerarCopiaDespesa = true;
            GerarCopiaDespesa = gerarCopiaDespesa;
        }
    }
}