namespace E7BeautyShop.Domain;

public class ServiceDescription
{
    public string? Name { get; private set; }
    public decimal Price { get; private set; } = 0;

    public ServiceDescription(string? name, decimal price)
    {
        Name = name;
        Price = price;
    }

    public void Validate()
    {
        DomainBusinessException.When(string.IsNullOrEmpty(Name), "Name cannot be null");
        DomainBusinessException.When(Price <= 0, "Price must be greater than 0");
    }
}