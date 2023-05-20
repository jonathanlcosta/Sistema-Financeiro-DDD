using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaFinanceiros.Dominio.util.Filtros.Enumeradores;

namespace SistemaFinanceiros.Dominio.util.Filtros
{
     public class PaginacaoFiltro
    {
        private int qt;
        public int Qt
        {
            get => qt;
            set => qt = (value < 100 ? value : 100);
        }

        public int Pg { get; set; }
        public TipoOrdenacaoEnum TpOrd { get; set; }
        public string CpOrd { get; set; }

        public PaginacaoFiltro(string cpOrd, TipoOrdenacaoEnum tpOrd)
        {
            this.Qt = 10;
            this.Pg = 1;
            this.CpOrd = cpOrd;
            this.TpOrd = tpOrd;
        }

        public string ObterSqlOrdenacao()
        {
            if (string.IsNullOrWhiteSpace(CpOrd))
            {
                return string.Empty;
            }

            return string.Format(" ORDER BY {0} {1}", CpOrd, TpOrd);
        }
    }
}