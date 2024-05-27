namespace E7BeautyShop.Schedule.Tests;

public class CustomerIdTest
{
    [Fact]
    public void Should_CreateCustomerId()
    {
        var id = Guid.NewGuid();
        var customerId = new CustomerId(id);
        Assert.Equal(id, customerId.Id);
        Assert.NotNull(customerId);
    }
}