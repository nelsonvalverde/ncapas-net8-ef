using WebApi.Entities.UserRole.Dtos;
using WebApi.Entities.User.Dtos;
using WebApi.Entities.User.Enums;
using Test.WebApi.Data.Behaviours.Context;

namespace Test.WebApi.Data.UnitOfWorkContext;

public class UserUnitOfWorkTest(SharedContext sharedContext) : RootTest(sharedContext)
{
    [Fact]
    public async Task CreateUserAndAssignRoleAsync_Should_Return_NewUser_WithRole_KeepOnceConnection()
    {
        // Arrange
        var userId = Guid.NewGuid().ToString("D");
        var email = $"{userId}@outlook.com";
        var createUser = new CreateUserDto
        (
            Id: userId,
            Name: "Nelson",
            LastName: "Valverde La torre",
            PhoneNumber: "923238397",
            EmailConfirmed: false,
            Birthday: new DateOnly(1992, 2, 19),
            Email: email,
            PasswordHash: "Mi Password 123",
            StatusId: UserStatus.PendingForConfirmed
        );

        // Act
        await EfUnitOfWork.UserRepository.CreateAsync(createUser);
        await EfUnitOfWork.UserRoleRepository.AssignRoleToUser(new AssignUserRoleDto(userId, "0d53c6f7-1b1e-4180-b113-153f1190b604"));
        await EfUnitOfWork.SaveChangesAsync();

        //Assert
        var user = await EfUnitOfWork.UserRepository.GetByIdAsync(userId);
        var userRoles = await EfUnitOfWork.UserRoleRepository.GetRolesByUser(userId);
        Assert.NotNull(user);
        Assert.Equal(userId, user.Id);
        Assert.True(userRoles.TotalRecord > 0);
    }

}