namespace Test.WebApi.Data.Behaviours.TestPriority;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class TestPriorityAttribute(int priority) : Attribute
{
    public int Priority { get; private set; } = priority;
}