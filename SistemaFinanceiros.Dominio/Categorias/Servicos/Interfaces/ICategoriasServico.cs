using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaFinanceiros.Dominio.Categorias.Entidades;

namespace SistemaFinanceiros.Dominio.Categorias.Servicos.Interfaces
{
    public interface ICategoriasServico
    {
        Categoria Validar(int id);
        Categoria Inserir(Categoria categoria);
        Categoria Instanciar(string nome, int idSistemaFinanceiro);
        Categoria Editar(int id, string nome, int idSistemaFinanceiro);
    }
}