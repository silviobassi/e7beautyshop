namespace E7BeautyShop.Appointment.Core;

public class ServiceDescription
{
    public string? Name { get; internal set; }
    public decimal? Price { get; internal set; }

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