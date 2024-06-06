using E7BeautyShop.Appointment.Core;

namespace E7BeautyShop.Appointment.Tests.Core;

public class ServiceDescriptionTest
{
    [Fact]
    public void Should_CreateServiceDescription()
    {
        const string name = "Corte Cabelo + Barba";
        const decimal price = 40.50M;
        var serviceDescription = new ServiceDescription(name, price);
        Assert.Equal(name, serviceDescription.Name);
        Assert.Equal(price, serviceDescription.Price);
    }

    [Fact]
    public void Should_ThrowException_When_NameIsNull()
    {
        var exception = Assert.Throws<ArgumentNullException>(
            () =>  new ServiceDescription(null, 40.50M));
        Assert.Equal("Value cannot be null. (Parameter 'Name')", exception.Message);
    }

    [Fact]
    public void Should_ThrowException_When_PriceIsZero()
    {
        var exception = Assert.Throws<BusinessException>(
            () => new ServiceDescription("Corte Cabelo + Barba", 0));
        Assert.Equal("Price must be greater than 0", exception.Message);
    }
    
    [Fact]
    public void Should_NotThrowException_When_NameNotIsNull()
    {
        var serviceDescription = new ServiceDescription("Corte Barba", 40.50M);
        var exception = Record.Exception(() => serviceDescription);
        Assert.Null(exception);
    }

    [Fact]
    public void Should_NotThrowException_When_PriceGreaterThanZero()
    {
        var serviceDescription = new ServiceDescription("Corte Cabelo + Barba", 30.0M);
        var exception = Record.Exception(() => serviceDescription);
        Assert.Null(exception);
    }
}