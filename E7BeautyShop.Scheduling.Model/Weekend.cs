public class Weekend
{
    public TimeSpan StartAt { get; private set; }
    public TimeSpan EndAt { get; private set; }

    public Weekend(TimeSpan startAt, TimeSpan endAt
    )
    {
        StartAt = startAt;
        EndAt = endAt;
    }
}