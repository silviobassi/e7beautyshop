namespace E7BeautyShop.Schedule.Tests;

public class ServiceCatalogTest
{
    [Fact]
    public void Should_CreateServiceCatalog()
    {
        ServiceCatalog serviceCatalog = new(new ServiceDescription("Haircut", 30M));
        Assert.Equal(30M, serviceCatalog.DescriptionPrice);
        Assert.Equal("Haircut", serviceCatalog.DescriptionName);
        Assert.NotNull(serviceCatalog);
    }
    
    [Fact]
    public void Should_UpdateServiceCatalog()
    {
        var catalogId = Guid.NewGuid();
        ServiceCatalog serviceCatalog = new(new ServiceDescription("Haircut", 30M));
        serviceCatalog.Update(catalogId, new ServiceDescription("Haircut", 35M));
        Assert.Equal(35M, serviceCatalog.DescriptionPrice);
        Assert.Equal(catalogId, serviceCatalog.Id);
    }
}