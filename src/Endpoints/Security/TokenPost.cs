using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using ken_lo.Security;
using ken_lo.Shared;

namespace w_escolas.Endpoints.Usuarios;

public record LoginRequest(string Email, string Password);

public class TokenPost
{
    protected TokenPost() { }
    public static string Template => "/token";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => ActionAsync;
    private static ILogger<TokenPost>? _logger;

    [AllowAnonymous]
    public static async Task<IResult> ActionAsync(
        ILogger<TokenPost> logger,
        LoginRequest loginRequest,
        UserManager<ApplicationUser> userManager,
        IConfiguration configuration)
    {
        _logger = logger;
        _logger.LogInformation($"{loginRequest.Email} logando ...");
        var user = await userManager.FindByEmailAsync(loginRequest.Email);
        if (user == null)
            return Results.Unauthorized();

        if (!user.EmailConfirmed)
            return Results.Unauthorized();

        var passwordCheck = await userManager.CheckPasswordAsync(user, loginRequest.Password);
        if (!passwordCheck)
            return Results.Unauthorized();

        var claims = await userManager.GetClaimsAsync(user);
        _logger.LogInformation($"{loginRequest.Email} logou.");

        var refreshToken = TokenGenerator.RefreshToken();
        var accessToken = TokenGenerator.AccessToken(claims, loginRequest.Email, configuration);

        _ = int.TryParse(configuration["JWT:RefreshTokenValidityInMinutes"],
            out int refreshTokenValidityInMinutes);

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = Util.HorarioOficialBrasilia().AddMinutes(refreshTokenValidityInMinutes);

        await userManager.UpdateAsync(user);

        return Results.Ok(new
        {
            accessToken,
            refreshToken
        });
    }
}
