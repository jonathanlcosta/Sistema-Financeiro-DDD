using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaFinanceiros.Dominio.Genericos;
using SistemaFinanceiros.Dominio.Usuarios.Entidades;
using SistemaFinanceiros.Dominio.Usuarios.Repositorios.Filtros;

namespace SistemaFinanceiros.Dominio.Usuarios.Repositorios
{
    public interface IUsuariosRepositorio : IGenericoRepositorio<Usuario>
    {
        Usuario RecuperaUsuarioPorEmail(string email);
        IQueryable<Usuario> Filtrar(UsuarioListarFiltro filtro);

    }
}