using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaFinanceiros.Dominio.Despesas.Entidades;
using SistemaFinanceiros.Dominio.Despesas.Enumeradores;
using SistemaFinanceiros.Dominio.Despesas.Servicos.Comandos;

namespace SistemaFinanceiros.Dominio.Despesas.Servicos.Interfaces
{
    public interface IDespesasServico
    {
        Despesa Validar(int id);
        Despesa Inserir(DespesaComando comando);
        Despesa Editar(int id, DespesaComando comando);
        object CarregaGraficos(string emailUsuario);
        
    }
}