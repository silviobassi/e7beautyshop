using E7BeautyShop.AgendaService.Core.ObjectsValue;
using E7BeautyShop.AgendaService.Core.Validations;

namespace E7BeautyShop.AgendaService.Tests.UnitTests.Core;

public class ServiceDescriptionTest
{
    [Fact]
    public void Should_CreateServiceDescription()
    {
        const string name = "Corte Cabelo + Barba";
        const decimal price = 40.50M;
        ServiceDescription serviceDescription = (name, price);
        Assert.Equal(name, serviceDescription.Name);
        Assert.Equal(price, serviceDescription.Price);
    }

    [Fact]
    public void Should_ThrowException_When_NameIsNull()
    {
        var exception = Assert.Throws<ArgumentNullException>(() =>
        {
            ServiceDescription serviceDescription = (null, 40.50M);
            return serviceDescription;
        });
        Assert.Equal("Value cannot be null. (Parameter 'Name')", exception.Message);
    }


    [Fact]
    public void Should_ThrowException_When_NameIsGreaterThan150()
    {
        const string strGreaterThan150 = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx" +
                                         "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx" +
                                         "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx" +
                                         "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";

        var exception = Assert.Throws<BusinessException>(() =>
        {
            ServiceDescription serviceDescription = (strGreaterThan150, 40.50M);
            return serviceDescription;
        });
        Assert.Equal($"Name must be less than {ServiceDescription.MaxNameLength} characters", exception.Message);
    }

    [Fact]
    public void Should_ThrowException_When_PriceIsZero()
    {
        var exception = Assert.Throws<BusinessException>(() =>
        {
            ServiceDescription serviceDescription = ("Corte Cabelo + Barba", 0);
            return serviceDescription;
        });
        Assert.Equal(Messages.PriceShouldGreaterThanZero, exception.Message);
    }

    [Fact]
    public void Should_NotThrowException_When_NameNotIsNull()
    {
        ServiceDescription serviceDescription = ("Corte Barba", 40.50M);
        var exception = Record.Exception(() => serviceDescription);
        Assert.Null(exception);
    }

    [Fact]
    public void Should_NotThrowException_When_PriceGreaterThanZero()
    {
        var serviceDescription = ("Corte Cabelo + Barba", 30.0M);
        var exception = Record.Exception(() => serviceDescription);
        Assert.Null(exception);
    }
}