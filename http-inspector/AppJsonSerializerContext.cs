using HttpInspector.Dtos;
using System.Text.Json.Serialization;

namespace HttpInspector;

[JsonSerializable(typeof(RequestInfoDto))]
[JsonSerializable(typeof(AboutDto))]
[JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class AppJsonSerializerContext : JsonSerializerContext;