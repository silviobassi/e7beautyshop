using E7BeautyShop.AgendaService.Core.Entities;

namespace E7BeautyShop.AgendaService.Core.Services;

public class HasNoItemsValid(IReadOnlyCollection<OfficeHour> timesScheduled)
    : AbstractValidatorOfficeHoursScheduled(timesScheduled)
{
    private readonly IReadOnlyCollection<OfficeHour> _timesScheduledOrdered = timesScheduled;

    public override bool Validate() => _timesScheduledOrdered.Count == 0;
}