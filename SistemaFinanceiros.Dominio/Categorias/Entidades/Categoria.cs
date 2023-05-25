using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaFinanceiros.Dominio.Execoes;
using SistemaFinanceiros.Dominio.SistemaFinanceiros.Entidades;

namespace SistemaFinanceiros.Dominio.Categorias.Entidades
{
    public class Categoria
    {
        public virtual int Id { get; protected set; }
        public virtual string Nome { get; protected set; }
        public virtual SistemaFinanceiro SistemaFinanceiro { get; protected set; }

        public Categoria(string nome, SistemaFinanceiro sistemaFinanceiro)
        {
            SetNome(nome);
            SetSistema(sistemaFinanceiro);
        }

        protected Categoria(){}

        public Categoria(string nome)
        {
            SetNome(nome);
        }

        public virtual void SetNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new AtributoObrigatorioExcecao("Nome");
            if (nome.Length > 100)
                throw new TamanhoDeAtributoInvalidoExcecao("Nome");
            Nome = nome;
        }

        public virtual void SetSistema(SistemaFinanceiro sistema)
        {
           SistemaFinanceiro = sistema ?? throw new AtributoObrigatorioExcecao("Sistema Financeiro");
        }

    }
}