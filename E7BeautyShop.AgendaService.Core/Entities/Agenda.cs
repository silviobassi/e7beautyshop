using E7BeautyShop.AgendaService.Core.ObjectsValue;
using E7BeautyShop.AgendaService.Core.Validations;
using static E7BeautyShop.AgendaService.Core.Validations.Messages;

namespace E7BeautyShop.AgendaService.Core.Entities;

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
        return IsAnyDaysRestEqualTo(officeHour) && HasDayRestInList;
    }

    private bool IsAnyDaysRestEqualTo(OfficeHour officeHour)
    {
        return _daysRest.Exists(dr => dr.DayOnWeek == officeHour.DateAndHour!.Value.DayOfWeek);
    }
    
    private bool HasDayRestInList => _daysRest.Count > 0;
    
    private void Validate()
    {
        // Value cannot be null. (Parameter 'ProfessionalId')"
        BusinessException.ThrowIf(StartAt == DateTime.MinValue, StartAtTooLow);
        BusinessException.ThrowIf(EndAt == DateTime.MinValue, nameof(EndAt));
        ArgumentException.ThrowIfNullOrEmpty(nameof(ProfessionalId));
        ArgumentNullException.ThrowIfNull(Weekday);
        ArgumentNullException.ThrowIfNull(Weekend);
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