﻿namespace E7BeautyShop.Schedule;

public sealed class Schedule : IAggregateRoot
{
    private readonly Weekday? _weekday;
    private readonly Weekend? _weekend;

    public Schedule(DateTime startAt, DateTime endAt, ProfessionalId professionalId, Weekday weekday, Weekend weekend)
    {
        StartAt = startAt;
        EndAt = endAt;
        ProfessionalId = professionalId;
        _weekday = weekday;
        _weekend = weekend;
        Validate();
    }

    public DateTime StartAt { get; private set; }

    public DateTime EndAt { get; private set; }

    public ProfessionalId? ProfessionalId { get; private set; }
    public List<DayRest> DaysRest { get; } = [];

    public List<OfficeHour> OfficeHours { get; } = [];

    public void AddDayRest(DayRest dayRest) => DaysRest.Add(dayRest);

    public void AddOfficeHour(OfficeHour officeHour)
    {
        if (IsDayRest(officeHour)) return;
        OfficeHours.Add(officeHour);
    }

    private bool IsDayRest(OfficeHour officeHour)
    {
        var existsDayRest = DaysRest.Exists(dr => dr.DayOnWeek == officeHour.DateAndHour.DayOfWeek);
        return existsDayRest && DaysRest.Count > 0;
    }

    public static bool IsWeekday(OfficeHour officeHour) => !IsWeekend(officeHour);

    private static bool IsWeekend(OfficeHour? officeHour)
        => officeHour?.DateAndHour.DayOfWeek is DayOfWeek.Sunday or DayOfWeek.Saturday;


    private void Validate()
    {
        BusinessException.When(StartAt == DateTime.MinValue, "StartAt cannot be empty");
        BusinessException.When(EndAt == DateTime.MinValue, "EndAt cannot be empty");
        BusinessNullException.When(ProfessionalId is null, nameof(ProfessionalId));
        BusinessNullException.When(_weekday is null, nameof(_weekday));
        BusinessNullException.When(_weekend is null, nameof(_weekend));
    }
}