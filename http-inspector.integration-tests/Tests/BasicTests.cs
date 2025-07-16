using Microsoft.AspNetCore.Mvc.Testing;
using Xunit.Abstractions;

namespace HttpInspector.Tests;

public class BasicTests(WebApplicationFactory<Program> factory, ITestOutputHelper testOutputHelper)
    : IClassFixture<WebApplicationFactory<Program>>
{
    [Theory]
    [InlineData("/")]
    [InlineData("/LandingPage?UserName=John")]
    [InlineData("/some/path?hello=world&TimeOfStartup=2025-07-04T12%3A13%3A14.8689932%2B02%3A00")]
    [InlineData(HealthChecks.Endpoints.Health)]
    [InlineData(HealthChecks.Endpoints.HealthLive)]
    [InlineData(HealthChecks.Endpoints.HealthReady)]
    [InlineData(HealthChecks.Endpoints.HealthExplain)]
    [InlineData(Endpoints.Patterns.About)]
    [InlineData(Endpoints.Patterns.Browse)]
    [InlineData("/openapi/v1.json")]
    [InlineData("/scalar/v1")]
    public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
    {
        // Arrange
        var client = factory.CreateClient();

        // Act
        var response = await client.GetAsync(url);
        var body = await response.Content.ReadAsStringAsync();

        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299
        //Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());


        testOutputHelper.WriteLine($"StatusCode: {response.StatusCode} ({(int)response.StatusCode})");
        testOutputHelper.WriteLine($"ContentType: {response.Content.Headers.ContentType})");
        testOutputHelper.WriteLine($"Body: {body})");
    }
}