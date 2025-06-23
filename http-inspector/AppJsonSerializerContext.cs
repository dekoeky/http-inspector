using http_inspector.Dtos;
using System.Text.Json.Serialization;

namespace http_inspector;

[JsonSerializable(typeof(RequestInfoDto))]
[JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class AppJsonSerializerContext : JsonSerializerContext;