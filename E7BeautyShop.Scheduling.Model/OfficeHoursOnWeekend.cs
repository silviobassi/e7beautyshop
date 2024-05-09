namespace E7BeautyShop.Domain;

public class OfficeHoursOnWeekend
{
    public TimeSpan StartAt { get; private set; }
    public TimeSpan EndAt { get; private set; }

    public OfficeHoursOnWeekend(TimeSpan startAt, TimeSpan endAt)
    {
        StartAt = startAt;
        EndAt = endAt;
    }
}