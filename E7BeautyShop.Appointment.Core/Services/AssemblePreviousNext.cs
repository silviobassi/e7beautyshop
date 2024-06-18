using E7BeautyShop.Appointment.Core.Entities;
using E7BeautyShop.Appointment.Core.Validations;

namespace E7BeautyShop.Appointment.Core.Services
{
    public class AssemblePreviousNext
    {
        private readonly OfficeHour _officeHour;
        private readonly List<OfficeHour> _orderedOfficeHours;
        public  OfficeHour? PreviousOfficeHour { get; private set; }
        public OfficeHour? NextOfficeHour { get; private set; }

        public AssemblePreviousNext(OfficeHour officeHour, IReadOnlyCollection<OfficeHour> officeHours)
        {
            _officeHour = officeHour;
            _orderedOfficeHours = officeHours.OrderBy(of => of.DateAndHour).ToList()!;
            PreviousOfficeHour = GetPreviousOfficeHour();
            NextOfficeHour = GetNextOfficeHour();
        }
        

        private OfficeHour GetPreviousOfficeHour()
            => _orderedOfficeHours.LastOrDefault(ooh => ooh.GetEndTime() < _officeHour.GetEndTime());

        private OfficeHour GetNextOfficeHour()
            => _orderedOfficeHours.FirstOrDefault(ooh => ooh.GetEndTime() > _officeHour.GetEndTime());
    }
}