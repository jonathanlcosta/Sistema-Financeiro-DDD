using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SistemaFinanceiros.DataTransfer.Categorias.Request;
using SistemaFinanceiros.DataTransfer.Categorias.Response;
using SistemaFinanceiros.Dominio.Categorias.Entidades;

namespace SistemaFinanceiros.Aplicacao.Categorias.Profiles
{
    public class CategoriasProfile : Profile
    {
        public CategoriasProfile()
        {
        CreateMap<Categoria, CategoriaResponse>()
        .ForMember(x => x.idSistemaFinanceiro, m => m.MapFrom(y => y.SistemaFinanceiro!.Id));
        }
    }
}