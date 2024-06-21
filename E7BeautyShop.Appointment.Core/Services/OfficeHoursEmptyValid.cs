using E7BeautyShop.Appointment.Core.Entities;

namespace E7BeautyShop.Appointment.Core.Services;

public class OfficeHoursEmptyValid : IValidator
{
    private readonly IReadOnlyCollection<OfficeHour> _officeHoursOrdered;

    public OfficeHoursEmptyValid(IReadOnlyCollection<OfficeHour> officeHours)
    {
        ArgumentNullException.ThrowIfNull(nameof(officeHours));
        _officeHoursOrdered = officeHours.OrderBy(of => of.DateAndHour).ToList().AsReadOnly();
    }

    public bool Validate()
    {
        return _officeHoursOrdered.Count == 0;
    }
}