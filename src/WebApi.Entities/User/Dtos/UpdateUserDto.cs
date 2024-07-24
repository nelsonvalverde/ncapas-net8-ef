namespace WebApi.Entities.User.Dtos;

public sealed record UpdateUserDto(
    string Id, 
    string Name, 
    string LastName, 
    DateOnly Birthday);