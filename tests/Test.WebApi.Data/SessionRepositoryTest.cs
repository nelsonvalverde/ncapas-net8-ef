using Test.WebApi.Data.Behaviours.Context;
using WebApi.Entities.Session.Dtos;
using WebApi.Entities.Session.Enums;

namespace Test.WebApi.Data;

[TestCaseOrderer(
    ordererTypeName: "Test.WebApi.Data.Behaviours.TestPriority.PriorityOrderer",
    ordererAssemblyName: "Test.WebApi.Data")]
public class SessionRepositoryTest(SharedContext sharedContext) : RootTest(sharedContext)
{
    [Fact, TestPriority(1)]
    public async Task CreateSession_Should_Return_TaskSuccess()
    {
        //Arrange
        var firstPageFromUsers = await EfUnitOfWork.UserRepository.GetPageAsync(pageNumber: 1, pageSize: 1);
        var user = firstPageFromUsers.List.FirstOrDefault();
        SharedContext.SetOverrideId(user?.Id ?? string.Empty);

        var refreshToken = Guid.NewGuid().ToString("D");
        var createSession = new CreateSessionDto(
            Id: Guid.NewGuid().ToString("D"),
            UserId: SharedContext.Id,
            Token: "MiNuevoToken123",
            RefreshToken: refreshToken,
            StatusId: SessionStatus.Active,
            Expires: DateTime.UtcNow.AddMinutes(60)
         );

        //Act
        var createSessionTask = UnitOfWork.SessionRepository.CreateAsync(createSession);
        var createAsync = await Record.ExceptionAsync(() => createSessionTask);
        await EfUnitOfWork.SaveChangesAsync();

        //Assert
        Assert.Null(createAsync);

        //Task
        SharedContext.SetRefreshToken(refreshToken);
    }

    [Fact, TestPriority(2)]
    public async Task GetSession_Should_Return_Session()
    {
        //Arrange
        var userId = SharedContext.Id;
        var refreshToken = SharedContext.RefreshToken ?? string.Empty;
        var token = "MiNuevoToken123";
        //Act
        var session = await UnitOfWork.SessionRepository.GetAsync(userId, refreshToken);

        //Assert
        Assert.NotNull(session);
        Assert.Equal(userId, session.UserId);
        Assert.Equal(refreshToken, session.RefreshToken);
        Assert.Equal(token, session.Token);
    }
}