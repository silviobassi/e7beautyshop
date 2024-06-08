namespace E7BeautyShop.Appointment.Core;

public sealed class Catalog : Entity
{

    private ServiceDescription? _serviceDescription;
    public Catalog()
    {
    }

    public Catalog(ServiceDescription serviceDescription)
    {
        Id = Guid.NewGuid();
        _serviceDescription = serviceDescription;
        Validate();
    }
    
    public string? DescriptionName
    {
        get => _serviceDescription?.Name;
        init => _serviceDescription!.Name = value;
    }

    public decimal? DescriptionPrice
    {
        get => _serviceDescription?.Price;
        init => _serviceDescription!.Price = value;
    }

    public void Update(Guid id, ServiceDescription? serviceDescription)
    {
        Id = id;
        _serviceDescription = serviceDescription!;
        Validate();
    }
    
    private void Validate()
    {
        BusinessNullException.When(Id == Guid.Empty, nameof(Id));
        BusinessNullException.When(_serviceDescription is null, nameof(ServiceDescription));
    }
}