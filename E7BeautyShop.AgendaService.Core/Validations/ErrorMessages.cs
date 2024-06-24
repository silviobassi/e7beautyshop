using static E7BeautyShop.AgendaService.Core.Entities.OfficeHour;
using static E7BeautyShop.Appointment.Core.ObjectsValue.ServiceDescription;

namespace E7BeautyShop.Appointment.Core.Validations;

public static class ErrorMessages
{
    // ServiceDescription Error Messages
    public static readonly string NameShouldLessThanInformed = $"Name must be less than {MaxNameLength} characters";
    
    public const string PriceShouldGreaterThanZero = "Price should be greater than zero";

    // OfficeHour Error Messages
    public static readonly string DurationCannotLessThanInformed =
        $"Duration cannot be less than {MinimumDuration} minutes";

    public const string TimeAlreadyAttend = "Time already attend";

    public const string ReservedRegisteredEventFactoryIsNotInitialized =
        "Reserved registered event factory is not initialized";

    public const string DurationCannotLessOrEqualZero = "Duration cannot be less than or equal to zero";

    // Agenda Error Messages
    public const string StartAtLessThanEndAt = "Start at cannot be less than end at";
    
    // WeekDayOrWeekend Error Messages
    public const string StartAtCannotGreaterThanEndAt = "StartAt cannot be greater than EndAt";
}