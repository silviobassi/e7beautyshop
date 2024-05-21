namespace E7BeautyShop.Domain;

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
        ModelBusinessException.When(startAt == null, "StartAt is required");
        ModelBusinessException.When(startAt <= TimeSpan.FromHours(0), "Hour must be greater than 0");
        ModelBusinessException.When(endAt == null, "EndAt is required");
        ModelBusinessException.When(startAt >= endAt, "StartAt must be less than EndAt");
    }
}