using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace E7BeautyShop.Appointment.Application.Ports;

public interface IUnitOfWork
{
    ISchedulePersistencePort SchedulePersistence { get; }
    IOfficeHourPersistencePort OfficeHourPersistence { get; }
    ICatalogPersistencePort CatalogPersistence { get; }
    
    IDayRestPersistencePort DayRestPersistence { get; }
    Task Commit();

    void Dispose();
    EntityEntry Entry(object entity);
}