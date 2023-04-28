using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaFinanceiros.DataTransfer.SistemaFinanceiros.Request;
using SistemaFinanceiros.DataTransfer.SistemaFinanceiros.Response;
using SistemaFinanceiros.Dominio.util;

namespace SistemaFinanceiros.Aplicacao.SistemaFinanceiros.Servicos.Interfaces
{
    public interface ISistemaFinanceirosAppServico
    {
        PaginacaoConsulta<SistemaFinanceiroResponse> Listar(int? pagina, int quantidade, SistemaFinanceiroListarRequest sistemaFinanceiroListarRequest);
        SistemaFinanceiroResponse Recuperar(int id);
        SistemaFinanceiroResponse Inserir(SistemaFinanceiroInserirRequest sistemaFinanceiroInserirRequest);
        SistemaFinanceiroResponse Editar(int id, SistemaFinanceiroEditarRequest sistemaFinanceiroEditarRequest);
        void Excluir(int id);
    }
}