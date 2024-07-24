using Test.WebApi.Data.Behaviours.Context;

namespace Test.WebApi.Data;

public class UserClaimRepositoryTest : RootTest
{
    public UserClaimRepositoryTest(SharedContext sharedContext) : base(sharedContext)
    {
        sharedContext.SetOverrideId("12edc7bb-efa6-4155-9352-cf5977e9dbc1");
    }

    [Fact]
    public async Task GetUserClaims_Should_Return_Any_Claims()
    {
        // Arrange
        var userId = SharedContext.Id;

        // Act
        var userClaims = await EfUnitOfWork.UserClaimRepository.GetUserClaims(userId);

        //Assert
        Assert.NotNull(userClaims);
        Assert.True(userClaims.Any());
    }
}