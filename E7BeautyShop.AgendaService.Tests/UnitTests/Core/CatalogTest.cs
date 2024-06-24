using E7BeautyShop.AgendaService.Core.Entities;
using E7BeautyShop.AgendaService.Core.ObjectsValue;

namespace E7BeautyShop.AgendaService.Tests.UnitTests.Core;

public class CatalogTest
{
    [Fact]
    public void Should_CreateServiceCatalog()
    {
        ServiceDescription serviceDescription = ("Haircut", 30M);
        var catalog = Catalog.Create(serviceDescription);
        Assert.Equal(30M, catalog.DescriptionPrice);
        Assert.Equal("Haircut", catalog.DescriptionName);
        Assert.NotNull(catalog);
    }
    
    [Fact]
    public void Should_UpdateServiceCatalog()
    {
        ServiceDescription serviceDescription1 = ("Haircut", 30M);
        ServiceDescription serviceDescription2 = ("Haircut", 35M);
        var catalogId = Guid.NewGuid();
        var catalog = Catalog.Create(serviceDescription1);
        catalog.Update(catalogId, serviceDescription2);
        Assert.Equal(35M, catalog.DescriptionPrice);
        Assert.Equal(catalogId, catalog.Id);
    }
}