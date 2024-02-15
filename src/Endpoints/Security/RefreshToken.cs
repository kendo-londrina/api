using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using ken_lo.Security;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ken_lo.Shared;

namespace w_escolas.Endpoints.Security;

public record RefreshTokenRequest(string AccessToken, string RefreshToken);

public static class RefreshToken
{
    public static string Template => "/refresh-token";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => ActionAsync;

    [AllowAnonymous]
    public static async Task<IResult> ActionAsync(
        RefreshTokenRequest refreshTokenRequest,
        UserManager<ApplicationUser> userManager,
        IConfiguration configuration)
    {

        string? accessToken = refreshTokenRequest.AccessToken;
        string? refreshToken = refreshTokenRequest.RefreshToken;        
        
        var principal = GetPrincipalFromExpiredToken(accessToken, configuration);
        if (principal == null)
        {
            return Results.BadRequest("01-Invalid access token/refresh token");
        }

        string userEmail = principal.Claims.ToList()[0].Value;
        var user = await userManager.FindByEmailAsync(userEmail);

        if (user == null)
        {
            return Results.BadRequest("02-Invalid access token/refresh token");
        }
        if (user.RefreshToken != refreshToken)
        {
            return Results.BadRequest("03-Invalid access token/refresh token");
        }
        if (user.RefreshTokenExpiryTime <= DateTime.Now)
        {
            return Results.BadRequest("04-Invalid access token/refresh token");
        }

        var claims = await userManager.GetClaimsAsync(user);

        var newRefreshToken = TokenGenerator.RefreshToken();
        var newAccessToken = TokenGenerator.AccessToken(claims, userEmail, configuration);
        _ = int.TryParse(configuration["JWT:RefreshTokenValidityInMinutes"],
            out int refreshTokenValidityInMinutes);        

        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiryTime = Util.HorarioOficialBrasilia().AddMinutes(refreshTokenValidityInMinutes);
        await userManager.UpdateAsync(user);

        return Results.Ok(new
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken
        });
    }

    private static ClaimsPrincipal? GetPrincipalFromExpiredToken(
        string? token, IConfiguration configuration
    )
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                                .GetBytes(configuration["Jwt:SecretKey"]!)),
            ValidateLifetime = false
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters,
                        out SecurityToken securityToken);

        if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                    !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                                    StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");

        return principal;
    }
}
