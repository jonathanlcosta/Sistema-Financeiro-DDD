using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaFinanceiros.Dominio.SistemaFinanceiros.Entidades;
using SistemaFinanceiros.Dominio.Usuarios.Entidades;

namespace SistemaFinanceiros.Dominio.Usuarios.Servicos.Interfaces
{
    public interface IUsuariosServico
    {
        Usuario Validar(int id);
        Usuario Inserir(Usuario usuario);
        Usuario Instanciar(string cpf, string nome, string email,string senha, bool administrador, bool sistemaAtual, int idSistemaFinanceiro);
        Usuario Editar(int id, string cpf, string nome, string email, bool administrador, bool sistemaAtual, int idSistemaFinanceiro);
    }
}