using E7BeautyShop.Appointment.Core.Entities;
using E7BeautyShop.Appointment.Core.Validations;

namespace E7BeautyShop.Appointment.Core.Services;

public class ValidatorTimeScheduling
{
    private IReadOnlyCollection<OfficeHour> OfficeHoursOrdered { get; }
    private OfficeHour TimeToSchedule { get; }

    public ValidatorTimeScheduling(IReadOnlyCollection<OfficeHour> officeHours, OfficeHour timeToSchedule)
    {
        ArgumentNullException.ThrowIfNull(nameof(officeHours));
        ArgumentNullException.ThrowIfNull(nameof(timeToSchedule));
        OfficeHoursOrdered = officeHours.OrderBy(of => of.DateAndHour).ToList().AsReadOnly();
        TimeToSchedule = timeToSchedule;
    }

    
    /*
     * Verificar se há apenas um item na lista
     * Verificar se há ao menos 2 itens na lista
     * Verificar dentro da lista pré ordenada se o horário a ser agendado é maior que o horário anterior e menor que o próximo horário
     * Verificar se o horário a ser agendado é menor que o primeiro item da lista
     * Verificar se i horário a ser agendado é maior que o último item da lista
     */
    public bool Validate()
    {
        return HasOneTimeOnly;
    }
    
    private bool HasOneTimeOnly => OfficeHoursOrdered.Count == 1;

    
}