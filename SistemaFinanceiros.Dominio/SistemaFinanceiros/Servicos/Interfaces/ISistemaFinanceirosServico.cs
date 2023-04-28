using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaFinanceiros.Dominio.SistemaFinanceiros.Entidades;

namespace SistemaFinanceiros.Dominio.SistemaFinanceiros.Servicos.Interfaces
{
    public interface ISistemaFinanceirosServico
    {
        SistemaFinanceiro Validar(int id);
        SistemaFinanceiro Inserir(SistemaFinanceiro sistemaFinanceiro);
        SistemaFinanceiro Instanciar(string nome, int mes, int ano, int diafechamento, bool gerarCopiaDespesa, int mesCopia, int anoCopia);
        SistemaFinanceiro Editar(int id, string nome, int mes, int ano, int diafechamento, bool gerarCopiaDespesa, int mesCopia, int anoCopia);
    }
}