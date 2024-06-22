using E7BeautyShop.Appointment.Core.Entities;

namespace E7BeautyShop.Appointment.Core.Services;

public abstract class ValidatorAbstract
{
    protected readonly IReadOnlyCollection<OfficeHour> _officeHoursOrdered;
    protected readonly OfficeHour _timeToSchedule;
    
    protected ValidatorAbstract(IReadOnlyCollection<OfficeHour> officeHours, OfficeHour timeToSchedule)
    {
        ArgumentNullException.ThrowIfNull(nameof(officeHours));
        ArgumentNullException.ThrowIfNull(nameof(timeToSchedule));
        _officeHoursOrdered = officeHours.OrderBy(of => of.DateAndHour).ToList().AsReadOnly();
        _timeToSchedule = timeToSchedule;
    }
    
    public abstract bool Validate();
}