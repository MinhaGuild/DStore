using Microsoft.IdentityModel.Tokens;
using ModelLibrary.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DStore.Services
{
    /// <summary>
    /// Classe estática que fará a geração do token.
    /// Esta classe depende de dois pacotes NuGet: 
    /// - Microsoft.AspNetCore.Authentication
    /// - Microsoft.AspNetCore.Authentication.JwtBearer
    /// </summary>
    public static class TokenService
    {
        /// <summary>
        /// Método único que realiza a criação do token a partir dos parametros passados para o memso.
        /// </summary>
        /// <param name="user">Objeto do tipo User. Este pode ser encontrado no projeto Model.base.</param>
        /// <param name="secret">String contendo o código secreto que reralizará o enrosco do token com a aplicação.</param>
        /// <returns>String contendo o token no formato Bearer.</returns>
        public static string GenerateToken(User user, string secret)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                //Neste Subject é possivel adicionar outras informações do usuário para que estas informações estejam presentes no token gerado.
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
