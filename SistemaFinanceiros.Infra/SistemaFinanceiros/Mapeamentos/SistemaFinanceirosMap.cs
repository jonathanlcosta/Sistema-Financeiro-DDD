using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using SistemaFinanceiros.Dominio.SistemaFinanceiros.Entidades;

namespace SistemaFinanceiros.Infra.SistemaFinanceiros.Mapeamentos
{
    public class SistemaFinanceirosMap : ClassMap<SistemaFinanceiro>
    {
        public SistemaFinanceirosMap()
        {
            Schema("SistemaFinanceiro");
            Table("SISTEMAFINANCEIROS");
            Id(x=>x.Id).Column("id");
            Map(x=>x.Nome).Column("nome");
            Map(x=>x.Mes).Column("mes");
            Map(x=>x.Ano).Column("ano");
            Map(x=>x.AnoCopia).Column("anoCopia");
            Map(x=>x.GerarCopiaDespesa).Column("gerarCopiaDespesa");
            Map(x=>x.MesCopia).Column("mesCopia");
            Map(x=>x.DiaFechamento).Column("diaFechamento");
           
        }
    }
}