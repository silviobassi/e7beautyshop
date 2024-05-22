namespace E7BeautyShop.Schedule;

public class ServiceDescription
{
    public string? Name { get; private set; }
    public decimal Price { get; private set; } = 0;

    public ServiceDescription(string? name, decimal price)
    {
        Validate(name, price);
        Name = name;
        Price = price;
    }

    private static void Validate(string? name, decimal price)
    {
        BusinessException.When(string.IsNullOrEmpty(name), "Name cannot be null");
        BusinessException.When(price <= 0, "Price must be greater than 0");
    }
}