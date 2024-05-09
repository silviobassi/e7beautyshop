using static System.DayOfWeek;

namespace E7BeautyShop.Domain;

public class Schedule
{
    public OfficeHoursOnWeekday OfficeHoursOnWeekday { get; private set; }
    public OfficeHoursOnHoliday OfficeHoursOnHoliday { get; private set; }
    public OfficeHoursOnWeekend OfficeHoursOnWeekend { get; private set; }

    public int ScheduleDurationInMonths { get; private set; }
    public int Intervalo { get; private set; }
    public IEnumerable<string>? DiasDeDescanso { get; private set; }


    public Schedule(OfficeHoursOnWeekday officeHoursOnWeekday,
        OfficeHoursOnHoliday officeHoursOnHoliday,
        OfficeHoursOnWeekend officeHoursOnWeekend,
        int scheduleDurationInMonths,
        int intervalo,
        List<string> diasDeDescanso)
    {
        OfficeHoursOnWeekday = officeHoursOnWeekday;
        OfficeHoursOnHoliday = officeHoursOnHoliday;
        OfficeHoursOnWeekend = officeHoursOnWeekend;
        ScheduleDurationInMonths = scheduleDurationInMonths;
        Intervalo = intervalo;
        DiasDeDescanso = diasDeDescanso;
    }

    public bool Generate()
    {
        // 
        return false;
    }
}