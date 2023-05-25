using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaFinanceiros.Dominio.Categorias.Entidades;
using SistemaFinanceiros.Dominio.Categorias.Servicos.Comandos;

namespace SistemaFinanceiros.Dominio.Categorias.Servicos.Interfaces
{
    public interface ICategoriasServico
    {
        Categoria Validar(int id);
        Categoria Inserir(CategoriaComando comando);
        Categoria Editar(int id, CategoriaComando comando);
        Categoria Instanciar(CategoriaComando comando);
    }
}