using E7BeautyShop.Appointment.Core;
using E7BeautyShop.Appointment.Core.ObjectsValue;

namespace E7BeautyShop.Appointment.Tests.Core;

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