using E7BeautyShop.AgendaService.Infra.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace E7BeautyShop.AgendaService.Tests;

public class TestStartup
{
    private readonly IServiceScope _serviceScope;
    public IServiceProvider ServiceProvider => _serviceScope.ServiceProvider;

    public TestStartup()
    {
        var builder = WebApplication.CreateBuilder();
        builder.Services.AddInfrastructure(builder.Configuration);
        var app = builder.Build();

        _serviceScope = app.Services.CreateScope();
    }
}