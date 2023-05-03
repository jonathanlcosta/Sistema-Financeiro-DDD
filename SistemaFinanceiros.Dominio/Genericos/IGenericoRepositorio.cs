using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaFinanceiros.Dominio.util;

namespace SistemaFinanceiros.Dominio.Genericos
{
    public interface IGenericoRepositorio<T> where T: class
    {
        T Recuperar(int codigo);

        T Inserir(T entidade);

        T Editar(T entidade);

        void Excluir(T entidade);
        void Inserir(IEnumerable<T> entidades);

        PaginacaoConsulta<T> Listar(IQueryable<T> query, int? pagina, int quantidade);

        IQueryable<T> Query();

        IList<T> QueryList();
        
    }
}