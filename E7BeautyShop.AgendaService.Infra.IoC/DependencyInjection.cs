using E7BeautyShop.AgendaService.Adapters.Outbound.Persistence;
using E7BeautyShop.AgendaService.Application.Ports;
using E7BeautyShop.AgendaService.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace E7BeautyShop.AgendaService.Infra.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                b => 
                    b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        services.AddScoped<ICatalogPersistencePort, CatalogPersistence>();
        services.AddScoped<IOfficeHourPersistencePort, OfficeHourPersistence>();
        services.AddScoped<IAgendaPersistencePort, AgendaPersistence>();
        services.AddScoped<IDayRestPersistencePort, DayRestPersistence>();
        
        return services;
    }
}