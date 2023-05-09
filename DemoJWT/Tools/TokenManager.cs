using DemoJWT_DAL.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DemoJWT.Tools
{
    public class TokenManager
    {
        public static string secretkey = "et la je met ce qu'il me plait";
        public string GenerateToken(User u)
        {
            //Générer la clé de validation du token
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretkey));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);

            //Creation de l'objet contenant les informations du token
            Claim[] claims = new[]
            {
                new Claim(ClaimTypes.Name, u.Nickname),
                new Claim(ClaimTypes.Role, (u.IsAdmin) ? "admin" : "user"),
                new Claim(ClaimTypes.Email, u.Email),
                new Claim("UserId", u.Id.ToString())
            };

            //Configuration et génération du token
            JwtSecurityToken token = new JwtSecurityToken(
                claims : claims,
                signingCredentials : credentials,
                expires: DateTime.Now.AddDays(1),
                issuer : "myclient.com",
                audience : "myapidomain.com"
                );

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            return handler.WriteToken(token);
        }
    }
}
