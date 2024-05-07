namespace E7BeautyShop.Domain;

public sealed class ServiceCatalog(ServiceDescription serviceDescription) : Entity
{
    public ServiceDescription ServiceDescription { get; private set; } = serviceDescription;


    public void Update(Guid id, ServiceDescription serviceDescription)
    {
        Id = id;
        ServiceDescription = serviceDescription;
    }
    
    
}