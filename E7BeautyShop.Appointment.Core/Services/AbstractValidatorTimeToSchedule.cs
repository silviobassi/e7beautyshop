using E7BeautyShop.Appointment.Core.Entities;

namespace E7BeautyShop.Appointment.Core.Services;

public abstract class AbstractValidatorTimeToSchedule(IReadOnlyCollection<OfficeHour> officeHoursScheduled, OfficeHour timeToSchedule)
    : AbstractValidatorOfficeHoursScheduled(officeHoursScheduled.OrderBy(of => of.DateAndHour).ToList())
{
    protected readonly OfficeHour TimeToSchedule =
        timeToSchedule ?? throw new ArgumentNullException(nameof(timeToSchedule));

    
}