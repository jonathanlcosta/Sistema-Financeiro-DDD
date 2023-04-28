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
        PaginacaoConsulta<DespesaResponse> Listar(int? pagina, int quantidade, DespesaListarRequest despesaListarRequest);
        DespesaResponse Recuperar(int id);
        DespesaResponse Inserir(DespesaInserirRequest despesaInserirRequest);
        DespesaResponse Editar(int id, DespesaEditarRequest despesaEditarRequest);
        void Excluir(int id); 
        IList<DespesaResponse> ListarDespesasUsuario(string emailUsuario);
        IList<DespesaResponse> ListarDespesasUsuarioNaoPagasMesesAnterior(string emailUsuario);


    }
}