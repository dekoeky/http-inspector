using HttpInspector.Dtos;

namespace HttpInspector.Tests.Dtos;

[TestClass]
public sealed class AboutDtoTests
{
    /// <summary>
    /// Validates if <see cref="AboutDto.Singleton"/> gets instantiated correctly.
    /// </summary>
    [TestMethod]
    public void ValidateSingleton()
    {
        //ACT
        var dto = AboutDto.Singleton;

        //ASSERT
        Assert.IsNotNull(dto);
    }
}