using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaFinanceiros.DataTransfer.Despesas.Request;
using SistemaFinanceiros.DataTransfer.Despesas.Response;
using SistemaFinanceiros.Dominio.Despesas.Repositorios.Consultas;
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
        PaginacaoConsulta<DespesaConsultaResponse> ListarDespesasUsuarioNaoPagasMesesAnteriorDapper(string email);
        object CarregaGraficos(string email);
        IList<DespesaResponse> ListarDespesasUsuarioNaoPagasMesesAtras(string email);
        PaginacaoConsulta<DespesaResponse> Listar(DespesaListarRequest request);
        PaginacaoConsulta<DespesaResponse> ListarDespesas(DespesaListarRequest request);
        IList<DespesasResumo> Consulta();
        Stream ExportarExcel(DespesaListarRequest request);

    }
}