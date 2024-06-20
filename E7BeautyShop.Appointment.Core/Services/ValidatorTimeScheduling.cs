using E7BeautyShop.Appointment.Core.Entities;

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
     * Verificar se há apenas um item na lista 🎯
     * Verificar se há ao menos 2 itens na lista 🎯
     * Verificar dentro da lista pré ordenada se o horário a ser agendado é maior que o horário anterior e menor que o próximo horário 🎯
     * Verificar se o horário a ser agendado é menor que o primeiro item da lista 🎯
     * Verificar se i horário a ser agendado é maior que o último item da lista 🎯
     * O intervalo entre os horários agendados e a agendar deve ser ao menos 30 minutos
     * Verificar se o horário a agendar + duração é <= ao próximo horário 🎯
     * verificar se o horário a agendar é >= ao horário anterior + duração 🎯
     */
    public bool Validate()
    {
        var uniqueTime = HasUniqueTime && IsGreaterThanPreviousTime || IsLessThanNextTime;
        
        var atLeastTwoTimes = HasAtLeastTwoTimes && IsGreaterThanPreviousTime && IsLessThanNextTime;
        return atLeastTwoTimes;
    }

    private bool HasUniqueTime => OfficeHoursOrdered.Count == 1;

    private bool HasAtLeastTwoTimes => OfficeHoursOrdered.Count >= 2;

    private bool IsGreaterThanPreviousTime =>
        PreviousTime is not null && TimeToSchedule.DateAndHour > PreviousTime.DateAndHour;

    private bool IsLessThanNextTime => NextTime is not null && TimeToSchedule.DateAndHour < NextTime.DateAndHour;

    private OfficeHour? PreviousTime =>
        OfficeHoursOrdered.FirstOrDefault(of => of.DateAndHour < TimeToSchedule.DateAndHour);

    private OfficeHour? NextTime =>
        OfficeHoursOrdered.FirstOrDefault(of => of.DateAndHour > TimeToSchedule.DateAndHour);

    private bool IsLessThanFirstTime => OfficeHoursOrdered.First().DateAndHour > TimeToSchedule.DateAndHour;

    private bool IsGreaterThanLastTime => OfficeHoursOrdered.Last().DateAndHour < TimeToSchedule.DateAndHour;

    private bool IsTimePlusDurationLessThanNext =>
        NextTime is not null && TimeToSchedule.GetEndTime() < NextTime.DateAndHour;

    private bool IsPreviousPlusLessThanTime =>
        PreviousTime is not null && PreviousTime.GetEndTime() < TimeToSchedule.DateAndHour;
}