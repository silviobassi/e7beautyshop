namespace E7BeautyShop.Schedule;

public sealed class ServiceCatalog(ServiceDescription serviceDescription) : Entity
{
    public ServiceDescription ServiceDescription { get; private set; } = serviceDescription;


    public void Update(Guid id, ServiceDescription? serviceDescription)
    {
        Validate(id, serviceDescription);
        Id = id;
        ServiceDescription = serviceDescription!;
    }

    private static void Validate(Guid id, ServiceDescription? serviceDescription)
    {
        BusinessException.When(id == Guid.Empty, "Id cannot be empty");
        BusinessException.When(serviceDescription == null, "Service Description cannot be null");
    }
}