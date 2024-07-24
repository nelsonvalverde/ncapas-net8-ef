using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using WebApi.Shared.Services.JwtService.Models;

namespace WebApi.Shared.Services.JwtService;

public interface IJwtService
{
    /// <summary>
    /// Generatea a new token.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <param name="claims">The claims (optional).</param>
    /// <returns></returns>
    JwtToken GenerateToken(JwtUser user, IEnumerable<Claim>? claims = null);
    /// <summary>
    /// Validates the token.
    /// </summary>
    /// <param name="token">The token.</param>
    /// <returns></returns>
    ClaimsPrincipal ValidateToken(string token);
    /// <summary>
    /// Gets the token validation parameters.
    /// </summary>
    /// <returns></returns>
    TokenValidationParameters GetTokenValidationParameters();
}