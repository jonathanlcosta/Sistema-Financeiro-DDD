using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NHibernate;
using SistemaFinanceiros.Dominio.Categorias.Entidades;
using SistemaFinanceiros.Dominio.Categorias.Repositorios;
using SistemaFinanceiros.Infra.Genericos;

namespace SistemaFinanceiros.Infra.Categorias
{
    public class CategoriasRepositorio : GenericoRepositorio<Categoria>, ICategoriasRepositorio
    {
        public CategoriasRepositorio(ISession session) : base (session)
        {
            
        }
    }
}