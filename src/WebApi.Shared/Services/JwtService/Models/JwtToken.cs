namespace WebApi.Shared.Services.JwtService.Models;

public sealed record JwtToken(string Token, string RefreshToken, DateTime Expires);