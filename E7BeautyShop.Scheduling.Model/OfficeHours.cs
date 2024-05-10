namespace E7BeautyShop.Domain;

public class OfficeHours
{
    public TimeSpan StartAt { get; private set; }
    public TimeSpan EndAt { get; private set; }

    public OfficeHours(TimeSpan startAt, TimeSpan endAt)
    {
        StartAt = startAt;
        EndAt = endAt;
    }
}