using Test.WebApi.Business.Behaviours.Context;
using Test.WebApi.Business.Behaviours.TestPriority;
using WebApi.Business.UseCases.Users.Commands.CreateUser;
using WebApi.Business.UseCases.Users.Queries.GetUser;

namespace Test.WebApi.Business.Users.Commands;

[TestCaseOrderer(
    ordererTypeName: "Test.WebApi.Business.Behaviours.TestPriority.PriorityOrderer",
    ordererAssemblyName: "Test.WebApi.Business")]
public class CreateUserCommandHandlerTest(SharedContext sharedContext) : RootTest(sharedContext)
{
    [Fact, TestPriority(1)]
    public async Task Handle_ShouldCreateNewUser_WhenUserIsValid()
    {
        //Arrange
        var command = new CreateUserCommand
        (
            Name: "Nelson",
            LastName: "Valverde",
            Email: "nelsonvalverdelt@outlook.com",
            Birthday: new DateOnly(1992, 02, 19),
            Password: PasswordHashService.Encrypt("MiNuevoUsuario"),
            PhoneNumber: "9124587"
        );

        //Act
        var handler = new CreateUserCommandHandler(EfUnitOfWork, PasswordHashService);
        var result = await handler.Handle(command, CancellationToken.None);
        var userId = result.Data;
        if (userId is not null) SharedContext.SetOverrideId(userId);
        //Assert
        Assert.True(result.Succeeded);
    }

    [Fact, TestPriority(2)]
    public async Task Handle_ShouldGetUser_ById()
    {
        //Arrange
        var query = new GetUserQuery(Id: SharedContext.Id);

        //Act
        var handler = new GetUserQueryHandler(EfUnitOfWork);
        var result = await handler.Handle(query, CancellationToken.None);

        //Assert
        Assert.True(result.Succeeded);
        Assert.NotNull(result.Data);
    }
}