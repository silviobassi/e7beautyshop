namespace E7BeautyShop.Schedule;

public class ServiceDescription
{
    public string? Name { get; private set; }
    public decimal Price { get; private set; } = 0;

    public ServiceDescription(string? name, decimal price)
    {
        Name = name;
        Price = price;
        Validate();
    }

    private void Validate()
    {
        BusinessException.When(string.IsNullOrEmpty(Name), "Name cannot be null");
        BusinessException.When(Price <= 0, "Price must be greater than 0");
    }
}