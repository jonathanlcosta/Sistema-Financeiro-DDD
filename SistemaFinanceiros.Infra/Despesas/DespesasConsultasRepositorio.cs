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
                    FROM DESPESAS D,
                    USUARIOS U,
                    CATEGORIAS C,
                    SISTEMAFINANCEIROS SF
                    WHERE D.idUsuario = U.id
                    AND D.idCategoria = C.id
                    AND C.idSistemaFinanceiro = SF.id
                    AND U.email = @Email
                    GROUP BY
                    IdSistemaFinanceiro,
                    NomeSistema,
                    IdCategoria,
                    NomeCategoria,
                    IdDespesa,
                    NomeDespesa,
                    IdUsuario,
                    NomeUsuario
                    ORDER BY COUNT(1) DESC";

          return Listar(query, parametros, pagina, quantidade);
      }


    }
}