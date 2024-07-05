using E7BeautyShop.AgendaService.Adapters.Outbound.Persistence;
using E7BeautyShop.AgendaService.Adapters.Outbound.Publishers;
using E7BeautyShop.AgendaService.Application.Interfaces;
using E7BeautyShop.AgendaService.Application.Ports.Persistence;
using E7BeautyShop.AgendaService.Application.Ports.Publishers;
using E7BeautyShop.AgendaService.Application.UseCases;
using E7BeautyShop.AgendaService.Infra.Context;
using E7BeautyShop.AgendaService.Infra.Data.Connection;
using E7BeautyShop.AgendaService.Infra.Publishers;
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

        services.AddSingleton<IConnectionDb>(new SqlServer(configuration));
        
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
        
        services.AddScoped<ICatalogRepository, CatalogRepository>();
        services.AddScoped<IOfficeHourRepository, OfficeHourRepository>();
        services.AddScoped<IAgendaRepository, AgendaRepository>();
        services.AddScoped<IDayRestRepository, DayRestRepository>();
        
        services.AddScoped<ICreateAgendaUseCase, CreateAgendaUseCaseUseCase>();

        services.AddScoped<IPersistenceQuery, PersistenceQuery>();
        
        return services;
    }
}