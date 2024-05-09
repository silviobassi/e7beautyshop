namespace E7BeautyShop.Domain;

public class OfficeHoursOnHoliday
{
    public TimeSpan StartAt { get; private set; }
    public TimeSpan EndAt { get; private set; }

    public OfficeHoursOnHoliday(TimeSpan startAt, TimeSpan endAt)
    {
        StartAt = startAt;
        EndAt = endAt;
    }
}