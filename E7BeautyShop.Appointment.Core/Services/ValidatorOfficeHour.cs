using E7BeautyShop.Appointment.Core.Entities;
using E7BeautyShop.Appointment.Core.Validations;

namespace E7BeautyShop.Appointment.Core.Services;

public class ValidatorOfficeHour
{
    private List<OfficeHour> OfficeHoursOrdered { get; set; }
    private OfficeHour TimeToSchedule { get; init; }

    public ValidatorOfficeHour(IReadOnlyCollection<OfficeHour> officeHours, OfficeHour timeToSchedule)
    {
        ArgumentNullException.ThrowIfNull(nameof(officeHours));
        ArgumentNullException.ThrowIfNull(nameof(timeToSchedule));
        OfficeHoursOrdered = officeHours.OrderBy(of => of.DateAndHour).ToList();
        TimeToSchedule = timeToSchedule;
    }

    // Get the previous time scheduled
    public OfficeHour PreviousTimeScheduled =>
        OfficeHoursOrdered.First(of => of.DateAndHour < TimeToSchedule.DateAndHour)!;

    // Get the next time scheduled 
    public OfficeHour NextTimeScheduled =>
        OfficeHoursOrdered.Last(of => of.DateAndHour > TimeToSchedule.DateAndHour)!;
    
    // Check if there is only one schedule
    public bool IsOnlyOneOfficeHourScheduled => OfficeHoursOrdered.Count == 1;

    // Check if there are two schedules
    public bool IsTwoOfficeHourScheduled => OfficeHoursOrdered.Count >= 2;
    
    public bool IsBiggerOrEqualTo => 
        NextTimeScheduled.DateAndHour.Subtract(PreviousTimeScheduled.DateAndHour).TotalMinutes >= 60;


    // Check if there is a scheduling before the current scheduling
    public bool IsPreviousOfficeHourScheduled()
    {
        BusinessException.When(!IsOnlyOneOfficeHourScheduled, "There is not only one OfficeHour scheduled");
        return OfficeHoursOrdered.Exists(of => of.DateAndHour < TimeToSchedule.DateAndHour);
    }

    // Check if there is a scheduling after the current scheduling
    public bool IsNextOfficeHourScheduled()
    {
        BusinessException.When(!IsOnlyOneOfficeHourScheduled, "There is not only one OfficeHour scheduled");
        return OfficeHoursOrdered.Exists(of => of.DateAndHour > TimeToSchedule.DateAndHour);
    }

    // Check if the time to be scheduled is greater than the previous scheduling time by adding the date and time + duration
    public bool IsOfficeHourGreaterThanCurrentOfficeHour()
    {
        BusinessException.When(!IsOnlyOneOfficeHourScheduled, "There is not only one OfficeHour scheduled");
        BusinessException.When(IsNextOfficeHourScheduled(), "There is a next OfficeHour scheduled");
        return OfficeHoursOrdered.Exists(of => TimeToSchedule.DateAndHour >= of.GetEndTime());
    }

    // Check if the time to be scheduled is less than previous scheduling time
    public bool IsOfficeHourLessThanCurrentOfficeHour()
    {
        BusinessException.When(!IsOnlyOneOfficeHourScheduled, "There is not only one OfficeHour scheduled");
        BusinessException.When(IsPreviousOfficeHourScheduled(), "There is a previous OfficeHour scheduled");
        return OfficeHoursOrdered.Exists(of => TimeToSchedule.GetEndTime() <= of.DateAndHour);
    }
}