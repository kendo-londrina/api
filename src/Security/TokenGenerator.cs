using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ken_lo.Shared;
using Microsoft.IdentityModel.Tokens;

namespace ken_lo.Security;

public static class TokenGenerator
{
    public static string AccessToken(
        IList<Claim> claims,
        string userEmail,
        IConfiguration configuration
    ) {
        var escolaId = claims.FirstOrDefault((claim) => claim.Type == "EscolaId")!.Value;

        var key = Encoding.ASCII.GetBytes(configuration["Jwt:SecretKey"]!);

        _ = int.TryParse(configuration["JWT:AccessTokenValidityInMinutes"],
            out int accessTokenValidityInMinutes);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Email, userEmail),
                new Claim("EscolaId", escolaId),
            }),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature),
            NotBefore = Util.HorarioOficialBrasilia(),
            Expires = Util.HorarioOficialBrasilia().AddMinutes(accessTokenValidityInMinutes),
            Audience = configuration["Jwt:Audience"],
            Issuer = configuration["Jwt:Issuer"]
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    
    public static string RefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}
