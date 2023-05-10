using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SistemaFinanceiros.DataTransfer.Despesas.Request;
using SistemaFinanceiros.DataTransfer.Despesas.Response;
using SistemaFinanceiros.Dominio.Despesas.Entidades;

namespace SistemaFinanceiros.Aplicacao.Despesas.Profiles
{
    public class DespesasProfile : Profile
    {
        public DespesasProfile()
        {
        CreateMap<Despesa, DespesaResponse>();
        }
    }
}