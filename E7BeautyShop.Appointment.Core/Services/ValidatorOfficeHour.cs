using E7BeautyShop.Appointment.Core.Entities;
using E7BeautyShop.Appointment.Core.Validations;

namespace E7BeautyShop.Appointment.Core.Services;

public class ValidatorOfficeHour
{
    private readonly OfficeHour _officeHour;
    private readonly OfficeHour? _previousOfficeHour;
    private readonly OfficeHour? _nextOfficeHour;
    private const int MinimumMinutesGap = 60;
    
    public ValidatorOfficeHour(OfficeHour officeHour, OfficeHour? previousOfficeHour, OfficeHour? nextOfficeHour)
    {
        _officeHour = officeHour;
        _previousOfficeHour = previousOfficeHour;
        _nextOfficeHour = nextOfficeHour;
    }
    
    public void ValidatePreviousOfficeHour()
    {
        if (_previousOfficeHour == null) return;
        BusinessException.When(_officeHour.GetEndTime() > _previousOfficeHour.DateAndHour,
            "Office hour is already attended");
    }

    public void ValidateNextOfficeHour()
    {
        if (_nextOfficeHour == null || _previousOfficeHour == null) return;

        var totalMinutes = CalculateTimeBetween(_previousOfficeHour, _nextOfficeHour);
        const int divisor = MinimumMinutesGap / 2;
        var result = (totalMinutes - MinimumMinutesGap) / divisor;

        const string errorMessage = "Office hour cannot be less than 60 minutes between previous and next office hour";
        BusinessException.When(IsInvalidGap(result, _officeHour.Duration), errorMessage);
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