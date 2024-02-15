using Microsoft.AspNetCore.Identity;

namespace ken_lo.Security;

public class ApplicationUser : IdentityUser
{
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
}