namespace E7BeautyShop.Schedule.Tests;

public class ReserveRegisteredEventTest
{
    [Fact]
    public void Validate_ThrowsBusinessException_WhenReserveDateAndHourIsMinValue()
    {
        var reserveRegisteredEvent = new ReserveRegisteredEvent();
        var exception = Assert.Throws<BusinessException>(() =>
            reserveRegisteredEvent.Create(Guid.NewGuid(), DateTime.MinValue, "ServiceName", 100));
        Assert.Equal("Invalid reserve date and hour", exception.Message);
    }

    [Fact]
    public void Validate_ThrowsBusinessException_WhenServiceNameIsEmpty()
    {
        var reserveRegisteredEvent = new ReserveRegisteredEvent();
        var exception = Assert.Throws<BusinessException>(() =>
            reserveRegisteredEvent.Create(Guid.NewGuid(), DateTime.Now, string.Empty, 100));
        Assert.Equal("Invalid service name", exception.Message);
    }

    [Fact]
    public void Validate_ThrowsBusinessException_WhenPriceServiceIsZero()
    {
        var reserveRegisteredEvent = new ReserveRegisteredEvent();
        var exception = Assert.Throws<BusinessException>(() =>
            reserveRegisteredEvent.Create(Guid.NewGuid(), DateTime.Now, "ServiceName", 0));
        Assert.Equal("Invalid price service", exception.Message);
    }

    [Fact]
    public void Validate_DoesNotThrow_WhenAllFieldsAreValid()
    {
        var reserveRegisteredEvent = new ReserveRegisteredEvent();
        reserveRegisteredEvent.Create(Guid.NewGuid(), DateTime.Now, "ServiceName", 100);
        var exception = Record.Exception(() => reserveRegisteredEvent);
        Assert.Null(exception);
    }
}