using System.Text.Json.Serialization;
using E7BeautyShop.AgendaService.Domain.Entities;
using MediatR;

namespace E7BeautyShop.AgendaService.Application.Commands;

public abstract class AgendaCommand : IRequest<Agenda>
{
    public DateTime StartAt { get; set; }
    public DateTime EndAt { get; set; }
    public Guid? ProfessionalId { get; set; }
    public TimeSpan WeekdayStartAt { get; set; }

    public TimeSpan WeekdayEndAt { get; set; }

    public TimeSpan WeekendStartAt { get; set; }

    public TimeSpan WeekendEndAt { get; set; }
    public List<DayRestCommand>? DaysRest { get; set; }
}

public class DayRestCommand
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public DayOfWeek DayOnWeek { get; set; }
}