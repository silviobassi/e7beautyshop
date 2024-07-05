using static E7BeautyShop.AgendaService.Domain.Entities.OfficeHour;
using static E7BeautyShop.AgendaService.Domain.ObjectsValue.ServiceDescription;

namespace E7BeautyShop.AgendaService.Domain.Validations;

public static class Messages
{
    // ServiceDescription Error Messages
    public static readonly string NameTooLong = $"Name must be less than {MaxNameLength} characters";

    public const string PriceTooLow = "Price should be greater than zero";

    // OfficeHour Error Messages
    public static readonly string DurationTooShort =
        $"Duration cannot be less than {MinimumDuration} minutes";

    public const string TimeAlreadyAttend = "Time already attend";

    public const string FactoryNotInitialized =
        "Reserved registered event factory is not initialized";

    public const string DurationTooLow = "Duration cannot be less than or equal to zero";

    // Agenda Error Messages
    public const string StartAtTooLow = "Start at cannot be less than end at";

    // WeekDayOrWeekend Error Messages
    public const string StartAtTooHigh = "StartAt cannot be greater than EndAt";

    // HasUniqueItemValid Error Messages
    public const string NewTimeBefore = "Time to schedule plus duration cannot be greater than the first current time";

    public const string NewTimeAfter = "Time to schedule cannot be less than the first current time plus duration";

    // HasAtLeastOneItemValid Error Messages
    public const string NewTimeBeforeNext = "New time should be less or equal a next time";

    public const string NewTimeAfterPrev = "New time should be greater or equal a previous time";

    public const string NewTimeBetweenPrevNext =
        "The new time plus duration should be less or equal to the next time " +
        "and bigger or equal to the previous time plus duration.";
}