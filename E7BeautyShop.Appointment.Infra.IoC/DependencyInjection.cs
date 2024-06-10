using E7BeautyShop.Appointment.Adapters.Outbound.Persistence;
using E7BeautyShop.Appointment.Application.Ports;
using E7BeautyShop.Appointment.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace E7BeautyShop.Appointment.Infra.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                b => 
                    b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        services.AddScoped<ICatalogPersistencePort, CatalogPersistence>();
        services.AddScoped<IOfficeHourPersistencePort, OfficeHourPersistence>();
        services.AddScoped<ISchedulePersistencePort, SchedulePersistence>();
        services.AddScoped<IDayRestPersistencePort, DayRestPersistence>();
        
        services.AddScoped(typeof(IPersistence<>), typeof(Persistence<>));
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        return services;
    }
}