using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using SistemaFinanceiros.Dominio.Autenticacoes.Servicos.Interfaces;
using SistemaFinanceiros.Dominio.Usuarios.Entidades;

namespace SistemaFinanceiros.Dominio.Autenticacoes.Servicos
{
    public class AutenticacoesServico : IAutenticacoesServico
    {
        public string GerarToken(Usuario usuario)
        {
             SymmetricSecurityKey chave = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("0asdjas09djsa09djasdjsadajsd09asjd09sajcnzxn")
                );
            SigningCredentials credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            Claim[] claimsCliente = new Claim[]
            {
                new Claim("id", usuario.Id.ToString())
            };
            
            JwtSecurityToken token = new JwtSecurityToken(
                claims: claimsCliente,
                signingCredentials: credenciais,
                expires: DateTime.UtcNow.AddHours(1)    
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public Usuario ValidarCadastro(string email, string senha)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new Exception("Email invalido");
            }

            if (string.IsNullOrEmpty(senha))
            {
               throw new Exception("Senha invalido"); 
            }

            var usuario = new Usuario(email, senha);
            return usuario;
        }

        public Usuario ValidarLogin(Usuario usuario, string senha)
        {
            if (usuario is null)
            {
                throw new Exception("Email incorreto");
            }
            if (!BCrypt.Net.BCrypt.Verify(senha, usuario.Senha))
            {
                throw new Exception("Senha incorreta");
            }
            return usuario;
        }
    }
}