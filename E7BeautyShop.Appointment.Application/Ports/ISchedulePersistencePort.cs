using System.Linq.Expressions;
using E7BeautyShop.Appointment.Core.Entities;

namespace E7BeautyShop.Appointment.Application.Ports;

public interface ISchedulePersistencePort
{
    Task<Schedule?> GetByIdAsync(Guid id);
    Task<Schedule?> CreateAsync(Schedule schedule);
    Task<Schedule?> UpdateAsync(Schedule schedule);
    Task<Schedule?> DeleteAsync(Schedule schedule);
}