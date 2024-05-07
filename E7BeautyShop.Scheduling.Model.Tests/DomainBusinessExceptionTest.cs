namespace E7BeautyShop.Domain.Tests;

public class DomainBusinessExceptionTest
{
    [Fact]
    public void Should_ThrowException_When_ConditionIsTrue()
    {
        var exception = Assert.Throws<ArgumentException>(
            () => DomainBusinessException.When(true, "Test"));
        Assert.Equal("Test", exception.Message);
    }
}