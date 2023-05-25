using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaFinanceiros.Dominio.Despesas.Repositorios.Consultas
{
    public class DespesasResumo
    {
        public string NomeSistema { get; set; }
        public string NomeCategoria { get; set; }
        public string NomeDespesa { get; set; }
        public string NomeUsuario { get; set; }
    }
}