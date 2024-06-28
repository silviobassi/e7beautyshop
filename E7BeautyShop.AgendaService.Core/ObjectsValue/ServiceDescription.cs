using E7BeautyShop.AgendaService.Core.Validations;
using static E7BeautyShop.AgendaService.Core.Validations.Messages;

namespace E7BeautyShop.AgendaService.Core.ObjectsValue;

public sealed record ServiceDescription
{
    public string? Name { get; }
    public decimal Price { get; }

    public const int MaxNameLength = 150;

    public ServiceDescription(){}
    private ServiceDescription(string? name, decimal price)
    {
        Name = name;
        Price = price;
        Validate();
    }

    private void Validate()
    {
        ArgumentNullException.ThrowIfNull(Name);
        BusinessException.ThrowIf(Name.Length > MaxNameLength, NameTooLong);
        BusinessException.ThrowIf(Price <= 0, PriceTooLow);
    }
    
    public static implicit operator ServiceDescription((string? name, decimal price) tuple)
        => new(tuple.name, tuple.price);
}