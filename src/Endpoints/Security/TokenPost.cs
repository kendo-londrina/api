﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace w_escolas.Endpoints.Usuarios;

public record LoginRequest(string Email, string Password);

public class TokenPost
{
    public static string Template => "/token";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => ActionAsync;
    private static ILogger<TokenPost>? _logger;

    [AllowAnonymous]
    public static async Task<IResult> ActionAsync(
        ILogger<TokenPost> logger,
        LoginRequest loginRequest,
        UserManager<IdentityUser> userManager,
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
        var escolaId = claims.FirstOrDefault((claim) => claim.Type == "EscolaId")!.Value;

        var key = Encoding.ASCII.GetBytes(configuration["JwtBearerTokenSettings:SecretKey"]!);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Email, loginRequest.Email),
                new Claim("EscolaId", escolaId),
            }),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature),
            Audience = configuration["JwtBearerTokenSettings:Audience"],
            Issuer = configuration["JwtBearerTokenSettings:Issuer"]
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        _logger.LogInformation($"{loginRequest.Email} logou.");

        return Results.Ok(new
        {
            token = tokenHandler.WriteToken(token)
        });
    }
}
