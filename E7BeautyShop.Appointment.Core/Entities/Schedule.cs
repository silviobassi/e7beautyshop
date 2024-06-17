using E7BeautyShop.Appointment.Core.ObjectsValue;
using E7BeautyShop.Appointment.Core.Validations;

namespace E7BeautyShop.Appointment.Core.Entities;

public sealed class Schedule : Entity, IAggregateRoot
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

    public Schedule()
    {
    }

    private Schedule(DateTime startAt, DateTime endAt, ProfessionalId? professionalId, Weekday weekday,
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

    public static Schedule Create(DateTime startAt, DateTime endAt, ProfessionalId? professionalId, Weekday weekday,
        Weekend weekend) => new(startAt, endAt, professionalId, weekday, weekend);

    public void AddDayRest(DayRest dayRest) => _daysRest.Add(dayRest);

    public void RemoveDayRest(DayRest dayRest) => _daysRest.Remove(dayRest);

    public void AddOfficeHour(OfficeHour officeHour)
    {
        if (IsDayRest(officeHour)) return;
        //var checkOfficeHour = CheckOfficeHour(officeHour);
        
        
        _officeHours.Add(officeHour);
    }

    public List<OfficeHour?> CheckOfficeHour(OfficeHour officeHour)
    {
        TimeSpan smallestBiggerDifference = TimeSpan.MaxValue;
        TimeSpan smallestDifference = TimeSpan.MaxValue;
            
        OfficeHour? biggerNearestOfficeHour = null;
        OfficeHour? smallestNearestOfficeHour = null;
        foreach (var of in _officeHours)
        {
            var difference = (of.DateAndHour - officeHour.DateAndHour).Duration();

            // Verificar se é a próxima maior hora
           
            
            if (of.DateAndHour > officeHour.DateAndHour && difference < smallestBiggerDifference)
            {
                smallestBiggerDifference = difference;
                biggerNearestOfficeHour = of;
            }

            // Atualizar a menor hora próxima
            
            if (difference < smallestDifference)
            {
                smallestDifference = difference;
                smallestNearestOfficeHour = of;
            }
        }
        
        List<OfficeHour?> officeHoursProcessed = [smallestNearestOfficeHour, biggerNearestOfficeHour];
        return officeHoursProcessed;
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