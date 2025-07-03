using HttpInspector.Dtos;
using HttpInspector.TestData._Internal;
using System.Reflection;

namespace HttpInspector.TestData;

/// <summary>
/// Types that should be JSON-serializable.
/// </summary>
internal class TypesToBeSerializedAttribute : TestDataSourceAttribute<Type>
{
    protected override IEnumerable<Type> GetDataInternal(MethodInfo _)
    {
        yield return typeof(AboutDto);
        yield return typeof(ConnectionInfoDto);
        yield return typeof(DiagnosticInfoDto);
        yield return typeof(EnvironmentInfoDto);
        yield return typeof(RequestInfoDto);
        yield return typeof(RequestMetadataDto);
        yield return typeof(RouteEndpointDto);
    }
}