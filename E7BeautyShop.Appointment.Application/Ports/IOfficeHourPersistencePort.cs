using E7BeautyShop.Appointment.Core;

namespace E7BeautyShop.Appointment.Application.Ports;

public interface IOfficeHourPersistencePort
{
    Task<OfficeHour?> GetByIdAsync(Guid? id);
}