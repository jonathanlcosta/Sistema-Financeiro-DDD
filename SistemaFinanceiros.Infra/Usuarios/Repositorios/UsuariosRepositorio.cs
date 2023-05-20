using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NHibernate;
using SistemaFinanceiros.Dominio.Usuarios.Entidades;
using SistemaFinanceiros.Dominio.Usuarios.Repositorios;
using SistemaFinanceiros.Dominio.Usuarios.Repositorios.Filtros;
using SistemaFinanceiros.Infra.Genericos;

namespace SistemaFinanceiros.Infra.Usuarios
{
    public class UsuariosRepositorio : GenericoRepositorio<Usuario>, IUsuariosRepositorio
    {
        public UsuariosRepositorio(ISession session) : base (session)
        {
            
        }

        public IQueryable<Usuario> Filtrar(UsuarioListarFiltro filtro)
        {
             IQueryable<Usuario> query = Query();

            if (!string.IsNullOrEmpty(filtro.Nome))
            {
                 query = query.Where(d => d.Nome.Contains(filtro.Nome));
            }

            if (!string.IsNullOrEmpty(filtro.CPF))
            {
                 query = query.Where(d => d.CPF.Contains(filtro.CPF));
            }

            if (!string.IsNullOrEmpty(filtro.Email))
            {
                 query = query.Where(d => d.Email.Contains(filtro.Email));
            }
            
            return query;
        }

        public Usuario RecuperaUsuarioPorEmail(string email)
        {
            Usuario usuario =  session.Query<Usuario>().Where(usuario => usuario.Email == email).FirstOrDefault();
            return usuario;
        }
    }
}