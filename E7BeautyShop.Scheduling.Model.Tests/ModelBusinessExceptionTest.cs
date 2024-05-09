namespace E7BeautyShop.Domain.Tests;

public class ModelBusinessExceptionTest
{
    [Fact]
    public void Should_ThrowException_When_ConditionIsTrue()
    {
        var exception = Assert.Throws<BusinessException>(
            () => ModelBusinessException.When(true, "Test"));
        Assert.Equal("Test", exception.Message);
    }
}