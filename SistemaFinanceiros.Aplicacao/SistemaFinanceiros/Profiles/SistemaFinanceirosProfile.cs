using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SistemaFinanceiros.DataTransfer.SistemaFinanceiros.Request;
using SistemaFinanceiros.DataTransfer.SistemaFinanceiros.Response;
using SistemaFinanceiros.Dominio.SistemaFinanceiros.Entidades;
using SistemaFinanceiros.Dominio.SistemaFinanceiros.Repositorios.Filtros;
using SistemaFinanceiros.Dominio.SistemaFinanceiros.Servicos.Comandos;

namespace SistemaFinanceiros.Aplicacao.SistemaFinanceiros.Profiles
{
    public class SistemaFinanceirosProfile : Profile
    {
        public SistemaFinanceirosProfile()
        {
        CreateMap<SistemaFinanceiro, SistemaFinanceiroResponse>();
        CreateMap<SistemaFinanceiroInserirRequest, SistemaFinanceiroComando>();
        CreateMap<SistemaFinanceiroEditarRequest, SistemaFinanceiroComando>();
        CreateMap<SistemaFinanceiroListarRequest, SistemaFinanceiroListarFiltro>();
        }
    }
}