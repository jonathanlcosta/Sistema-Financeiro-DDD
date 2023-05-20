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

namespace SistemaFinanceiros.Aplicacao.Usuarios.Profiles
{
    public class UsuariosProfile : Profile
    {
        public UsuariosProfile()
        {
        CreateMap<Usuario, CadastroResponse>();
        CreateMap<Usuario, UsuarioResponse>()
        .ForMember(x => x.idSistemaFinanceiro, m => m.MapFrom(y => y.SistemaFinanceiro!.Id));
        CreateMap<UsuarioListarRequest, UsuarioListarFiltro>();
        }
    }
}