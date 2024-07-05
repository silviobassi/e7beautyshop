using E7BeautyShop.AgendaService.Domain.Entities;

namespace E7BeautyShop.AgendaService.Domain.Interfaces;

public interface IOfficeHourRepository
{
    Task<OfficeHour?> GetBydIdAsync(Guid id);
    Task<OfficeHour?> CreateAsync(OfficeHour officeHour);
    Task<OfficeHour?> UpdateAsync(OfficeHour officeHour);
    Task<OfficeHour?> DeleteAsync(OfficeHour officeHour);
}