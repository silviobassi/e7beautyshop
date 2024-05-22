namespace E7BeautyShop.Schedule;

public class Weekend
{
    public TimeSpan? StartAt { get; private set; }
    public TimeSpan? EndAt { get; private set; }

    public Weekend(TimeSpan? startAt, TimeSpan? endAt)
    {
        Validate(startAt, endAt);
        StartAt = startAt;
        EndAt = endAt;
    }

    private static void Validate(TimeSpan? startAt, TimeSpan? endAt)
    {
        BusinessException.When(startAt == null, "StartAt is required");
        BusinessException.When(startAt <= TimeSpan.FromHours(0), "Hour must be greater than 0");
        BusinessException.When(endAt == null, "EndAt is required");
        BusinessException.When(startAt >= endAt, "StartAt must be less than EndAt");
    }
}