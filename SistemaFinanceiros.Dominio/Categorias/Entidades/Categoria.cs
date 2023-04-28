using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public virtual void SetNome(string nome)
        {
            if (String.IsNullOrEmpty(nome))
                throw new ArgumentNullException("O nome não pode ser vazio");
            if (nome.Length > 100)
                throw new ArgumentOutOfRangeException("O nome nao pode ter mais que 100 caracteres");
            Nome = nome;
        }

        public virtual void SetSistema(SistemaFinanceiro sistema)
        {
            if(sistema is null)
            throw new ArgumentException("O sistema é necessário para a categoria");
            SistemaFinanceiro = sistema;
        }

    }
}