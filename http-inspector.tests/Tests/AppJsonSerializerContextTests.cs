using HttpInspector.Json;
using HttpInspector.TestData;

namespace HttpInspector.Tests;

/// <summary>
/// <see cref="AppJsonSerializerContext"/> related tests.
/// </summary>
[TestClass]
public sealed class AppJsonSerializerContextTests
{
    [TestMethod]
    public void WriteIndented()
    {
        //ASSERT
        Assert.IsTrue(AppJsonSerializerContext.Default.Options.WriteIndented);
    }

    [DataTestMethod]
    [TypesToBeSerialized]
    public void TypeInfoAvailable(Type type)
    {
        //ARRANGE
        var options = AppJsonSerializerContext.Default.Options;

        //ACT
        var typeInfoAvailable = options.TryGetTypeInfo(type, out _);

        //ASSERT
        Assert.IsTrue(typeInfoAvailable);
    }
}