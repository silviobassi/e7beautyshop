namespace E7BeautyShop.Domain;

public class Schedule
{
    public Guid Id { get; private set; }
    public DateTime StartAt { get; private set; }
    public DateTime EndAt { get; private set; }

    public List<OfficeDay> OfficeDays { get; private set; } = [];

    public Schedule(DateTime startAt, DateTime endAt)
    {
        StartAt = startAt;
        EndAt = endAt;
    }

    public void Generate(OfficeDay officeDay)
    {
        officeDay.InformDate(StartAt);
        AddAllDays(officeDay);
    }

    private void AddAllDays(OfficeDay officeDay)
    {
        var daysBetweenDates = EndAt.Subtract(StartAt).Days;
        for (var i = 0; i <= daysBetweenDates; i++)
        {
            officeDay.IncrementDate(i);
            officeDay.Generate();
            OfficeDays.Add(officeDay);
        }
    }
}