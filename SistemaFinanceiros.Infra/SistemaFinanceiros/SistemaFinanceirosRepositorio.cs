using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NHibernate;
using SistemaFinanceiros.Dominio.SistemaFinanceiros.Entidades;
using SistemaFinanceiros.Dominio.SistemaFinanceiros.Repositorios;
using SistemaFinanceiros.Infra.Genericos;

namespace SistemaFinanceiros.Infra.SistemaFinanceiros
{
    public class SistemaFinanceirosRepositorio : GenericoRepositorio<SistemaFinanceiro>, ISistemaFinanceirosRepositorio
    {
       public SistemaFinanceirosRepositorio(ISession session) : base (session)
       {
        
       } 
    }
}