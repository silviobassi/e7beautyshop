using E7BeautyShop.AgendaService.Core.Entities;

namespace E7BeautyShop.AgendaService.Application.Ports.Persistence;

public interface IOfficeHourPersistencePort
{
    Task<OfficeHour?> GetBydIdAsync(Guid id);
    Task<OfficeHour?> CreateAsync(OfficeHour officeHour);
    Task<OfficeHour?> UpdateAsync(OfficeHour officeHour);
    Task<OfficeHour?> DeleteAsync(OfficeHour officeHour);
}