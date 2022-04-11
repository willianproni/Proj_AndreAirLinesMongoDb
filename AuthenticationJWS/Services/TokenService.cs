using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthenticationJWS.Config;
using Microsoft.IdentityModel.Tokens;
using Model;

namespace AuthenticationJWS.Services
{
    public class TokenService
    {
        public static string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler(); //Gerar o Token
            var key = Encoding.ASCII.GetBytes(Settings.Secret); //Enconding, trasforma a chave em um Array de Bytes
            var tokenDescriptor = new SecurityTokenDescriptor //Descreve as informações principais para o token funcionar
            {
                Subject = new ClaimsIdentity(new Claim[] //Informações relacionadas ao Usuário
                {
                    new Claim(ClaimTypes.Name, user.Login.ToString()), //User.Identity.Login
                    new Claim(ClaimTypes.Role, user.Function.Access.Description.ToString()), //User.Identity.Function.Access.Id
                }),
                Expires = DateTime.UtcNow.AddHours(2), //Quanto tempo o Token demora para expirar
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor); //Criando o Token
            return tokenHandler.WriteToken(token); //Retonrnado o Token
        }
    }
}
