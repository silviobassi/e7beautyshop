using System.Text.Json.Serialization;

namespace E7BeautyShop.AgendaService.Application.DTOs;

public record DayRestProjection
{
    [property: JsonConverter(typeof(JsonStringEnumConverter))]
    public DayOfWeek? DayOnWeek { get; init; }
}