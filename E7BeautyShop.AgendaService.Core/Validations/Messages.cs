using static E7BeautyShop.AgendaService.Core.Entities.OfficeHour;
using static E7BeautyShop.AgendaService.Core.ObjectsValue.ServiceDescription;

namespace E7BeautyShop.AgendaService.Core.Validations;

public static class Messages
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

    public const string NewTimeBefore =
        "Time to schedule plus duration cannot be greater than the first current time";

    public const string NewTimeAfter =
        "Time to schedule cannot be less than the first current time plus duration";
}