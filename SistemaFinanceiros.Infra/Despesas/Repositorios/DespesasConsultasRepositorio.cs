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

namespace SistemaFinanceiros.Infra.Despesas.Repositorios
{
    public class DespesasConsultasRepositorio : RepositorioDapper<DespesasConsulta>, IDespesasConsultasRepositorio
    {
        public DespesasConsultasRepositorio(ISession session) : base (session)
        {
            
        }

      public PaginacaoConsulta<DespesasConsulta> ListarDespesasUsuarioNaoPagasMesesAnterior(int pagina, int quantidade)
      {
          var parametros = new DynamicParameters();

                  var query = @"SELECT D.nome AS Nome,
              D.valor AS Valor,
              D.mes AS Mes,
              D.ano AS Ano,
              C.nome AS NomeCategoria,
              S.nome AS NomeSistema,
              U.email AS NomeUsuario
              FROM DESPESAS D,
              CATEGORIAS C,
              SISTEMAFINANCEIROS S,
              USUARIOS U";

          return Listar(query, parametros, pagina, quantidade);
      }


    }
}