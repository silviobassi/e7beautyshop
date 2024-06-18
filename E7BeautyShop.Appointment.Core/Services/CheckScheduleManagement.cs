using E7BeautyShop.Appointment.Core.Entities;

namespace E7BeautyShop.Appointment.Core.Services;

public class CheckScheduleManagement
{
    public static bool VerifyIfOnlyOneOfficeHourScheduled(IReadOnlyCollection<OfficeHour> officeHours) =>
        officeHours.Count == 1;
}