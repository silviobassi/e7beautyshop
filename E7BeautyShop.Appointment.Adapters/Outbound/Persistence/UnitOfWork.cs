using E7BeautyShop.Appointment.Application.Ports;
using E7BeautyShop.Appointment.Infra.Context;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace E7BeautyShop.Appointment.Adapters.Outbound.Persistence;

public class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
{
    private ISchedulePersistencePort? _schedulePersistence;
    private IOfficeHourPersistencePort? _officeHourPersistence;
    private ICatalogPersistencePort? _catalogPersistence;
    private IDayRestPersistencePort? _dayRestPersistence;

    public ISchedulePersistencePort SchedulePersistence =>
        _schedulePersistence ??= new SchedulePersistence(context);

    public IOfficeHourPersistencePort OfficeHourPersistence =>
        _officeHourPersistence ??= new OfficeHourPersistence(context);

    public ICatalogPersistencePort CatalogPersistence => _catalogPersistence ??= new CatalogPersistence(context);

    public IDayRestPersistencePort DayRestPersistence => _dayRestPersistence ??= new DayRestPersistence(context);


    public async Task Commit() => await context.SaveChangesAsync();

    public EntityEntry Entry(object entity) => context.Entry(entity);

    public void Dispose() => context.Dispose();
}