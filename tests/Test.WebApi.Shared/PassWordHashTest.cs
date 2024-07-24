using WebApi.Shared.Services.PasswordHashService;

namespace Test.WebApi.Shared;

public class PassWordHashTest()
{
    private readonly IEncryptService _passwordHashService = new PasswordHashService();

    [Fact]
    public void HashPassword_Should_Return_HashedPassword()
    {
        //Arrange
        var passowrd = "@Password123.";

        //Act
        var hashedPassword = _passwordHashService.Encrypt(passowrd);

        //Assert
        Assert.NotNull(hashedPassword);
        Assert.NotEqual(passowrd, hashedPassword);

    }

    [Fact]
    public void VerifyPassword_Should_Return_True_For_CorrectPassword()
    {
        //Arrange
        var passowrd = "@Password123.";
        var hashedPassword = _passwordHashService.Encrypt(passowrd);

        //Act
        var result = _passwordHashService.VerifyEncrypt(passowrd, hashedPassword);

        //Assert
        Assert.True(result);

    }
    [Fact]
    public void VerifyPassword_Should_Return_False_For_IncorrectPassword()
    {
        // Arrange
        var password = "@Password123.";
        var hashedPassword = _passwordHashService.Encrypt(password);
        var incorrectPassword = "WrongPassword";

        // Act
        var result = _passwordHashService.VerifyEncrypt(incorrectPassword, hashedPassword);

        // Assert
        Assert.False(result);
    }
}