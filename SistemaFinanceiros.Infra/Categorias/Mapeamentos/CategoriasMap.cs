using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using SistemaFinanceiros.Dominio.Categorias.Entidades;

namespace SistemaFinanceiros.Infra.Categorias.Mapeamentos
{
    public class CategoriasMap : ClassMap<Categoria>
    {
        public CategoriasMap()
        {
            Schema("SistemaFinanceiro");
            Table("CATEGORIAS");
            Id(x=>x.Id).Column("id");
            Map(x=>x.Nome).Column("nome");
            References(x=>x.SistemaFinanceiro).Column("idSistemaFinanceiro");
        }
    }
}