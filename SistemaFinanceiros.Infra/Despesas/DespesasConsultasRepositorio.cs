using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using NHibernate;
using SistemaFinanceiros.Dominio.Despesas.Repositorios;
using SistemaFinanceiros.Dominio.Despesas.Repositorios.Consultas;
using SistemaFinanceiros.Dominio.util;
using SistemaFinanceiros.Infra.Genericos;

namespace SistemaFinanceiros.Infra.Despesas
{
    public class DespesasConsultasRepositorio : RepositorioDapper<DespesasUsuarioNaoPagasMesesAnterior>, IDespesasConsultasRepositorio
    {
        public DespesasConsultasRepositorio(ISession session) : base (session)
        {
            
        }

      public PaginacaoConsulta<DespesasUsuarioNaoPagasMesesAnterior> ListarDespesasUsuarioNaoPagasMesesAnterior(int pagina, int quantidade, string email)
      {
          var parametros = new DynamicParameters();
          parametros.Add("@Email", email);

                    var query = @"SELECT 
                COUNT(1) as QuantidadeDespesas,
                SF.id as IdSistemaFinanceiro,
                SF.nome as NomeSistema,
                C.id as IdCategoria,
                C.nome as NomeCategoria,
                D.id as IdDespesa,
                D.nome as NomeDespesa,
                U.id as IdUsuario,
                U.nome as NomeUsuario
            FROM DESPESAS D
            INNER JOIN USUARIOS U ON D.idUsuario = U.id
            INNER JOIN CATEGORIAS C ON D.IdCategoria = C.id
            INNER JOIN SISTEMAFINANCEIROS SF ON C.idSistemaFinanceiro = SF.id
            WHERE U.email = @Email
                AND D.pago = false
                AND MONTH(D.dataVencimento) = MONTH(DATE_ADD(NOW(), INTERVAL -1 MONTH))
                AND YEAR(D.dataVencimento) = YEAR(DATE_ADD(NOW(), INTERVAL -1 MONTH))
            GROUP BY 
                IdSistemaFinanceiro,
                NomeSistema,
                IdCategoria,
                NomeCategoria,
                IdDespesa,
                NomeDespesa,
                IdUsuario,
                NomeUsuario
            ORDER BY QuantidadeDespesas DESC";

          return Listar(query, parametros, pagina, quantidade);
      }


    }
}