namespace E7BeautyShop.Domain.Tests;

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
        Assert.NotNull(serviceDescription);
    }

    [Fact]
    public void Should_ThrowException_When_NameIsNull()
    {
        var serviceDescription = new ServiceDescription(null, 40.50M);
        var exception = Assert.Throws<ArgumentException>(() =>  serviceDescription.Validate());
        Assert.Equal("Name cannot be null", exception.Message);
    }

    [Fact]
    public void Should_ThrowException_When_PriceIsZero()
    {
        var serviceDescription = new ServiceDescription("Corte Cabelo + Barba", 0);
        var exception = Assert.Throws<ArgumentException>(() => serviceDescription.Validate());
        Assert.Equal("Price must be greater than 0", exception.Message);
    }
    
    [Fact]
    public void Should_NotThrowException_When_NameNotIsNull()
    {
        var serviceDescription = new ServiceDescription("Corte Barba", 40.50M);
        var exception = Record.Exception(() => serviceDescription.Validate());
        Assert.Null(exception);
    }

    [Fact]
    public void Should_NotThrowException_When_PriceGreaterThanZero()
    {
        var serviceDescription = new ServiceDescription("Corte Cabelo + Barba", 30.0M);
        var exception = Record.Exception(() => serviceDescription.Validate());
        Assert.Null(exception);
    }
}