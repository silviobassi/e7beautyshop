using E7BeautyShop.AgendaService.Core.Entities;

namespace E7BeautyShop.Appointment.Core.Services;

public abstract class AbstractValidatorOfficeHoursScheduled(IReadOnlyCollection<OfficeHour> officeHoursScheduled)
{
    protected readonly IReadOnlyCollection<OfficeHour> OfficeHourScheduled =
        officeHoursScheduled ?? throw new ArgumentNullException(nameof(officeHoursScheduled));
    
    public abstract bool Validate();
    
}