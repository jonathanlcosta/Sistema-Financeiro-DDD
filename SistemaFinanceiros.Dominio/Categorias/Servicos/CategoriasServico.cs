using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaFinanceiros.Dominio.Categorias.Entidades;
using SistemaFinanceiros.Dominio.Categorias.Repositorios;
using SistemaFinanceiros.Dominio.Categorias.Servicos.Comandos;
using SistemaFinanceiros.Dominio.Categorias.Servicos.Interfaces;
using SistemaFinanceiros.Dominio.Execoes;
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
        public Categoria Editar(int id, CategoriaComando comando)
        {
            SistemaFinanceiro sistemaFinanceiro = sistemaFinanceirosServico.Validar(comando.IdSistemaFinanceiro);
            Categoria categoria = Validar(id);
            categoria.SetNome(comando.Nome);
            categoria.SetSistema(sistemaFinanceiro);
            categoriasRepositorio.Editar(categoria);
            return categoria;
        }

        public Categoria Inserir(CategoriaComando comando)
        {
        Categoria categoria = Instanciar(comando);
        categoriasRepositorio.Inserir(categoria);
           return categoria;
        }

         public Categoria Instanciar(CategoriaComando comando)
        {
            SistemaFinanceiro sistemaFinanceiro = sistemaFinanceirosServico.Validar(comando.IdSistemaFinanceiro);
            Categoria categoria = new(comando.Nome, sistemaFinanceiro);
            return categoria;
        }

        public Categoria Validar(int id)
        {
            var categoriaResponse = categoriasRepositorio.Recuperar(id);
            if(categoriaResponse is null){
                throw new RegraDeNegocioExcecao("Categoria não encontrada");
            }
           return categoriaResponse;

        }
    }
}