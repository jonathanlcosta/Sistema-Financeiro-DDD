using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using SistemaFinanceiros.Dominio.Usuarios.Entidades;

namespace SistemaFinanceiros.Infra.Usuarios.Mapeamentos
{
    public class UsuariosMap : ClassMap<Usuario>
    {
        public UsuariosMap()
        {
            Schema("SistemaFinanceiro");
            Table("USUARIOS");
            Id(x=>x.Id).Column("id");
            Map(x=>x.Nome).Column("nome");
            Map(x=>x.Email).Column("email");
            Map(x=>x.Senha).Column("senha");
            Map(x=>x.CPF).Column("cpf");
            Map(x=>x.Administrador).Column("administrador");
            Map(x=>x.SistemaAtual).Column("sistemaAtual");
            References(x=>x.SistemaFinanceiro).Column("idSistemaFinanceiro");
        }
    }
}