using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaFinanceiros.Dominio.Categorias.Entidades;
using SistemaFinanceiros.Dominio.Categorias.Servicos.Interfaces;
using SistemaFinanceiros.Dominio.Despesas.Entidades;
using SistemaFinanceiros.Dominio.Despesas.Enumeradores;
using SistemaFinanceiros.Dominio.Despesas.Repositorios;
using SistemaFinanceiros.Dominio.Despesas.Servicos.Comandos;
using SistemaFinanceiros.Dominio.Despesas.Servicos.Interfaces;
using SistemaFinanceiros.Dominio.Usuarios.Entidades;
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

        public object CarregaGraficos(string email)
        {
            var despesasUsuario =  despesasRepositorio.ListarDespesasUsuario(email);
            var despesasAnterior =  despesasRepositorio.ListarDespesasUsuarioNaoPagasMesesAnterior(email);

            var despesas_naoPagasMesesAnteriores = despesasAnterior.Any() ?
                despesasAnterior.ToList().Sum(x => x.Valor) : 0;

            var despesas_pagas = despesasUsuario.Where(d => d.Pago && d.TipoDespesa == EnumTipoDespesa.Contas)
                .Sum(x => x.Valor);

            var despesas_pendentes = despesasUsuario.Where(d => !d.Pago && d.TipoDespesa == EnumTipoDespesa.Contas)
                .Sum(x => x.Valor);

            var investimentos = despesasUsuario.Where(d => d.TipoDespesa == EnumTipoDespesa.Investimento)
                .Sum(x => x.Valor);

            return new
            {
                sucesso = "OK",
                despesas_pagas = despesas_pagas,
                despesas_pendentes = despesas_pendentes,
                despesas_naoPagasMesesAnteriores = despesas_naoPagasMesesAnteriores,
                investimentos = investimentos
            };
        }

        public Despesa Editar(int id, DespesaComando comando)
        {
            Categoria categoria = categoriasServico.Validar(comando.IdCategoria);
            Despesa despesa = Validar(id);
            despesa.SetNome(comando.Nome);
            despesa.SetValor(comando.Valor);
            despesa.SetTipoDespesa(comando.TipoDespesa);
            despesa.SetDataVencimento(comando.DataVencimento);
            despesa.SetPago(comando.Pago);
            despesa.SetDespesaAtrasada(comando.DespesaAtrasada);
            despesa.SetCategoria(categoria);
            despesa = despesasRepositorio.Editar(despesa);
            return despesa;

        }

        public Despesa Inserir(DespesaComando comando)
        {
           Despesa despesa = Instanciar(comando);
            var response = despesasRepositorio.Inserir(despesa);
            return response;
        }

        public Despesa Instanciar(DespesaComando comando)
        {
            Categoria categoria = categoriasServico.Validar(comando.IdCategoria);
            Usuario usuario = usuariosServico.Validar(comando.IdUsuario);
            Despesa despesa = new(comando.Nome, comando.Valor, comando.TipoDespesa, comando.DataVencimento, comando.Pago, comando.DespesaAtrasada,
            categoria, usuario);

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