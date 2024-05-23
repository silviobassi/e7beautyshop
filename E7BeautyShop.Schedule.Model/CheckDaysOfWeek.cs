namespace E7BeautyShop.Schedule;

public abstract class CheckDaysOfWeek
{
    protected static void Validate(TimeSpan start, TimeSpan end)
    {
        BusinessException.When(start == TimeSpan.Zero, "StartAt cannot be zero");
        BusinessException.When(end == TimeSpan.Zero, "EndAt cannot be zero");
        BusinessException.When(start >= end, "StartAt must be less than EndAt");
    }
}
    