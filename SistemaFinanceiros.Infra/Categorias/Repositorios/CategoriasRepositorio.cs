using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NHibernate;
using SistemaFinanceiros.Dominio.Categorias.Entidades;
using SistemaFinanceiros.Dominio.Categorias.Repositorios;
using SistemaFinanceiros.Dominio.Categorias.Repositorios.Filtros;
using SistemaFinanceiros.Infra.Genericos;

namespace SistemaFinanceiros.Infra.Categorias
{
    public class CategoriasRepositorio : GenericoRepositorio<Categoria>, ICategoriasRepositorio
    {
        public CategoriasRepositorio(ISession session) : base (session)
        {
            
        }

        public IQueryable<Categoria> Filtrar(CategoriaListarFiltro filtro)
        {
            IQueryable<Categoria> query = Query();

            if (!string.IsNullOrEmpty(filtro.Nome))
            {
                 query = query.Where(p => p.Nome.Contains(filtro.Nome));
            }
   

            if (!string.IsNullOrEmpty(filtro.NomeSistema))
            {
                query = query.Where(p => p.SistemaFinanceiro.Nome == filtro.NomeSistema);
            }

            return query;
        }
    }
}