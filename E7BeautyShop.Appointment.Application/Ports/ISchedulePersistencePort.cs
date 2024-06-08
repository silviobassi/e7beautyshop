using E7BeautyShop.Appointment.Core;

namespace E7BeautyShop.Appointment.Application.Ports;

public interface ISchedulePersistencePort
{
    Task<IEnumerable<Schedule>> GetSchedulesAsync();
}