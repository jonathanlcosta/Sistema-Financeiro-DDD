using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using SistemaFinanceiros.Dominio.Despesas.Entidades;
using SistemaFinanceiros.Dominio.Despesas.Enumeradores;

namespace SistemaFinanceiros.Infra.Despesas.Mapeamentos
{
    public class DespesasMap : ClassMap<Despesa>
    {
        public DespesasMap()
        {
            Schema("SistemaFinanceiro");
            Table("DESPESAS");
            Id(x=>x.Id).Column("id");
            Map(x=>x.Nome).Column("nome");
            Map(x=>x.DataCadastro).Column("dataCadastro");
            Map(x=>x.DataAlteracao).Column("dataAlteracao");
            Map(x=>x.DataPagamento).Column("dataPagamento");
            Map(x=>x.DataVencimento).Column("dataVencimento");
            Map(x=>x.DespesaAtrasada).Column("despesaAtrasada");
            Map(x=>x.Ano).Column("ano");
            Map(x=>x.Mes).Column("mes");
            Map(x=>x.Pago).Column("pago");
            Map(x=>x.Valor).Column("valor");
            Map(x=>x.TipoDespesa).CustomType<EnumTipoDespesa>().Column("tipoDespesa");
            References(x=>x.Categoria).Column("idCategoria");
        }
    }
}