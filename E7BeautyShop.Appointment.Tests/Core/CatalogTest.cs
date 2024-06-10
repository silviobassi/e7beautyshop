using E7BeautyShop.Appointment.Core;

namespace E7BeautyShop.Appointment.Tests.Core;

public class CatalogTest
{
    [Fact]
    public void Should_CreateServiceCatalog()
    {
        Catalog catalog = new(new ServiceDescription("Haircut", 30M));
        Assert.Equal(30M, catalog.DescriptionPrice);
        Assert.Equal("Haircut", catalog.DescriptionName);
        Assert.NotNull(catalog);
    }
    
    [Fact]
    public void Should_UpdateServiceCatalog()
    {
        var catalogId = Guid.NewGuid();
        Catalog catalog = new(new ServiceDescription("Haircut", 30M));
        catalog.Update(catalogId, new ServiceDescription("Haircut", 35M));
        Assert.Equal(35M, catalog.DescriptionPrice);
        Assert.Equal(catalogId, catalog.Id);
    }
}