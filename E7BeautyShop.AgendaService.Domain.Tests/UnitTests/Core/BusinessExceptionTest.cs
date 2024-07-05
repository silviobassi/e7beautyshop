using E7BeautyShop.AgendaService.Core.Validations;

namespace E7BeautyShop.AgendaService.Tests.UnitTests.Core;

public class BusinessExceptionTest
{
    [Fact]
    public void Should_ThrowException_When_ConditionIsTrue()
    {
        var exception = Assert.Throws<BusinessException>(
            () => BusinessException.ThrowIf(true, "Test"));
        Assert.Equal("Test", exception.Message);
    }
}