using E7BeautyShop.AgendaService.Core.Entities;

namespace E7BeautyShop.Appointment.Core.Services;

public class HasNoItemsValid(IReadOnlyCollection<OfficeHour> officeHoursScheduled)
    : AbstractValidatorOfficeHoursScheduled(officeHoursScheduled)
{
    private readonly IReadOnlyCollection<OfficeHour> _officeHoursScheduledOrdered = officeHoursScheduled;

    public override bool Validate() => _officeHoursScheduledOrdered.Count == 0;
}