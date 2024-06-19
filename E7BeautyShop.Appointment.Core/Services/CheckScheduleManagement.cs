using E7BeautyShop.Appointment.Core.Entities;
using E7BeautyShop.Appointment.Core.Validations;

namespace E7BeautyShop.Appointment.Core.Services;

public  class CheckScheduleManagement(IReadOnlyCollection<OfficeHour> officeHours, OfficeHour officeHour)
{
    public bool VerifyIfOnlyOneOfficeHourScheduled => officeHours.Count == 1;

    public bool VerifyIfPreviousOfficeHourScheduled()
    {
        BusinessException.When(!VerifyIfOnlyOneOfficeHourScheduled, "There is not only one OfficeHour scheduled");
        return officeHours.Any(of => of.DateAndHour < officeHour.DateAndHour);
    }
}