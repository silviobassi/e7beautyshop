using E7BeautyShop.Appointment.Core;
using E7BeautyShop.Appointment.Core.Entities;

namespace E7BeautyShop.Appointment.Application.Ports;

public interface IOfficeHourPersistencePort
{
    Task<OfficeHour?> GetBydIdAsync(Guid id);
    Task<OfficeHour?> CreateAsync(OfficeHour officeHour);
    Task<OfficeHour?> UpdateAsync(OfficeHour officeHour);
    Task<OfficeHour?> DeleteAsync(OfficeHour officeHour);
}