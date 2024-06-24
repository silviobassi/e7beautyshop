using E7BeautyShop.AgendaService.Core.ObjectsValue;

namespace E7BeautyShop.AgendaService.Tests.UnitTests.Core;

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