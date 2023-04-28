using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SistemaFinanceiros.DataTransfer.SistemaFinanceiros.Request;
using SistemaFinanceiros.DataTransfer.SistemaFinanceiros.Response;
using SistemaFinanceiros.Dominio.SistemaFinanceiros.Entidades;

namespace SistemaFinanceiros.Aplicacao.SistemaFinanceiros.Profiles
{
    public class SistemaFinanceirosProfile : Profile
    {
        public SistemaFinanceirosProfile()
        {
        CreateMap<SistemaFinanceiro, SistemaFinanceiroResponse>();
        CreateMap<SistemaFinanceiro, SistemaFinanceiroListarRequest>();
        CreateMap<SistemaFinanceiroInserirRequest, SistemaFinanceiro>();
        CreateMap<SistemaFinanceiroEditarRequest, SistemaFinanceiro>();
        }
    }
}