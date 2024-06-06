using E7BeautyShop.Appointment.Core;

namespace E7BeautyShop.Appointment.Tests.Core;

public class BusinessNullExceptionTest
{
    [Fact]
    public void Should_ThrowException_When_ConditionIsTrue()
    {
        var exception = Assert.Throws<ArgumentNullException>(
            () => BusinessNullException.When(true, "Name"));
        Assert.Equal("Value cannot be null. (Parameter 'Name')", exception.Message);
    }

    [Fact]
    public void Should_NotThrowException_When_ConditionIsFalse()
    {
        var exception = Record.Exception(() => BusinessNullException.When(false, "Name"));
        Assert.Null(exception);
    }

    [Fact]
    public async Task Should_ThrowException_When_BusinessNullException()
    {
        var exception = await Assert.ThrowsAsync<BusinessNullException>(
            () => throw new BusinessNullException("Test"));
        Assert.Equal("Test", exception.Message);
    }
}