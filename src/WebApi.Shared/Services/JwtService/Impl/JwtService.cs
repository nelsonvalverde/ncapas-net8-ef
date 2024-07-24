using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WebApi.Shared.Constants;
using WebApi.Shared.Services.JwtService.Models;

namespace WebApi.Shared.Services.JwtService.Impl;

public class JwtService(IConfiguration configuration) : IJwtService
{
    private readonly IConfiguration _configuration = configuration;

    public JwtToken GenerateToken(JwtUser user, IEnumerable<Claim>? claims = null)
    {
        var jwtStting = GetJwtSetting();

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(jwtStting.SecretKey);
        var subject = new ClaimsIdentity();
        var refreshToken = GenerateRefreshToken();
        var expires = DateTime.UtcNow.AddMinutes(jwtStting.ExpirationInMinutes);
        subject.AddClaim(new Claim(CustomClaimTypes.Sub, user.UserId));
        subject.AddClaim(new Claim(CustomClaimTypes.RefreshToken, refreshToken));
        subject.AddClaim(new Claim(ClaimTypes.Email, user.Email));
        subject.AddClaim(new Claim(ClaimTypes.Name, user.Name));
        
        if (claims is not null) subject.AddClaims(claims);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = subject,
            Expires = expires,
            Issuer = jwtStting.Issuer,
            Audience = jwtStting.Audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return new JwtToken(Token: tokenHandler.WriteToken(token), RefreshToken: refreshToken, Expires: expires);
    }

    public ClaimsPrincipal ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenValidationParemeters = GetTokenValidationParameters();
        return tokenHandler.ValidateToken(token, tokenValidationParemeters, out SecurityToken validatedToken);
    }

    public TokenValidationParameters GetTokenValidationParameters()
    {
        var jwtStting = GetJwtSetting();
        var key = Encoding.ASCII.GetBytes(jwtStting.SecretKey);
        return new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true,
            ValidIssuer = jwtStting.Issuer,
            ValidateAudience = true,
            ValidAudience = jwtStting.Audience,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    }

    public JwtSetting GetJwtSetting()
    {
        var jwtSettingSection = _configuration.GetSection("JwtSetting");
        return new JwtSetting(
            SecretKey: jwtSettingSection.GetValue<string>("SecretKey") ?? string.Empty,
            ExpirationInMinutes: jwtSettingSection.GetValue<int>("ExpirationInMinutes"),
            Issuer: jwtSettingSection.GetValue<string>("Issuer") ?? string.Empty,
            Audience: jwtSettingSection.GetValue<string>("Audience") ?? string.Empty
        );
    }

    #region Private Methods

    private static string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    #endregion Private Methods
}