using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaFinanceiros.Dominio.SistemaFinanceiros.Entidades;
using SistemaFinanceiros.Dominio.SistemaFinanceiros.Servicos.Comandos;

namespace SistemaFinanceiros.Dominio.SistemaFinanceiros.Servicos.Interfaces
{
    public interface ISistemaFinanceirosServico
    {
        SistemaFinanceiro Validar(int id);
        SistemaFinanceiro Inserir(SistemaFinanceiroComando comando);
        SistemaFinanceiro Editar(int id, SistemaFinanceiroComando comando);
    }
}