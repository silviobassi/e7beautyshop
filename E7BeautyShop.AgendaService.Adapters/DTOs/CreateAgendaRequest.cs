namespace E7BeautyShop.AgendaService.Adapters.DTOs;

public record CreateAgendaRequest(
    DateTime StartAt,
    DateTime EndAt,
    Guid ProfessionalId,
    bool Weekday,
    bool Weekend
);