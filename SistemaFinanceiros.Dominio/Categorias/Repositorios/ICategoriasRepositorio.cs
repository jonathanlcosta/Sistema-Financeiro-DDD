using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaFinanceiros.Dominio.Categorias.Entidades;
using SistemaFinanceiros.Dominio.Categorias.Repositorios.Filtros;
using SistemaFinanceiros.Dominio.Genericos;

namespace SistemaFinanceiros.Dominio.Categorias.Repositorios
{
    public interface ICategoriasRepositorio : IGenericoRepositorio<Categoria>
    {
         IQueryable<Categoria> Filtrar(CategoriaListarFiltro filtro);
         IList<Categoria> ListarNomesCategoria();
    }
}