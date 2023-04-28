using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SistemaFinanceiros.Dominio.util;

namespace SistemaFinanceiros.Aplicacao.Paginacao
{
    public class PaginacaoConsultaProfile : Profile
    {
        public PaginacaoConsultaProfile()
        {
            CreateMap(typeof(PaginacaoConsulta<>), typeof(PaginacaoConsulta<>));
        }
    }
}