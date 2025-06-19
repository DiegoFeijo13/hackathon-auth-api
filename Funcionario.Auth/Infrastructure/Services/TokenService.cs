using Core.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;

namespace Infrastructure.Services;
public class TokenService(IConfiguration config) : ITokenService
{
    public string GetToken(FuncionarioEntity funcionario)
    {
        var handler = new JwtSecurityTokenHandler();
        var secret = config.GetValue<string>("Secret");
        var issuerUrl = config.GetValue<string>("IssuerUrl");
        var audienceUrl = config.GetValue<string>("AudienceUrl");

        if(string.IsNullOrEmpty(secret))
            return string.Empty;

        var key = Encoding.ASCII.GetBytes(secret);
        var props = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity([
                new Claim(ClaimTypes.Email, funcionario.Email),
                new Claim(ClaimTypes.Role, funcionario.Funcao.ToString())
                ]),
            Expires = DateTime.UtcNow.AddHours(1),

            Issuer = issuerUrl,
            Audience = audienceUrl,

            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = handler.CreateToken(props);
        return handler.WriteToken(token);
    }
}
