using E7BeautyShop.AgendaService.Application.Interfaces;
using E7BeautyShop.AgendaService.Domain.Interfaces;
using E7BeautyShop.AgendaService.Infra.Data.Connection;
using E7BeautyShop.AgendaService.Infra.Data.Context;
using E7BeautyShop.AgendaService.Infra.Publishers;
using E7BeautyShop.AgendaService.Infra.Repositories;
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

        var myHandlers = AppDomain.CurrentDomain.Load("E7BeautyShop.AgendaService.Application");
        services.AddMediatR(s => s.RegisterServicesFromAssembly(myHandlers));


        services.AddSingleton<IBrokerMessageSender, RabbitMqMessageSender>();
        services.AddSingleton<IMessageBusPort, MessageBus>();

        services.AddScoped<ICatalogRepository, CatalogRepository>();
        services.AddScoped<IOfficeHourRepository, OfficeHourRepository>();
        services.AddScoped<IAgendaRepository, AgendaRepository>();
        services.AddScoped<IDayRestRepository, DayRestRepository>();

        return services;
    }
}