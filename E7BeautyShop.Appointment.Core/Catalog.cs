namespace E7BeautyShop.Appointment.Core;

public sealed class Catalog : Entity
{
    public Catalog()
    {
    }

    public Catalog(ServiceDescription serviceDescription)
    {
        Id = Guid.NewGuid();
        ServiceDescription = serviceDescription;
        Validate();
    }
    
    public string? DescriptionName => ServiceDescription?.Name;
    
    public decimal? DescriptionPrice => ServiceDescription?.Price;

    public ServiceDescription? ServiceDescription { get; private set; }
    public void Update(Guid id, ServiceDescription? serviceDescription)
    {
        Id = id;
        ServiceDescription = serviceDescription!;
        Validate();
    }
    
    
    private void Validate()
    {
        BusinessNullException.When(Id == Guid.Empty, nameof(Id));
        BusinessNullException.When(ServiceDescription is null, nameof(Core.ServiceDescription));
    }
}