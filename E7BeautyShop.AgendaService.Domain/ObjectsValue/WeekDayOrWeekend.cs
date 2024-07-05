using E7BeautyShop.AgendaService.Core.Validations;
using static E7BeautyShop.AgendaService.Core.Validations.Messages;

namespace E7BeautyShop.AgendaService.Core.ObjectsValue;

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
        BusinessException.ThrowIf(StartAt >= EndAt, StartAtTooHigh);
    }
}