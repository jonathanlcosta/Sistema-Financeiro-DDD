using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaFinanceiros.DataTransfer.Usuarios.Request;
using SistemaFinanceiros.DataTransfer.Usuarios.Response;
using SistemaFinanceiros.Dominio.util;

namespace SistemaFinanceiros.Aplicacao.Usuarios.Servicos.Interfaces
{
    public interface IUsuariosAppServico
    {
        PaginacaoConsulta<UsuarioResponse> Listar(int? pagina, int quantidade, UsuarioListarRequest usuarioListarRequest);
        UsuarioResponse Recuperar(int id);
        UsuarioResponse Editar(int id, UsuarioEditarRequest usuarioEditarRequest);
        void Excluir(int id); 
    }
}