using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaFinanceiros.Dominio.Categorias.Entidades;
using SistemaFinanceiros.Dominio.Despesas.Enumeradores;
using SistemaFinanceiros.Dominio.Execoes;
using SistemaFinanceiros.Dominio.Usuarios.Entidades;

namespace SistemaFinanceiros.Dominio.Despesas.Entidades
{
    public class Despesa
    {
        public virtual int Id { get; protected set; }
        public virtual string Nome { get; protected set; }
        public virtual decimal Valor { get; protected set; }
        public virtual int Mes { get; protected set; }
        public virtual int Ano { get; protected set; }
        public virtual Usuario Usuario { get; protected set; }
        public virtual EnumTipoDespesa TipoDespesa { get; protected set; }
        public virtual DateTime DataCadastro { get; protected set; }
        public virtual DateTime DataAlteracao { get; protected set; }
        public virtual DateTime DataPagamento { get; protected set; }
        public virtual DateTime DataVencimento { get; protected set; }
        public virtual bool Pago { get; protected set; }
        public virtual bool DespesaAtrasada { get; protected set; }
        public virtual Categoria Categoria { get; protected set; }

        public Despesa(string nome, decimal valor, EnumTipoDespesa tipoDespesa,
        DateTime dataVencimento, bool pago, bool despesaAtrasada,
        Categoria categoria, Usuario usuario)
        {
            SetNome(nome);
            SetValor(valor);
            Mes = DateTime.UtcNow.Month;
            Ano = DateTime.UtcNow.Year;
            SetTipoDespesa(tipoDespesa);
            DataCadastro = DateTime.UtcNow;
            DataAlteracao = DateTime.UtcNow;
            SetDataVencimento(dataVencimento);
            SetPago(pago);
            SetDespesaAtrasada(despesaAtrasada);
            SetCategoria(categoria);
            SetUsuario(usuario);
        }

        protected Despesa(){}

       public virtual void SetNome(string nome)
        {
            if (string.IsNullOrEmpty(nome))
                throw new AtributoObrigatorioExcecao("Nome");
            if (nome.Length > 100)
                throw new TamanhoDeAtributoInvalidoExcecao("Nome");
            Nome = nome;
        }

        public virtual void SetUsuario(Usuario usuario)
        {
           Usuario = usuario ?? throw new AtributoObrigatorioExcecao("Usuario");
        }

        public virtual void SetValor(decimal valor)
        {
            if(valor <= 0)
            throw new AtributoObrigatorioExcecao("Valor");
            Valor = valor;
        }

        public virtual void SetTipoDespesa(EnumTipoDespesa tipoDespesa)
        {
            TipoDespesa = tipoDespesa;
        }

        public virtual void SetDataVencimento(DateTime data)
        {
            if (data == DateTime.MinValue)
            {
                throw new AtributoObrigatorioExcecao("Data");
            }
            DataVencimento = data;
        }

        public virtual void SetPago(bool pago)
        {
            var data = DateTime.UtcNow;
            Pago = pago;
            if(pago == true){
                DataPagamento = data;
            }
        }

        public virtual void SetDespesaAtrasada(bool despesaAtrasada)
        {
            DespesaAtrasada = despesaAtrasada;
        }

        public virtual void SetCategoria(Categoria categoria)
        {
             Categoria = categoria ?? throw new AtributoObrigatorioExcecao("Categoria");
        }
    }
}