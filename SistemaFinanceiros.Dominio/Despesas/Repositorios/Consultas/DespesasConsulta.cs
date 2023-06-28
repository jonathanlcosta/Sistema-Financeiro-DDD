using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaFinanceiros.Dominio.Despesas.Repositorios.Consultas
{
    public class DespesasConsulta
    {
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public int Mes { get; set; }
        public int Ano { get; set; }
        public string NomeCategoria { get; set; }
        public string NomeSistema { get; set; }
        public string NomeUsuario { get; set; }
    }
}