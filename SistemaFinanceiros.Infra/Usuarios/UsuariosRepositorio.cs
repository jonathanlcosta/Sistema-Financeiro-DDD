using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NHibernate;
using SistemaFinanceiros.Dominio.Usuarios.Entidades;
using SistemaFinanceiros.Dominio.Usuarios.Repositorios;
using SistemaFinanceiros.Infra.Genericos;

namespace SistemaFinanceiros.Infra.Usuarios
{
    public class UsuariosRepositorio : GenericoRepositorio<Usuario>, IUsuariosRepositorio
    {
        public UsuariosRepositorio(ISession session) : base (session)
        {
            
        }

        public Usuario RecuperaUsuarioPorEmail(string email)
        {
            Usuario usuario =  session.Query<Usuario>().Where(usuario => usuario.Email == email).FirstOrDefault();
            return usuario;
        }
    }
}