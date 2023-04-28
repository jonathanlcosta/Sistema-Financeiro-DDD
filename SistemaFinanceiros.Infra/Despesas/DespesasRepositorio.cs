using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using SistemaFinanceiros.Dominio.Categorias.Entidades;
using SistemaFinanceiros.Dominio.Despesas.Entidades;
using SistemaFinanceiros.Dominio.Despesas.Repositorios;
using SistemaFinanceiros.Dominio.Despesas.Repositorios.Consultas;
using SistemaFinanceiros.Dominio.SistemaFinanceiros.Entidades;
using SistemaFinanceiros.Dominio.Usuarios.Entidades;
using SistemaFinanceiros.Dominio.util;
using SistemaFinanceiros.Infra.Genericos;

namespace SistemaFinanceiros.Infra.Despesas
{
    public class DespesasRepositorio : GenericoRepositorio<Despesa>, IDespesasRepositorio
    { 
        public DespesasRepositorio(ISession session) : base (session)
        {
            
        }

        public IList<Despesa> ListarDespesasUsuarioNaoPagasMesesAnterior(string emailUsuario)
        {
               var despesas = session.Query<Despesa>()
            .Join(session.Query<Categoria>(),
                  d => d.Id,
                  c => c.Id,
                  (d, c) => new { Despesa = d, Categoria = c })
            .Join(session.Query<SistemaFinanceiro>(),
                  dc => dc.Categoria.SistemaFinanceiro.Id,
                  s => s.Id,
                  (dc, s) => new { DespesaCategoria = dc, SistemaFinanceiro = s })
            .Join(session.Query<Usuario>(),
                  dcs => dcs.DespesaCategoria.Despesa.Categoria.SistemaFinanceiro.Usuario.Id,
                  u => u.Id,
                  (dcs, u) => new { DespesaCategoriaSistemaFinanceiro = dcs, UsuarioSistemaFinanceiro = u })
            .Where(s => s.UsuarioSistemaFinanceiro.Email == emailUsuario && s.DespesaCategoriaSistemaFinanceiro.DespesaCategoria.Despesa.Mes < DateTime.Now.Month && !s.DespesaCategoriaSistemaFinanceiro.DespesaCategoria.Despesa.Pago)
            .Select(s => s.DespesaCategoriaSistemaFinanceiro.DespesaCategoria.Despesa)
            .ToList();

        return despesas;
        }

        public IList<Despesa> ListarDespesasUsuario(string emailUsuario)
{
    var despesas = session.Query<Despesa>()
            .Join(session.Query<Categoria>(), // Segunda sequência a ser unida
                  d => d.Id,        // Chave da primeira sequência
                  c => c.Id,        // Chave da segunda sequência
                  (d, c) => new { Despesa = d, Categoria = c }) // Objeto resultante
            .Join(session.Query<SistemaFinanceiro>(), // Terceira sequência a ser unida
                  dc => dc.Categoria.Id, // Chave da primeira sequência
                  s => s.Id, // Chave da terceira sequência
                  (dc, s) => new { DespesaCategoria = dc, SistemaFinanceiro = s }) // Objeto resultante
            .Join(session.Query<Usuario>(), // Quarta sequência a ser unida
                  dcs => dcs.DespesaCategoria.Despesa.Id, // Chave da primeira sequência
                  u => u.Id, // Chave da quarta sequência
                  (dcs, u) => new { DespesaCategoriaSistemaFinanceiro = dcs, UsuarioSistemaFinanceiro = u }) // Objeto resultante
            .Where(s => s.DespesaCategoriaSistemaFinanceiro.DespesaCategoria.Despesa.Categoria.SistemaFinanceiro.Usuario.Email == emailUsuario && s.DespesaCategoriaSistemaFinanceiro.SistemaFinanceiro.Mes == s.DespesaCategoriaSistemaFinanceiro.DespesaCategoria.Despesa.Mes && s.DespesaCategoriaSistemaFinanceiro.SistemaFinanceiro.Ano == s.DespesaCategoriaSistemaFinanceiro.DespesaCategoria.Despesa.Ano)
            .Select(s => s.DespesaCategoriaSistemaFinanceiro.DespesaCategoria.Despesa)
            .ToList();

    return despesas;
}

public IList<Despesa> ListarDespesasUsuarioNaoPagasMesesAnteriorDois(string emailUsuario)
{
        var despesas = session.Query<Despesa>()
            .Join(session.Query<Categoria>(),
                  d => d.Id,
                  c => c.Id,
                  (d, c) => new { Despesa = d, Categoria = c })
            .Join(session.Query<SistemaFinanceiro>(),
                  dc => dc.Categoria.SistemaFinanceiro.Id,
                  s => s.Id,
                  (dc, s) => new { DespesaCategoria = dc, SistemaFinanceiro = s })
            .Join(session.Query<Usuario>(),
                  dcs => dcs.DespesaCategoria.Despesa.Categoria.SistemaFinanceiro.Usuario.Id,
                  u => u.Id,
                  (dcs, u) => new { DespesaCategoriaSistemaFinanceiro = dcs, UsuarioSistemaFinanceiro = u })
            .Where(s => s.UsuarioSistemaFinanceiro.Email == emailUsuario && s.DespesaCategoriaSistemaFinanceiro.DespesaCategoria.Despesa.Mes < DateTime.Now.Month && !s.DespesaCategoriaSistemaFinanceiro.DespesaCategoria.Despesa.Pago)
            .Select(s => s.DespesaCategoriaSistemaFinanceiro.DespesaCategoria.Despesa)
            .ToList();

        return despesas;
    
}




    }
}