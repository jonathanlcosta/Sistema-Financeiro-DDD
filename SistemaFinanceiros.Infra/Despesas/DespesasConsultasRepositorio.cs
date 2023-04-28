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

        public PaginacaoConsulta<DespesasUsuarioNaoPagasMesesAnterior> ListarDespesasUsuarioNaoPagasMesesAnterior(int pagina, int quantidade)
        {
                 return Listar(@"SELECT COUNT(1) as QuantidadeDespesas,
                           SF.IdSistemaFinanceiro,
                           SF.NomeSistema,
                           C.IdCategoria,
                           C.NomeCategoria,
                           D.IdDespesa,
                           D.NomeDespesa,
                           U.IdUsuario,
                           U.NomeUsuario
                    FROM DESPESAS D
                    INNER JOIN USUARIOS U ON D.IdUsuario = U.IdUsuario
                    INNER JOIN CATEGORIAS C ON D.IdCategoria = C.IdCategoria
                    INNER JOIN SISTEMAFINANCEIROS SF ON D.IdSistemaFinanceiro = SF.IdSistemaFinanceiro
                    WHERE D.pago = 0
                      AND D.dataVencimento < DATEADD(month, DATEDIFF(month, 0, GETDATE()), 0)
                    GROUP BY SF.IdSistemaFinanceiro,
                             SF.NomeSistema,
                             C.IdCategoria,
                             C.NomeCategoria,
                             D.IdDespesa,
                             D.NomeDespesa,
                             U.IdUsuario,
                             U.NomeUsuario
                    ORDER BY COUNT(1) DESC", new DynamicParameters(), pagina, quantidade);
            }

    }
}