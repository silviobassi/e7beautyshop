using E7BeautyShop.Appointment.Core;

namespace E7BeautyShop.Appointment.Application.Ports;

public interface ISchedulePersistencePort
{
    Task<Schedule?> GetByIdAsync(Guid? id);
}