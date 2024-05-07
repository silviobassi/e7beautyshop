namespace E7BeautyShop.Domain.Tests;

public class ServiceCatalogTest
{
    [Fact]
    public void Should_CreateServiceCatalog()
    {
        ServiceCatalog serviceCatalog = new(new ServiceDescription("Haircut", 30M));
        Assert.Equal(30M, serviceCatalog.ServiceDescription.Price);
        Assert.Equal("Haircut", serviceCatalog.ServiceDescription.Name);
        Assert.NotNull(serviceCatalog);
    }
    
    [Fact]
    public void Should_UpdateServiceCatalog()
    {
        var catalogId = Guid.NewGuid();
        ServiceCatalog serviceCatalog = new(new ServiceDescription("Haircut", 30M));
        serviceCatalog.Update(catalogId, new ServiceDescription("Haircut", 35M));
        Assert.Equal(35M, serviceCatalog.ServiceDescription.Price);
        Assert.Equal(catalogId, serviceCatalog.Id);
    }
}