namespace E7BeautyShop.Schedule;

public sealed class ServiceCatalog(ServiceDescription serviceDescription) : Entity
{
    public ServiceDescription? ServiceDescription { get; private set; } = serviceDescription;


    public void Update(Guid id, ServiceDescription? serviceDescription)
    {
        Id = id;
        ServiceDescription = serviceDescription!;
        Validate();
    }

    private void Validate()
    {
        BusinessException.When(Id == Guid.Empty, "Id cannot be empty");
        BusinessException.When(ServiceDescription == null, "Service Description cannot be null");
    }
}