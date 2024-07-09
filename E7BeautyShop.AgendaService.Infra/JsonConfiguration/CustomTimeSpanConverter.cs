using System.Text.Json;
using System.Text.Json.Serialization;

namespace E7BeautyShop.AgendaService.Infra.JsonConfiguration;

public class CustomTimeSpanConverter : JsonConverter<TimeSpan>
{
    public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var timeSpanString = reader.GetString();
        return TimeSpan.ParseExact(timeSpanString!, "h\\:mm\\:ss", System.Globalization.CultureInfo.InvariantCulture);
    }

    public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString("h\\:mm\\:ss"));
    }
}