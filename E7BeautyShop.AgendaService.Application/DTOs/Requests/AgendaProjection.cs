namespace E7BeautyShop.AgendaService.Application.DTOs.Requests;

public record AgendaProjection
{
    public DateTime StartAt { get; set; }
    public DateTime EndAt { get; set; }
    public Guid? ProfessionalId { get; set; }
    public TimeSpan WeekdayStartAt { get; set; }

    public TimeSpan WeekdayEndAt { get; set; }

    public TimeSpan WeekendStartAt { get; set; }

    public TimeSpan WeekendEndAt { get; set; }
    public List<DayRestRequest>? DaysRest { get; set; }
}