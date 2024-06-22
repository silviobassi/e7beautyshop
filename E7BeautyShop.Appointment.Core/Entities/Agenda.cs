using E7BeautyShop.Appointment.Core.ObjectsValue;
using E7BeautyShop.Appointment.Core.Services;
using E7BeautyShop.Appointment.Core.Validations;

namespace E7BeautyShop.Appointment.Core.Entities;

public sealed class Agenda : Entity, IAggregateRoot
{
    public DateTime StartAt { get; private set; }
    public DateTime EndAt { get; private set; }
    public ProfessionalId? ProfessionalId { get; private set; }
    public Weekday? Weekday { get; private set; }
    public Weekend? Weekend { get; private set; }

    private readonly List<OfficeHour> _officeHours = [];
    private readonly List<DayRest> _daysRest = [];
    public IReadOnlyCollection<OfficeHour> OfficeHours => _officeHours.AsReadOnly();
    public IReadOnlyCollection<DayRest> DaysRest => _daysRest.AsReadOnly();

    public Agenda()
    {
    }

    private Agenda(DateTime startAt, DateTime endAt, ProfessionalId? professionalId, Weekday weekday,
        Weekend weekend)
    {
        Id = Guid.NewGuid();
        StartAt = startAt;
        EndAt = endAt;
        ProfessionalId = professionalId;
        Weekday = weekday;
        Weekend = weekend;
        Validate();
    }

    public static Agenda Create(DateTime startAt, DateTime endAt, ProfessionalId? professionalId, Weekday weekday,
        Weekend weekend) => new(startAt, endAt, professionalId, weekday, weekend);

    public void AddDayRest(DayRest dayRest) => _daysRest.Add(dayRest);

    public void RemoveDayRest(DayRest dayRest) => _daysRest.Remove(dayRest);

    public void AddOfficeHour(OfficeHour officeHour)
    {
        if (IsDayRest(officeHour)) officeHour.IsAvailable = false;
        _officeHours.Add(officeHour);
    }

    public void RemoveOfficeHour(OfficeHour officeHour)
    {
        _officeHours.Remove(officeHour);
    }

    private bool IsDayRest(OfficeHour officeHour)
    {
        var existsDayRest = _daysRest.Exists(dr => dr.DayOnWeek == officeHour.DateAndHour.DayOfWeek);
        return existsDayRest && DaysRest.Count > 0;
    }

    public bool IsWeekday(OfficeHour officeHour) => !IsWeekend(officeHour);

    private bool IsWeekend(OfficeHour? officeHour)
        => officeHour?.DateAndHour.DayOfWeek is DayOfWeek.Sunday or DayOfWeek.Saturday;


    private void Validate()
    {
        BusinessException.When(StartAt == DateTime.MinValue, "StartAt cannot be empty");
        BusinessException.When(EndAt == DateTime.MinValue, "EndAt cannot be empty");
        BusinessNullException.When(ProfessionalId is null, nameof(ProfessionalId));
        BusinessNullException.When(Weekday is null, nameof(Weekday));
        BusinessNullException.When(Weekend is null, nameof(Weekend));
    }

    public void Update(Guid id, DateTime startAt, DateTime endAt, ProfessionalId? professionalId, Weekday weekday,
        Weekend weekend)
    {
        Id = id;
        StartAt = startAt;
        EndAt = endAt;
        ProfessionalId = professionalId;
        Weekday = weekday;
        Weekend = weekend;
    }
}