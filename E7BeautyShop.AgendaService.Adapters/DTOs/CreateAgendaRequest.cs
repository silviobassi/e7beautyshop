namespace E7BeautyShop.AgendaService.Adapters.DTOs;

public record CreateAgendaRequest
{
    public DateTime StartAt { get; init; }
    public DateTime EndAt { get; init; }
    public Guid ProfessionalId { get; init; }
    public bool Weekday { get; init; }
    public bool Weekend { get; init; }
}