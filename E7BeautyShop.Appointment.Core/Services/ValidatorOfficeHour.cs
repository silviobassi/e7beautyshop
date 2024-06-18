using E7BeautyShop.Appointment.Core.Entities;
using E7BeautyShop.Appointment.Core.Validations;

namespace E7BeautyShop.Appointment.Core.Services;

public class ValidatorOfficeHour(OfficeHour officeHour, OfficeHour? previousOfficeHour, OfficeHour? nextOfficeHour)
{
    private const int MinimumMinutesGap = 60;

    public void ValidatePreviousOfficeHour()
    {
        if (previousOfficeHour == null) return;
        BusinessException.When(officeHour.GetEndTime() > previousOfficeHour.DateAndHour,
            "Office hour is already attended");
    }

    public void ValidateNextOfficeHour()
    {
        if (nextOfficeHour == null || previousOfficeHour == null) return;

        var totalMinutes = CalculateTimeBetween(previousOfficeHour, nextOfficeHour);
        const int divisor = MinimumMinutesGap / 2;
        var result = (totalMinutes - MinimumMinutesGap) / divisor;

        const string errorMessage = "Office hour cannot be less than 60 minutes between previous and next office hour";
        BusinessException.When(IsInvalidGap(result, officeHour.Duration), errorMessage);
    }
    
    private static int CalculateTimeBetween(OfficeHour previous, OfficeHour next)
    {
        return (int)(next.DateAndHour - previous.DateAndHour).TotalMinutes;
    }

    private static bool IsInvalidGap(int result, int duration)
    {
        return result < 2 && result / duration < 2;
    }
}