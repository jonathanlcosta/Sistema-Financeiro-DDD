using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaFinanceiros.Dominio.Execoes;
using SistemaFinanceiros.Dominio.Usuarios.Entidades;

namespace SistemaFinanceiros.Dominio.SistemaFinanceiros.Entidades
{
    public class SistemaFinanceiro
    {
        public virtual int Id { get; protected set; }
        public virtual string Nome { get; protected set; }
        public virtual int Mes { get; protected set; }
        public virtual int Ano { get; protected set; }
        public virtual int DiaFechamento { get; protected set; }
        public virtual bool GerarCopiaDespesa { get; protected set; }
        public virtual int MesCopia { get; protected set; }
        public virtual int AnoCopia { get; protected set; }

        public SistemaFinanceiro(string nome)
        {
            SetNome(nome);
            Ano = DateTime.Now.Year;
            DiaFechamento = 1;
            GerarCopiaDespesa = true;
            Mes = DateTime.Now.Month;
            MesCopia = DateTime.Now.Month;
            AnoCopia = DateTime.Now.Year;
        }

        public SistemaFinanceiro()
        {
            
        }

        public virtual void SetNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new AtributoObrigatorioExcecao("Nome");
            }
            Nome = nome;
        }
    }
}