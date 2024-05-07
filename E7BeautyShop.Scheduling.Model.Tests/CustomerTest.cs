namespace E7BeautyShop.Domain.Tests;

public class CustomerTest
{
    [Fact]
    public void Should_CreateCustomerId()
    {
        var id = Guid.NewGuid();
        var customerId = new Customer(id);
        Assert.Equal(id, customerId.Id);
        Assert.NotNull(customerId);
    }
}