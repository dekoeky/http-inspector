using System.Reflection;

namespace HttpInspector.TestData._Internal;

[AttributeUsage(AttributeTargets.Method)]
public abstract class TestDataSourceAttribute : Attribute, ITestDataSource
{
    public abstract IEnumerable<object?[]> GetData(MethodInfo methodInfo);

    public virtual string? GetDisplayName(MethodInfo methodInfo, object?[]? data) =>
        data is null
            ? methodInfo.Name
            : $"{methodInfo.Name} ({string.Join(", ", data)})";
}

public abstract class TestDataSourceAttribute<T> : TestDataSourceAttribute
{
    protected abstract IEnumerable<T> GetDataInternal(MethodInfo methodInfo);

    public sealed override IEnumerable<object?[]> GetData(MethodInfo methodInfo) =>
        GetDataInternal(methodInfo).Select(d => (object?[])[d]);
}