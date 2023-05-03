using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NHibernate;
using SistemaFinanceiros.Dominio.Genericos;
using SistemaFinanceiros.Dominio.util;

namespace SistemaFinanceiros.Infra.Genericos
{
    public class GenericoRepositorio<T> : IGenericoRepositorio<T> where T : class
    {
         protected readonly ISession session;
        public GenericoRepositorio(ISession session)
        {
            this.session = session;
        }
        public T Editar(T entidade)
        {
            session.Update(entidade);
            return entidade;
        }

        public void Excluir(T entidade)
        {
            session.Delete(entidade);
        }

        public T Inserir(T entidade)
        {
            session.Save(entidade);
            return entidade;
        }

        public void Inserir(IEnumerable<T> entidades)
        {
            foreach (T entidade in entidades)
            {
                session.Save(entidade);
            }
        }

        public PaginacaoConsulta<T> Listar(IQueryable<T> query, int? pagina, int quantidade)
        {
            int quantidadeRegistros = query.ToList().Count();
            IList<T> registros = query.Skip((pagina.Value-1)*quantidade).Take(quantidade).ToList();
            PaginacaoConsulta<T> consulta = new PaginacaoConsulta<T>(quantidadeRegistros, registros);
            return consulta;
        }

        public IQueryable<T> Query()
        {
            return session.Query<T>();
        }

         public IList<T> QueryList()
        {
            return session.Query<T>().ToList();
        }


        public T Recuperar(int id)
        {
            return session.Get<T>(id);
        }
    }
}