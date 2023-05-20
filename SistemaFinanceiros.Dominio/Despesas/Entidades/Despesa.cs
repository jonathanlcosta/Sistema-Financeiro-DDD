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

        public Despesa(string nome, decimal valor, EnumTipoDespesa tipoDespesa,
        DateTime dataVencimento, bool pago, bool despesaAtrasada,
        Categoria categoria, Usuario usuario)
        {
            SetNome(nome);
            SetValor(valor);
            SetMes();
            SetAno();
            SetTipoDespesa(tipoDespesa);
            SetDataCadastro();
            SetDataAlteracao();
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
                throw new ArgumentException("O nome não pode ser vazio");
            if (nome.Length > 100)
                throw new ArgumentException("O nome nao pode ter mais que 100 caracteres");
            Nome = nome;
        }

        public virtual void SetUsuario(Usuario usuario)
        {
            if(usuario is null)
            throw new ArgumentException("O usuario é obrigatório");
            Usuario = usuario;
        }

        public virtual void SetValor(decimal valor)
        {
            if(valor <= 0)
            throw new ArgumentException("O valor não pode ser igual ou menor que zero");
            Valor = valor;
        }

        public virtual void SetMes()
        {
             var data = DateTime.UtcNow;
             var mes = data.Month;
            Mes = mes;
        }

        public virtual void SetAno()
        {   
            var data = DateTime.UtcNow;
            var ano = data.Year;
            Ano = ano;
        }

        public virtual void SetTipoDespesa(EnumTipoDespesa tipoDespesa)
        {
            TipoDespesa = tipoDespesa;
        }

         public virtual void SetDataCadastro()
        {
            var data = DateTime.UtcNow;
            DataCadastro = data;
        }

        public virtual void SetDataAlteracao()
        {
             var data = DateTime.UtcNow;
            DataAlteracao = data;
        }

        public virtual void SetDataVencimento(DateTime data)
        {
            if (data == DateTime.MinValue)
            {
                throw new ArgumentException("A data não foi informada.");
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