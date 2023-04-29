using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaFinanceiros.Dominio.Categorias.Servicos.Interfaces;
using SistemaFinanceiros.Dominio.Despesas.Entidades;
using SistemaFinanceiros.Dominio.Despesas.Enumeradores;
using SistemaFinanceiros.Dominio.Despesas.Repositorios;
using SistemaFinanceiros.Dominio.Despesas.Servicos.Interfaces;
using SistemaFinanceiros.Dominio.Usuarios.Servicos.Interfaces;

namespace SistemaFinanceiros.Dominio.Despesas.Servicos
{
    public class DespesasServico : IDespesasServico
    {
        private readonly IDespesasRepositorio despesasRepositorio;
        private readonly ICategoriasServico categoriasServico;
        private readonly IUsuariosServico usuariosServico;
        public DespesasServico(IDespesasRepositorio despesasRepositorio,ICategoriasServico categoriasServico,
        IUsuariosServico usuariosServico )
        {
            this.despesasRepositorio = despesasRepositorio;
            this.categoriasServico = categoriasServico;
            this.usuariosServico = usuariosServico;

        }

        public Despesa Editar(int id, string nome, decimal valor, int mes, int ano, EnumTipoDespesa tipoDespesa, DateTime dataCadastro, DateTime dataAlteracao, DateTime dataVencimento, bool pago, bool despesaAtrasada, int idCategoria)
        {
            var categoria = categoriasServico.Validar(idCategoria);
            var despesa = Validar(id);
            if(!string.IsNullOrWhiteSpace(nome) && despesa.Nome != nome) despesa.SetNome(nome);
            despesa.SetValor(valor);
            despesa.SetMes(mes);
            despesa.SetAno(ano);
            despesa.SetTipoDespesa(tipoDespesa);
            despesa.SetDataCadastro(dataCadastro);
            despesa.SetDataAlteracao(dataAlteracao);
            despesa.SetDataVencimento(dataVencimento);
            despesa.SetPago(pago);
            despesa.SetDespesaAtrasada(despesaAtrasada);
            despesa.SetCategoria(categoria);
            despesa = despesasRepositorio.Editar(despesa);
            return despesa;

        }

        public Despesa Inserir(Despesa despesa)
        {
            var response = despesasRepositorio.Inserir(despesa);
            return response;
        }

        public Despesa Instanciar(string nome, decimal valor, int mes, int ano, EnumTipoDespesa tipoDespesa, DateTime dataCadastro, DateTime dataAlteracao, DateTime dataVencimento, bool pago, bool despesaAtrasada, int idCategoria, int IdUsuario)
        {   var categoria = categoriasServico.Validar(idCategoria);
            var usuario = usuariosServico.Validar(IdUsuario);
            var despesa = new Despesa(nome, valor, mes, ano, tipoDespesa, dataCadastro, dataAlteracao, dataVencimento, pago, despesaAtrasada, categoria, usuario);
            return despesa;
        }

        public Despesa Validar(int id)
        {
            var despesaResponse = this.despesasRepositorio.Recuperar(id);
            if(despesaResponse is null)
            {
                 throw new ArgumentException("Despesa não encontrada");
            }
            return despesaResponse;
        }
    }
}