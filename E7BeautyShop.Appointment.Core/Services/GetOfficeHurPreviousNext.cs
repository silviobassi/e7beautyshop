using E7BeautyShop.Appointment.Core.Entities;

namespace E7BeautyShop.Appointment.Core.Services
{
    public class GetOfficeHurPreviousNext
    {
        private readonly OfficeHour _officeHour;
        private readonly List<OfficeHour?> _officeHoursOrdered;
        public  OfficeHour? OfficePrevious { get; private set; }
        public OfficeHour? OfficeHourNext { get; private set; }

        public GetOfficeHurPreviousNext(OfficeHour officeHour, IReadOnlyCollection<OfficeHour> officeHours)
        {
            _officeHour = officeHour;
            _officeHoursOrdered = officeHours.OrderBy(of => of.DateAndHour).ToList()!;
            OfficePrevious = GetPreviousOfficeHour();
            OfficeHourNext = GetNextOfficeHour();
        }
        
        private OfficeHour GetPreviousOfficeHour()
            => _officeHoursOrdered.LastOrDefault(ooh => ooh?.GetEndTime() < _officeHour.GetEndTime())!;

        private OfficeHour GetNextOfficeHour()
            => _officeHoursOrdered.FirstOrDefault(ooh => ooh?.GetEndTime() > _officeHour.GetEndTime())!;
    }
}