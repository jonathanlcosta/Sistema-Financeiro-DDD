using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SistemaFinanceiros.DataTransfer.Autenticacoes.Response;
using SistemaFinanceiros.DataTransfer.Usuarios.Request;
using SistemaFinanceiros.DataTransfer.Usuarios.Response;
using SistemaFinanceiros.Dominio.Usuarios.Entidades;
using SistemaFinanceiros.Dominio.Usuarios.Repositorios.Filtros;
using SistemaFinanceiros.Dominio.Usuarios.Servicos.Comandos;

namespace SistemaFinanceiros.Aplicacao.Usuarios.Profiles
{
    public class UsuariosProfile : Profile
    {
        public UsuariosProfile()
        {
        CreateMap<Usuario, CadastroResponse>();
        CreateMap<Usuario, UsuarioResponse>();
        CreateMap<UsuarioListarRequest, UsuarioListarFiltro>();
        CreateMap<UsuarioEditarRequest, UsuarioEditarComando>();
        }
    }
}