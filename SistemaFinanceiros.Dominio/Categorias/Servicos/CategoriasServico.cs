using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaFinanceiros.Dominio.Categorias.Entidades;
using SistemaFinanceiros.Dominio.Categorias.Repositorios;
using SistemaFinanceiros.Dominio.Categorias.Servicos.Interfaces;
using SistemaFinanceiros.Dominio.SistemaFinanceiros.Entidades;
using SistemaFinanceiros.Dominio.SistemaFinanceiros.Servicos.Interfaces;

namespace SistemaFinanceiros.Dominio.Categorias.Servicos
{
    public class CategoriasServico : ICategoriasServico
    {
        private readonly ICategoriasRepositorio categoriasRepositorio;
        private readonly ISistemaFinanceirosServico sistemaFinanceirosServico;
        public CategoriasServico(ICategoriasRepositorio categoriasRepositorio, ISistemaFinanceirosServico sistemaFinanceirosServico)
        {
            this.categoriasRepositorio = categoriasRepositorio;
            this.sistemaFinanceirosServico = sistemaFinanceirosServico;
        }
        public Categoria Editar(int id, string nome, int idSistemaFinanceiro)
        {
            var sistemaFinanceiro = sistemaFinanceirosServico.Validar(idSistemaFinanceiro);
            var categoria = Validar(id);
            categoria.SetNome(nome);
            categoria.SetSistema(sistemaFinanceiro);
            categoria = categoriasRepositorio.Editar(categoria);
            return categoria;
        }

        public Categoria Inserir(Categoria categoria)
        {
           var response = categoriasRepositorio.Inserir(categoria);
           return response;
        }

        public Categoria Instanciar(string nome, int idSistemaFinanceiro)
        {
           SistemaFinanceiro sistemaFinanceiro = sistemaFinanceirosServico.Validar(idSistemaFinanceiro);
           var categoria = new Categoria(nome, sistemaFinanceiro);
           return categoria;
        }

        public Categoria Validar(int id)
        {
            var categoriaResponse = categoriasRepositorio.Recuperar(id);
            if(categoriaResponse is null){
                throw new ArgumentException("Categoria não encontrada");
            }
           return categoriaResponse;

        }
    }
}