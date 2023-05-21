using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SistemaFinanceiros.DataTransfer.Categorias.Request;
using SistemaFinanceiros.DataTransfer.Categorias.Response;
using SistemaFinanceiros.Dominio.Categorias.Entidades;
using SistemaFinanceiros.Dominio.Categorias.Repositorios.Filtros;
using SistemaFinanceiros.Dominio.Categorias.Servicos.Comandos;

namespace SistemaFinanceiros.Aplicacao.Categorias.Profiles
{
    public class CategoriasProfile : Profile
    {
        public CategoriasProfile()
        {
        CreateMap<Categoria, CategoriaResponse>()
        .ForMember(x => x.idSistemaFinanceiro, m => m.MapFrom(y => y.SistemaFinanceiro!.Id));
        CreateMap<CategoriaInserirRequest, CategoriaComando>();
        CreateMap<CategoriaEditarRequest, CategoriaComando>();
        CreateMap<CategoriaListarRequest, CategoriaListarFiltro>();
        CreateMap<Categoria, CategoriaNomeResponse>();

        }
    }
}