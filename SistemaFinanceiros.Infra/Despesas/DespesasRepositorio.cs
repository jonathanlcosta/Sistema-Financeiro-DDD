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

        public IList<Despesa> ListarDespesasUsuario(string email)
        {
            IList<Despesa> despesas = session.Query<Despesa>().Join(session.Query<Categoria>(), 
                  d => d.Id,      
                  c => c.Id,     
                  (d, c) => new { Despesa = d, Categoria = c }) 
            .Join(session.Query<SistemaFinanceiro>(), 
                  dc => dc.Categoria.Id, 
                  s => s.Id,
                  (dc, s) => new { DespesaCategoria = dc, SistemaFinanceiro = s }) 
            .Join(session.Query<Usuario>(), 
                  dcs => dcs.DespesaCategoria.Despesa.Id, 
                  u => u.Id, 
                  (dcs, u) => new { DespesaCategoriaSistemaFinanceiro = dcs, UsuarioSistemaFinanceiro = u }) 
            .Where(s => s.DespesaCategoriaSistemaFinanceiro.DespesaCategoria.Despesa.Usuario.Email == email && s.DespesaCategoriaSistemaFinanceiro.SistemaFinanceiro.Mes == s.DespesaCategoriaSistemaFinanceiro.DespesaCategoria.Despesa.Mes && s.DespesaCategoriaSistemaFinanceiro.SistemaFinanceiro.Ano == s.DespesaCategoriaSistemaFinanceiro.DespesaCategoria.Despesa.Ano)
            .Select(s => s.DespesaCategoriaSistemaFinanceiro.DespesaCategoria.Despesa)
            .ToList();

            return despesas;
        }

       public IList<Despesa> ListarDespesasUsuarioNaoPagasMesesAnterior(string email)
        {
            int mesAnterior = DateTime.Now.AddMonths(-1).Month;
            IList<Despesa> despesas = session.Query<Despesa>()
                .Join(session.Query<Categoria>(), 
                    d => d.Id,      
                    c => c.Id,     
                    (d, c) => new { Despesa = d, Categoria = c }) 
                .Join(session.Query<SistemaFinanceiro>(), 
                    dc => dc.Categoria.Id, 
                    s => s.Id,
                    (dc, s) => new { DespesaCategoria = dc, SistemaFinanceiro = s }) 
                .Join(session.Query<Usuario>(), 
                    dcs => dcs.SistemaFinanceiro.Id, 
                    usf => usf.Id, 
                    (dcs, usf) => new { DespesaCategoriaSistemaFinanceiro = dcs, UsuarioSistemaFinanceiro = usf }) 
                .Where(s => s.UsuarioSistemaFinanceiro.Email == email && s.DespesaCategoriaSistemaFinanceiro.SistemaFinanceiro.Mes == mesAnterior && !s.DespesaCategoriaSistemaFinanceiro.DespesaCategoria.Despesa.Pago)
                .Select(s => s.DespesaCategoriaSistemaFinanceiro.DespesaCategoria.Despesa)
                .ToList();

            return despesas;
        }

    }
}