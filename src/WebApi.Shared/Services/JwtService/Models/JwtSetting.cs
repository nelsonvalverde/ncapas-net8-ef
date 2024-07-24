namespace WebApi.Shared.Services.JwtService.Models;

public record JwtSetting(
  string SecretKey,
  string Issuer,
  string Audience,
  int ExpirationInMinutes
);