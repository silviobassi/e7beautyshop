﻿using E7BeautyShop.AgendaService.Domain.Entities;
using E7BeautyShop.AgendaService.Domain.Interfaces;
using E7BeautyShop.AgendaService.Domain.ObjectsValue;
using Microsoft.Extensions.DependencyInjection;

namespace E7BeautyShop.AgendaService.Domain.Tests.IntegrationTests;

public class CatalogRepositoryTest(TestStartup startup): IClassFixture<TestStartup>
{
   
    private readonly ICatalogRepository _catalogPersistence = 
        startup.ServiceProvider.GetRequiredService<ICatalogRepository>();
    
    [Fact]
    public async Task Should_Create_Catalog()
    {
        const string name = "Catalog Test";
        const decimal price = 100M;
        ServiceDescription serviceDescription = (name, price);
        
        var catalog = Catalog.Create(serviceDescription);
        await _catalogPersistence.CreateAsync(catalog);
        var currentCatalog = await _catalogPersistence.GetByIdAsync(catalog.Id);
        Assert.NotNull(currentCatalog);
        Assert.Equal(catalog.Id, currentCatalog.Id);
        Assert.Equal(catalog.DescriptionName, currentCatalog.DescriptionName);
        Assert.Equal(catalog.DescriptionPrice, currentCatalog.DescriptionPrice);

        var deleteAsync = await _catalogPersistence.DeleteAsync(catalog);
        var deletedCatalog = await _catalogPersistence.GetByIdAsync(deleteAsync!.Id);
        Assert.Null(deletedCatalog);
    }
    
    [Fact]
    public async Task Should_Update_Catalog()
    {
        const string name = "Catalog Test";
        const decimal price = 100M;
        ServiceDescription serviceDescription1 = (name, price);
        ServiceDescription serviceDescription2 = (name, 150M);
        
        var catalog = Catalog.Create(serviceDescription1);
        await _catalogPersistence.CreateAsync(catalog);

        var currentCatalog = await _catalogPersistence.GetByIdAsync(catalog.Id);
        Assert.NotNull(currentCatalog);
        Assert.Equal(catalog.Id, currentCatalog.Id);
        Assert.Equal(catalog.DescriptionName, currentCatalog.DescriptionName);
        Assert.Equal(catalog.DescriptionPrice, currentCatalog.DescriptionPrice);
        
        currentCatalog.Update(catalog.Id, serviceDescription2);
        await _catalogPersistence.UpdateAsync(currentCatalog!);
        
        var updatedCatalog = await _catalogPersistence.GetByIdAsync(currentCatalog!.Id);
        Assert.Equal(150M, updatedCatalog?.DescriptionPrice);
        Assert.Equal(catalog.Id, updatedCatalog?.Id);
        Assert.Equal(name, updatedCatalog?.DescriptionName);
        Assert.NotNull(updatedCatalog);
        
        var deleteAsync = await _catalogPersistence.DeleteAsync(updatedCatalog!);
        var deletedCatalog = await _catalogPersistence.GetByIdAsync(deleteAsync!.Id);
        Assert.Null(deletedCatalog);
    }
    
}