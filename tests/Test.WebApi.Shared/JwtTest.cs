using System.Security.Claims;
using Test.WebApi.Shared.Behaviours;
using Test.WebApi.Shared.Behaviours.TestPriority;
using WebApi.Shared.Services.JwtService;
using WebApi.Shared.Services.JwtService.Impl;
using WebApi.Shared.Services.JwtService.Models;

namespace Test.WebApi.Shared;

[TestCaseOrderer(
    ordererTypeName: "Test.WebApi.Shared.Behaviours.TestPriority.PriorityOrderer",
    ordererAssemblyName: "Test.WebApi.Shared")]
public class JwtTest(SharedContext sharedContext) : IClassFixture<SharedContext>
{
    private readonly IJwtService _jwtService = new JwtService(sharedContext.Configuration);
    private readonly SharedContext _sharedContext = sharedContext;

    [Fact, TestPriority(1)]
    public void GenerateToken_Should_Return_NewToken()
    {
        //Arrange
        var userId = _sharedContext.Id;
        var jwtUser = new JwtUser(
            UserId: userId,
            Name: "Nelson H.",
            Email: "nelsonvalverdelt@outlook.com"
        );

        var claims = new List<Claim>
        {
            new(ClaimTypes.MobilePhone, "918789875"),
            new(ClaimTypes.Role, "Admin"),
        };

        //Act
        var tokenGenerated = _jwtService.GenerateToken(jwtUser, claims);
        _sharedContext.SetToken(tokenGenerated.Token);

        //Assert
        Assert.NotNull(tokenGenerated.Token);
        Assert.True(!string.IsNullOrEmpty(tokenGenerated.Token));
    }

    [Fact, TestPriority(2)]
    public void GenerateToken_Should_Return_ValidateToken()
    {
        //Arrange
        var token = _sharedContext.Token ?? string.Empty;

        //Act
        var claimPrincipal = _jwtService.ValidateToken(token);

        //Assert
        Assert.True(claimPrincipal is not null);
    }

    [Fact, TestPriority(3)]
    public void GenerateToken_Should_Return_ValidateClaimsFromUser()
    {
        //Arrange
        var userId = _sharedContext.Id;
        var token = _sharedContext.Token ?? string.Empty;
        var jwtUser = new JwtUser(
            UserId: userId,
            Name: "Nelson H.",
            Email: "nelsonvalverdelt@outlook.com"
        );
        //Act
        var claimPrincipal = _jwtService.ValidateToken(token);
        var nameClaim = claimPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
        var emailClaim = claimPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
        //Assert
        Assert.Equal(jwtUser.Name, nameClaim);
        Assert.Equal(jwtUser.Email, emailClaim);
    }

    [Fact, TestPriority(3)]
    public void GenerateToken_Should_Return_ValidateOptionClaims()
    {
        //Arrange
        var token = _sharedContext.Token ?? string.Empty;

        var mobilePhoneClaimExpected = new Claim(ClaimTypes.MobilePhone, "918789875");
        var roleClaimExpected = new Claim(ClaimTypes.Role, "Admin");

        //Act
        var claimPrincipal = _jwtService.ValidateToken(token);
        var mobilePhoneClaim = claimPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.MobilePhone);
        var roleClaim = claimPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role);

        //Assert
        Assert.Equal(mobilePhoneClaimExpected.Value, mobilePhoneClaim?.Value);
        Assert.Equal(roleClaimExpected.Value, roleClaim?.Value);
    }
}