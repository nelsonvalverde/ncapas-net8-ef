using Test.WebApi.Data.Behaviours.Context;
using WebApi.Entities.Error.Dto;

namespace Test.WebApi.Data;

[TestCaseOrderer(
    ordererTypeName: "Test.WebApi.Data.Behaviours.TestPriority.PriorityOrderer",
    ordererAssemblyName: "Test.WebApi.Data")]
public class ErrorRepositoryTest(SharedContext sharedContext) : RootTest(sharedContext)
{
    [Fact, TestPriority(1)]
    public async Task CreateErrorAsync_Should_Return_TaskSuccess()
    {
        // Arrange
        var createError = new CreateErrorDto
        (
            App: "WebApi",
            Type: "System.Error.Test",
            Message: "Error de sistema Test",
            Body: "{param1: test}",
            Method: "POST",
            Path: "/users",
            QueryString: "",
            UserAgent: "Agent...",
            StackTrace: "Stacktrace: "
        );

        // Act
        var createErrorTask =  UnitOfWork.ErrorRepository.CreateError(createError);
        var createAsync = await Record.ExceptionAsync(() => createErrorTask);
        
        //Assert
        Assert.Null(createAsync);   
    }

    [Fact, TestPriority(1)]
    public async Task ClearErrorsAsync_Should_Return_TaskSuccess()
    {
        // Arrange
        
        // Act
        var clearErrorTask = UnitOfWork.ErrorRepository.ClearErrors();
        var createAsync = await Record.ExceptionAsync(() => clearErrorTask);

        //Assert
        Assert.Null(createAsync);
    }
}