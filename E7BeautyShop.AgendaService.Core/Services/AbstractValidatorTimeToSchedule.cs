using E7BeautyShop.AgendaService.Core.Entities;

namespace E7BeautyShop.AgendaService.Core.Services;

public abstract class AbstractValidatorTimeToSchedule(
    IReadOnlyCollection<OfficeHour> timesScheduled,
    OfficeHour newTime)
    : AbstractValidatorOfficeHoursScheduled(timesScheduled.OrderBy(of => of.DateAndHour).ToList())
{
    protected readonly OfficeHour TimeToSchedule =
        newTime ?? throw new ArgumentNullException(nameof(newTime));

    protected bool IsTimeScheduledBefore =>
        TimeToSchedule.DateAndHour < TimesScheduled.First().DateAndHour;
    
    protected bool IsTimeScheduledAfter =>
        TimeToSchedule.DateAndHour > TimesScheduled.First().DateAndHour;
}