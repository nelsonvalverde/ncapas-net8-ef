namespace WebApi.Shared.Services.JwtService.Models;

public sealed record JwtUser(string UserId, string Name, string Email);