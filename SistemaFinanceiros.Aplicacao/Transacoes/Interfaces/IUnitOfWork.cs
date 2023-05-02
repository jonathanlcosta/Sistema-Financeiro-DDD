using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaFinanceiros.Aplicacao.Transacoes.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        void BeginTransaction();
        void Commit();
        void DefinirUsuarioDoBancoDeDadosNaSessao(int id);
        void Rollback();
    }
}