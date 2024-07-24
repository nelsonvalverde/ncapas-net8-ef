using Xunit.Abstractions;
using Xunit.Sdk;

namespace Test.WebApi.Data.Behaviours.TestPriority;

public class PriorityOrderer : ITestCaseOrderer
{
    public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases) where TTestCase : ITestCase
    {
        var sortedTestCases = testCases
            .OrderBy(testCase =>
            {
                var priorityAttribute = testCase.TestMethod.Method
                    .GetCustomAttributes(typeof(TestPriorityAttribute))
                    .FirstOrDefault();
                return priorityAttribute != null
                             ? priorityAttribute.GetNamedArgument<int>("Priority")
                             : int.MaxValue;
            });

        return sortedTestCases;
    }
}