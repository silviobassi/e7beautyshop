using E7BeautyShop.AgendaService.Adapters.Outbound.Persistence;
using E7BeautyShop.AgendaService.Adapters.Outbound.Publishers;
using E7BeautyShop.AgendaService.Application.Ports;
using E7BeautyShop.AgendaService.Application.Ports.Persistence;
using E7BeautyShop.AgendaService.Application.Ports.Publishers;
using E7BeautyShop.AgendaService.Application.Ports.UseCases;
using E7BeautyShop.AgendaService.Application.UseCases;
using E7BeautyShop.AgendaService.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace E7BeautyShop.AgendaService.Infra.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        
        // Connection DB
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                b => 
                    b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        // Connection RabbitMQ
        var rabbitMqConfig = configuration.GetSection("RabbitMQ");
        services.AddSingleton<IConnection>(sp =>
        {
            var factory = new ConnectionFactory
            {
                HostName = rabbitMqConfig["HostName"],
                Port = Convert.ToInt32(rabbitMqConfig["Port"]),
                UserName = rabbitMqConfig["UserName"],
                Password = rabbitMqConfig["Password"]
            };
            return factory.CreateConnection();
        });

        services.AddSingleton<IBrokerMessageSender, RabbitMqMessageSender>();
        services.AddSingleton<IMessageBusPort, MessageBus>();
        
        services.AddScoped<ICatalogPersistencePort, CatalogPersistence>();
        services.AddScoped<IOfficeHourPersistencePort, OfficeHourPersistence>();
        services.AddScoped<IAgendaPersistencePort, AgendaPersistence>();
        services.AddScoped<IDayRestPersistencePort, DayRestPersistence>();
        
        services.AddScoped<ICreateAgendaPort, CreateAgenda>();
        
        return services;
    }
}