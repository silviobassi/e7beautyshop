using E7BeautyShop.AgendaService.Core.Entities;

namespace E7BeautyShop.AgendaService.Core.Services;

public abstract class AbstractValidatorOfficeHoursScheduled(IReadOnlyCollection<OfficeHour> officeHoursScheduled)
{
    protected readonly IReadOnlyCollection<OfficeHour> TimesScheduled =
        officeHoursScheduled ?? throw new ArgumentNullException(nameof(officeHoursScheduled));
    
    public abstract bool Validate();
    
}