using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaFinanceiros.DataTransfer.Despesas.Request;
using SistemaFinanceiros.DataTransfer.Despesas.Response;
using SistemaFinanceiros.Dominio.util;

namespace SistemaFinanceiros.Aplicacao.Despesas.Servicos.Interfaces
{
    public interface IDespesasAppServico
    {
        DespesaResponse Recuperar(int id);
        DespesaResponse Inserir(DespesaInserirRequest despesaInserirRequest);
        DespesaResponse Editar(int id, DespesaEditarRequest despesaEditarRequest);
        void Excluir(int id); 
        IList<DespesaResponse> ListarDespesasUsuario(string emailUsuario);
        PaginacaoConsulta<DespesaResponse> ListarDespesasUsuarioNaoPagasMesesAnterior(string emailUsuario);
        object CarregaGraficos(string email);
        IList<DespesaResponse> ListarDespesasUsuarioNaoPagasMesesAtras(string email);
        PaginacaoConsulta<DespesaResponse> Listar(int? pagina, int quantidade, string email);

    }
}