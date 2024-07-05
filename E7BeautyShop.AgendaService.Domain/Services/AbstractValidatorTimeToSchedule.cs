using E7BeautyShop.AgendaService.Domain.Entities;

namespace E7BeautyShop.AgendaService.Domain.Services;

public abstract class AbstractValidatorTimeToSchedule(
    IReadOnlyCollection<OfficeHour> timesScheduled,
    OfficeHour newTime)
    : AbstractValidatorOfficeHoursScheduled(timesScheduled.OrderBy(of => of.DateAndHour).ToList())
{
    protected readonly OfficeHour NewTime =
        newTime ?? throw new ArgumentNullException(nameof(newTime));

    protected bool IsLessThan => NewTime.DateAndHour < TimesScheduled.First().DateAndHour;

    protected bool IsGreaterThan => NewTime.DateAndHour > TimesScheduled.Last().DateAndHour;
}