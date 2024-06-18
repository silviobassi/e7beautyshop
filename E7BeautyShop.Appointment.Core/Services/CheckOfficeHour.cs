using E7BeautyShop.Appointment.Core.Entities;
using E7BeautyShop.Appointment.Core.Validations;

namespace E7BeautyShop.Appointment.Core.Services;

public class CheckOfficeHour(IReadOnlyCollection<OfficeHour> officeHours, OfficeHour officeHour)
{
    public void WhenPrevious(ref OfficeHour? previous)
    {
        PreviousNextProcess().TryGetValue($"Previous", out previous);
        if (previous is null) return;
        BusinessException.When(officeHour.GetEndTime() > previous.DateAndHour, "Office hour is already attended");
    }

    public void WhenNext(ref OfficeHour? next)
    {
        PreviousNextProcess().TryGetValue("Next", out next);
        PreviousNextProcess().TryGetValue("Previous", out var previous);

        if (next is null || previous is null) return;

        var nextBetweenPrevious = next.DateAndHour.Subtract(previous.DateAndHour);
        var totalMinutes = (int)nextBetweenPrevious.TotalMinutes;
        const int baseMinutes = 60;
        const int divisor = baseMinutes / 2;
        var result = (totalMinutes - baseMinutes) / divisor;

        const string message = "Office hour cannot be less than 60 minutes between previous and next office hour";
        BusinessException.When(result < 2 && result / officeHour.Duration < 2, message);
    }

    private Dictionary<string, OfficeHour> PreviousNextProcess()
    {
        var result = new Dictionary<string, OfficeHour>();
        var (previous, next) = GetPreviousAndNext();
        MountPreviousNext(previous, next, result);
        return result;
    }

    private static void MountPreviousNext(OfficeHour? previous, OfficeHour? next, Dictionary<string, OfficeHour> result)
    {
        if (previous is not null || next is not null) ToAssemblerPreviousAndNext(previous, next, result);
        else if (previous is not null) ToAssemblerPrevious(previous, result);
        else if (next is not null) ToAssemblerNext(next, result);
    }

    private static void ToAssemblerPreviousAndNext(OfficeHour? previous, OfficeHour? next,
        Dictionary<string, OfficeHour> result)
    {
        result.Add("Previous", previous!);
        result.Add("Next", next!);
    }

    private static void ToAssemblerPrevious(OfficeHour? previous, Dictionary<string, OfficeHour> result) =>
        result.Add("Previous", previous!);
    
    private static void ToAssemblerNext(OfficeHour? next, Dictionary<string, OfficeHour> result) =>
        result.Add("Next", next!);

    private (OfficeHour? previous, OfficeHour? next) GetPreviousAndNext()
    {
        var orderedOfficeHours = officeHours.OrderBy(oh => oh.DateAndHour).ToList();
        return (GetPrevious(orderedOfficeHours), GetNext(orderedOfficeHours));
    }

    private OfficeHour? GetPrevious(List<OfficeHour> orderedOfficeHours) =>
        orderedOfficeHours.Find(ooh => ooh.GetEndTime() < officeHour.GetEndTime());

    private OfficeHour? GetNext(List<OfficeHour> orderedOfficeHours) =>
        orderedOfficeHours.Find(ooh => ooh.GetEndTime() > officeHour.GetEndTime());
}