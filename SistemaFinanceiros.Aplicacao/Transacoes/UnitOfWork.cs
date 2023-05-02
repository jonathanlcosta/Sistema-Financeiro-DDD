using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NHibernate;
using SistemaFinanceiros.Aplicacao.Transacoes.Interfaces;

namespace SistemaFinanceiros.Aplicacao.Transacoes
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
         private ISession session;
        private ITransaction transaction;

        public UnitOfWork(ISession session)
        {
            this.session = session;
        }

        public void BeginTransaction()
        {
            this.transaction = session.BeginTransaction();
        }

        public void Commit()
        {
            if (transaction != null && transaction.IsActive)
            {
                transaction.Commit();
            }
        }

        public void Rollback()
        {
            if (transaction != null && transaction.IsActive)
            {
                transaction.Rollback();
            }
        }

        public void DefinirUsuarioDoBancoDeDadosNaSessao(int id)
        {
            if (transaction == null)
            {
                throw new Exception("Transação não iniciada");
            }

            if (session == null)
            {
                throw new Exception("Sessão não definida");
            }

            session
                .CreateSQLQuery("call DELTA.PKG_USUARIOSESSAO.PRC_DEFINIRUSUARIOATUAL(:PCODUSUARIO)")
                .SetParameter("PCODUSUARIO", id)
                .UniqueResult();
        }

        public void Dispose()
        {
            if (transaction != null)
            {
                transaction.Dispose();
                transaction = null;
            }

            if (session.IsOpen)
            {
                session.Close();
                session = null;
            }
        }

        public void Limpar()
        {
            if (session != null)
                session.Clear();
        }

        public void Flush()
        {
            if (session != null)
                session.Flush();
        }
    }
}