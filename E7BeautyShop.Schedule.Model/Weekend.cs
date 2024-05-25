namespace E7BeautyShop.Schedule;

public sealed class Weekend
{

    public TimeSpan StartAt { get; private set; }
    public TimeSpan EndAt { get; private set; }

    public Weekend(TimeSpan start, TimeSpan end)
    {
        StartAt = start;
        EndAt = end;
        Validate();
    }

    public void Validate()
    {
        BusinessException.When(StartAt == TimeSpan.Zero, "StartAt cannot be empty");
        BusinessException.When(EndAt == TimeSpan.Zero, "EndAt cannot be empty");
        BusinessException.When(StartAt >= EndAt, "StartAt cannot be greater than EndAt");
    }
}