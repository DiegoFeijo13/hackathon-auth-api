using Core.Entities;
using Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Services;
public class TokenService(IConfiguration config) : ITokenService
{
    public string GetFuncionarioToken(FuncionarioEntity funcionario)
    {
        var claims = new ClaimsIdentity([
                new Claim(ClaimTypes.Email, funcionario.Email),
                new Claim(ClaimTypes.Role, funcionario.Funcao.ToString())
                ]);

        return GetToken(claims);
    }

    public string GetClienteToken(ClienteEntity entity)
    {
        var claims = new ClaimsIdentity([
                new Claim(ClaimTypes.Email, entity.Email),
                new Claim(ClaimTypes.Sid, entity.Cpf)
                ]);

        return GetToken(claims);
    }

    private string GetToken(ClaimsIdentity claims)
    {
        var handler = new JwtSecurityTokenHandler();
        var secret = config.GetValue<string>("Secret");
        var issuerUrl = config.GetValue<string>("IssuerUrl");
        var audienceUrl = config.GetValue<string>("AudienceUrl");

        if (string.IsNullOrEmpty(secret))
            return string.Empty;

        var key = Encoding.ASCII.GetBytes(secret);
        var props = new SecurityTokenDescriptor()
        {
            Subject = claims,
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
