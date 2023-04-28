using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaFinanceiros.Dominio.Despesas.Repositorios.Consultas
{
    public class DespesasUsuarioNaoPagasMesesAnterior
    {
        public int QuantidadeDespesas { get; set; }
        public int IdSistemaFinanceiro { get; set; }
        public string NomeSistema { get; set; }
        public int IdCategoria { get; set; }
        public string NomeCategoria { get; set; }
        public int IdDespesa { get; set; }
        public string NomeDespesa { get; set; }
        public int IdUsuario { get; set; }
        public string NomeUsuario { get; set; }
    }
}