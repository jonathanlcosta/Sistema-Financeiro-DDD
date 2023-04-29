using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaFinanceiros.Dominio.Categorias.Entidades;
using SistemaFinanceiros.Dominio.Despesas.Enumeradores;
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

        public Despesa(string nome, decimal valor, int mes, int ano, EnumTipoDespesa tipoDespesa, DateTime dataCadastro,
        DateTime dataAlteracao, DateTime dataVencimento, bool pago, bool despesaAtrasada,
        Categoria categoria, Usuario usuario)
        {
            SetNome(nome);
            SetValor(valor);
            SetMes(mes);
            SetAno(ano);
            SetTipoDespesa(tipoDespesa);
            SetDataCadastro(dataCadastro);
            SetDataAlteracao(dataAlteracao);
            SetDataVencimento(dataVencimento);
            SetPago(pago);
            SetDespesaAtrasada(despesaAtrasada);
            SetCategoria(categoria);
            SetUsuario(usuario);
        }

        protected Despesa(){}

       public virtual void SetNome(string nome)
        {
            if (String.IsNullOrEmpty(nome))
                throw new ArgumentNullException("O nome não pode ser vazio");
            if (nome.Length > 100)
                throw new ArgumentOutOfRangeException("O nome nao pode ter mais que 100 caracteres");
            Nome = nome;
        }

        public virtual void SetUsuario(Usuario usuario)
        {
            Usuario = usuario;
        }

        public virtual void SetValor(decimal valor)
        {
            if(valor <= 0)
            throw new ArgumentException("O valor não pode ser igual ou menor que zero");
            Valor = valor;
        }

        public virtual void SetMes(int mes)
        {
             var data = DateTime.UtcNow;
             mes = data.Month;
            Mes = mes;
        }

        public virtual void SetAno(int ano)
        {   
            var data = DateTime.UtcNow;
            ano = data.Year;
            Ano = ano;
        }

        public virtual void SetTipoDespesa(EnumTipoDespesa tipoDespesa)
        {
            TipoDespesa = tipoDespesa;
        }

         public virtual void SetDataCadastro(DateTime dataCadastro)
        {
            var data = DateTime.UtcNow;
            dataCadastro = data;
            DataCadastro = dataCadastro;
        }

        public virtual void SetDataAlteracao(DateTime dataAlteracao)
        {
             var data = DateTime.UtcNow;
            dataAlteracao = data;
            DataAlteracao = data;
        }

        public virtual void SetDataVencimento(DateTime data)
        {
            if (data == DateTime.MinValue)
            {
                throw new ArgumentNullException("A data não foi informada.");
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
            if(categoria is null)
            throw new ArgumentException("A categoria precisa ser informada");
            Categoria = categoria;
        }
    }
}