namespace E7BeautyShop.Domain.Tests;

public class ValidateTest
{
    [Fact]
    public void Should_ThrowExceptions_DateAndHour()
    {
        var dateAndHour = DateTime.Now.AddMinutes(-1);
        Assert.Throws<BusinessException>(() => Validate.DateAndHour(dateAndHour));
    }
    
    [Fact]
    public void Should_NotThrowExceptions_DateAndHour()
    {
        var dateAndHour = DateTime.Now.AddMinutes(1);
        var exception = Record.Exception(() => Validate.DateAndHour(dateAndHour));
        Assert.Null(exception);
    }
}