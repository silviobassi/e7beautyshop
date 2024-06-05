namespace E7BeautyShop.Schedule;

public readonly struct ServiceDescription
{
    public string? Name { get; init; }
    public decimal Price { get; } = 0;

    public ServiceDescription(string? name, decimal price)
    {
        Name = name;
        Price = price;
        Validate();
    }

    private void Validate()
    {
        BusinessNullException.When(string.IsNullOrEmpty(Name), nameof(Name));
        BusinessException.When(Price <= 0, "Price must be greater than 0");
    }
}