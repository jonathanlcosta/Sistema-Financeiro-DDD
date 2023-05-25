using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaFinanceiros.Dominio.Despesas.Repositorios.Consultas;
using SistemaFinanceiros.Dominio.util;

namespace SistemaFinanceiros.Dominio.Despesas.Repositorios
{
    public interface IDespesasConsultasRepositorio
    {
        PaginacaoConsulta<DespesasResumo> ListarDespesasUsuarioNaoPagasMesesAnterior(int pagina, int quantidade, string email);
    }
}