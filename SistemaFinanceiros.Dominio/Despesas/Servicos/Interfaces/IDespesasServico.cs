using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaFinanceiros.Dominio.Despesas.Entidades;
using SistemaFinanceiros.Dominio.Despesas.Enumeradores;

namespace SistemaFinanceiros.Dominio.Despesas.Servicos.Interfaces
{
    public interface IDespesasServico
    {
        Despesa Validar(int id);
        Despesa Inserir(Despesa despesa);
        Despesa Instanciar(string nome, decimal valor, int mes, int ano, EnumTipoDespesa tipoDespesa, DateTime dataCadastro,
        DateTime dataAlteracao, DateTime dataVencimento, bool pago, bool despesaAtrasada,
        int idCategoria, int IdUsuario);
        Despesa Editar(int id, string nome, decimal valor, int mes, int ano, EnumTipoDespesa tipoDespesa, DateTime dataCadastro,
        DateTime dataAlteracao, DateTime dataVencimento, bool pago, bool despesaAtrasada,
        int idCategoria);
        
    }
}