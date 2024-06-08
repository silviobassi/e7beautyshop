using E7BeautyShop.Appointment.Core;

namespace E7BeautyShop.Appointment.Tests.Core;

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