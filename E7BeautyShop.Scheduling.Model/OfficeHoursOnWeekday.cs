namespace E7BeautyShop.Domain;

public class OfficeHoursOnWeekday
{
    public TimeSpan StartAt { get; private set; }
    public TimeSpan EndAt { get; private set; }

    public OfficeHoursOnWeekday(TimeSpan startAt, TimeSpan endAt)
    {
        StartAt = startAt;
        EndAt = endAt;
    }
}