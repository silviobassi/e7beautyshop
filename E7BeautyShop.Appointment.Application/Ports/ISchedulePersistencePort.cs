using System.Linq.Expressions;
using E7BeautyShop.Appointment.Core.Entities;

namespace E7BeautyShop.Appointment.Application.Ports;

public interface ISchedulePersistencePort
{
    Task<Core.Entities.Agenda?> GetByIdAsync(Guid id);
    Task<Core.Entities.Agenda?> CreateAsync(Core.Entities.Agenda calendart);
    Task<Core.Entities.Agenda?> UpdateAsync(Core.Entities.Agenda calendart);
    Task<Core.Entities.Agenda?> DeleteAsync(Core.Entities.Agenda calendart);
}