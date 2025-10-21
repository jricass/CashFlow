using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Security.Tokens;
using Microsoft.IdentityModel.Tokens;

namespace CashFlow.Infrastructure.Security.Tokens;

public class JwtTokenGenerator : IAccessTokenGenerator
{
    // Tempo de expiração do token em minutos.
    private readonly uint _expirationTimeMinutes; // 'uint' só aceita valores positivos
    // Chave secreta para assinar o token.
    private readonly string _signingKey;

    public JwtTokenGenerator(uint expirationTimeMinutes, string signignKey)
    {
        _expirationTimeMinutes = expirationTimeMinutes;
        _signingKey = signignKey;
    }

    // Método principal para geração do token.
    public string Generate(User user)
    {
        // Cria uma lista de Claim que conteém informações do usuário.
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Sid, user.UserIdentifier.ToString()), // Valor único: GUID
            new Claim(ClaimTypes.Role, user.Role)
        };

        // descriptor: Descreve como um token JWT deve ser gerado.
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Expires = DateTime.UtcNow.AddMinutes(_expirationTimeMinutes), // Data de expiração.
            SigningCredentials = new SigningCredentials(SecurityKey(), SecurityAlgorithms.HmacSha256Signature), // Infos para assinar o token.
            Subject = new ClaimsIdentity(claims) // Identidade do usuário, conjunto de informações associados ao usuário.
        };

        var tokenHandler = new JwtSecurityTokenHandler(); // Cria um objeto responsável por gerar e manipular tokens JWT.

        var securityToken = tokenHandler.CreateToken(tokenDescriptor); // Usa o método CreateToken a partir de tokenHandler.

        return tokenHandler.WriteToken(securityToken); // Serializa o objeto securityToken em uma string e retorna.
    }

    private SymmetricSecurityKey SecurityKey()
    {
        var key = Encoding.UTF8.GetBytes(_signingKey);

        return new SymmetricSecurityKey(key);
    }
}

/*
Essa classe serve para gerar tokens JWT para autenticação de usuários. Cria tokens seguros
contendo infromações do usuário, que podem ser usados para validar acessos em APIs.
*/