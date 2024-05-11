namespace E7BeautyShop.Domain;

public class Schedule
{
    public Guid Id { get; private set; }
    public List<IOfficeDay?> OfficeDays { get; private set; } = [];
    private DateTime? StartAt { get; set; }
    private DateTime? EndAt { get; set; }
    
    public Schedule(DateTime? startAt, DateTime? endAt)
    {
        ModelBusinessException.When(startAt == null, "StartAt is required");
        ModelBusinessException.When(startAt < DateTime.MinValue, "StartAt value not valid");
        ModelBusinessException.When(endAt == null, "EndAt is required");
        ModelBusinessException.When(startAt >= endAt, "StartAt must be less than EndAt");
        StartAt = startAt;
        EndAt = endAt;
    }

    public void Generate(IOfficeDay? officeDay)
    {
        ModelBusinessException.When(officeDay == null, "OfficeDay is required");
        officeDay?.InformDate(StartAt);
        AddAllDays(officeDay!);
    }

    private void AddAllDays(IOfficeDay? officeDay)
    {
        ModelBusinessException.When(officeDay == null, "OfficeDay is required");
        var daysBetweenDates = (EndAt - StartAt)?.Days;
        for (var i = 0; i <= daysBetweenDates; i++)
        {
            officeDay?.IncrementDate(i);
            officeDay?.Generate();
            OfficeDays.Add(officeDay);
        }
    }
}