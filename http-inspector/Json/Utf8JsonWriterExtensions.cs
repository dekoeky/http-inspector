using System.Text.Json;

namespace HttpInspector.Json;

public static class Utf8JsonWriterExtensions
{
    public static void TryToSerialize(this Utf8JsonWriter jsonWriter, string propertyName, object? value)
    {
        if (value is null)
        {
            jsonWriter.WriteNull(propertyName);
            return;
        }

        var type = value.GetType();

        if (AppJsonSerializerContext.Default.GetTypeInfo(type) is not { } typeInfo)
        {
            jsonWriter.WriteString(propertyName, $"Serialization for {type.FullName} is not supported");
            return;
        }

        jsonWriter.WritePropertyName(propertyName);
        JsonSerializer.Serialize(jsonWriter, value, typeInfo);
    }
}