using Test.WebApi.Data.Behaviours.Context;
using WebApi.Entities.User;
using WebApi.Entities.User.Dtos;
using WebApi.Entities.User.Enums;

namespace Test.WebApi.Data;

[TestCaseOrderer(
    ordererTypeName: "Test.WebApi.Data.Behaviours.TestPriority.PriorityOrderer",
    ordererAssemblyName: "Test.WebApi.Data")]
public class UserRepositoryTest(SharedContext sharedContext) : RootTest(sharedContext)
{
    [Fact, TestPriority(0)]
    public async Task CreateUserAsync_Should_Return_NewUser()
    {
        // Arrange
        var userId = SharedContext.Id;
        var email = "nelsonvalverdelt@outlook.com";
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
        await EfUnitOfWork.SaveChangesAsync();
        //Assert
        var user = await EfUnitOfWork.UserRepository.GetByIdAsync(userId);

        Assert.NotNull(user);
        Assert.Equal(userId, user.Id);
    }

    [Fact, TestPriority(1)]
    public async Task GetUserByIdAsync_Should_Return_If_User_Exists()
    {
        // Arrange
        var userId = SharedContext.Id;

        // Act
        var user = await EfUnitOfWork.UserRepository.GetByIdAsync(userId);

        // Assert
        Assert.NotNull(user);
    }

    [Fact, TestPriority(1)]
    public async Task GetUserByEmailAsync_Should_Return_If_User_Exists()
    {
        // Arrange
        var email = "nelsonvalverdelt@outlook.com";

        // Act
        var user = await EfUnitOfWork.UserRepository.GetUserByEmailAsync(email);

        // Assert
        Assert.NotNull(user);
    }

    [Fact, TestPriority(2)]
    public async Task UpdateUserAsync_Should_Return_User_Updated()
    {
        // Arrange
        var userId = SharedContext.Id;
        var birthday = new DateOnly(1992, 2, 19);
        var updateUser = new UpdateUserDto
        (
            Id: userId,
            Name: "Jorge",
            LastName: "Mendoza",
            Birthday: birthday
        );

        // Act
        await EfUnitOfWork.UserRepository.UpdateAsync(updateUser);
        await EfUnitOfWork.SaveChangesAsync();
        // Assert
        var user = await EfUnitOfWork.UserRepository.GetByIdAsync(userId);
        Assert.NotNull(user);
        Assert.Equal(user.Name, updateUser.Name);
        Assert.Equal(user.LastName, updateUser.LastName);
        Assert.Equal(user.Birthday, updateUser.Birthday);
    }

    [Fact, TestPriority(3)]
    public async Task UpdateUserStatusAsync_Should_Return_Another_Status()
    {
        // Arrange
        var userId = SharedContext.Id;
        var updateStatusUser = new UpdateUserStatusDto
        (
            Id: userId,
            StatusId: UserStatus.PendingForConfirmed
        );

        // Act
        await EfUnitOfWork.UserRepository.UpdateUserStatus(updateStatusUser);
        await EfUnitOfWork.SaveChangesAsync();
        // Assert
        var user = await EfUnitOfWork.UserRepository.GetByIdAsync(userId);
        Assert.NotNull(user);
        Assert.Equal(user.StatusId, updateStatusUser.StatusId);
    }

    [Fact]
    public async Task GetPageAsync_Should_Return_Page_1()
    {
        // Arrange

        var pageNumber = 1;
        var pageSize = 10;

        // Act
        var userPage = await EfUnitOfWork.UserRepository.GetPageAsync(pageNumber: pageNumber, pageSize: pageSize);

        // Assert
        Assert.NotNull(userPage);
        Assert.True(userPage.TotalRecord > 0);
        Assert.True(userPage.List.Count() > 1);
    }

    [Fact]
    public async Task GetPageAsync_Should_Return_Filter_By_Name()
    {
        // Arrange

        var pageNumber = 1;
        var pageSize = 10;
        var filterName = "Nelson";

        // Act
        var userPage = await EfUnitOfWork.UserRepository.GetPageAsync(pageNumber: pageNumber, pageSize: pageSize, filterName: filterName);

        // Assert
        Assert.NotNull(userPage);
        Assert.All(userPage.List, (UserEntity user) => user.Name.Equals(filterName, StringComparison.CurrentCultureIgnoreCase));
        Assert.All(userPage.List, (UserEntity user) => user.FullName.Equals(filterName, StringComparison.CurrentCultureIgnoreCase));

    }

    [Fact]
    public async Task FilterAsync_Should_Return_Filter_By_Name()
    {
        // Arrange

        var filter = "Nelson";

        // Act
        var userPage = await EfUnitOfWork.UserRepository.FilterAsync(filter);

        // Assert
        Assert.NotNull(userPage);
        Assert.All(userPage.List, (UserEntity user) => user.Name.Equals(filter, StringComparison.CurrentCultureIgnoreCase));
        Assert.All(userPage.List, (UserEntity user) => user.FullName.Equals(filter, StringComparison.CurrentCultureIgnoreCase));

    }
}