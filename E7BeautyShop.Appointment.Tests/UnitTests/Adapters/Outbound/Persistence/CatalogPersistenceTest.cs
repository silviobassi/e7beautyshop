using E7BeautyShop.Appointment.Application.Ports;
using E7BeautyShop.Appointment.Core.Entities;
using E7BeautyShop.Appointment.Core.ObjectsValue;
using Microsoft.Extensions.DependencyInjection;

namespace E7BeautyShop.Appointment.Tests.UnitTests.Adapters.Outbound.Persistence;

public class CatalogPersistenceTest
{
    private readonly ICatalogPersistencePort _catalogPersistence;
    private readonly IUnitOfWork _unitOfWork;
    
    public CatalogPersistenceTest()
    {
        var startup = new TestStartup();
        _catalogPersistence = startup.ServiceProvider.GetRequiredService<ICatalogPersistencePort>();
        _unitOfWork = startup.ServiceProvider.GetRequiredService<IUnitOfWork>();
    }
    
    [Fact]
    public void Should_Create_Catalog()
    {
        ServiceDescription description = ("Corte de cabelo + Barba", 30M);
        
        var catalog = Catalog.Create(description);
        var entity = _catalogPersistence.CreateAsync(catalog);
        Assert.NotNull(entity);
        Assert.Equal(catalog.Id, entity.Id);
        Assert.Equal(catalog.ServiceDescription, entity.ServiceDescription);
        Assert.Equal(catalog.DescriptionName, entity.DescriptionName);
        Assert.Equal(catalog.DescriptionPrice, entity.DescriptionPrice);
    }
    
    [Fact]
    public void Should_Update_Catalog()
    {
        ServiceDescription description = ("Corte de cabelo + Barba", 30M);
        
        var catalog = Catalog.Create(description);
        var entity = _catalogPersistence.UpdateAsync(catalog);
        Assert.NotNull(entity);
        Assert.Equal(catalog.Id, entity.Id);
        Assert.Equal(catalog.ServiceDescription, entity.ServiceDescription);
        Assert.Equal(catalog.DescriptionName, entity.DescriptionName);
        Assert.Equal(catalog.DescriptionPrice, entity.DescriptionPrice);
    }
    
    [Fact]
    public void Should_Delete_Catalog()
    {
        ServiceDescription description = ("Corte de cabelo + Barba", 30M);
        
        var catalog = Catalog.Create(description);
        var entity = _catalogPersistence.DeleteAsync(catalog);
        Assert.NotNull(entity);
        Assert.Equal(catalog.Id, entity.Id);
        Assert.Equal(catalog.ServiceDescription, entity.ServiceDescription);
        Assert.Equal(catalog.DescriptionName, entity.DescriptionName);
        Assert.Equal(catalog.DescriptionPrice, entity.DescriptionPrice);
    }
    
    [Fact]
    public async Task Should_NotGet_Catalog()
    {
        ServiceDescription description = ("Corte de cabelo + Barba", 30M);
        var catalog = Catalog.Create(description);
        var entity = _catalogPersistence.CreateAsync(catalog);
        entity = await _catalogPersistence.GetByIdAsync( x => x.Id == entity.Id);
        Assert.Null(entity);
    }
}