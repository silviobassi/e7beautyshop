using E7BeautyShop.AgendaService.Domain.ObjectsValue;

namespace E7BeautyShop.AgendaService.Domain.Tests.UnitTests.Core;

public class CustomerIdTest
{
    [Fact]
    public void Should_CreateCustomerId()
    {
        var id = Guid.NewGuid();
        CustomerId customerId = id;
        Assert.Equal(id, customerId.Value);
        Assert.NotNull(customerId);
    }
}