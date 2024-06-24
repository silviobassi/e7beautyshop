using E7BeautyShop.Appointment.Core.Validations;

namespace E7BeautyShop.Appointment.Core.ObjectsValue;

public record WeekDayOrWeekend
{
    public TimeSpan? StartAt { get; private set; }
    public TimeSpan? EndAt { get; private set; }
    
    protected WeekDayOrWeekend()
    {
    }
    protected WeekDayOrWeekend(TimeSpan? startAt, TimeSpan? endAt)
    {
        StartAt = startAt;
        EndAt = endAt;
        Validate();
    }

    private void Validate()
    {
        ArgumentNullException.ThrowIfNull(StartAt);
        ArgumentNullException.ThrowIfNull(EndAt);
        BusinessException.When(StartAt >= EndAt, "StartAt cannot be greater than EndAt");
    }
}