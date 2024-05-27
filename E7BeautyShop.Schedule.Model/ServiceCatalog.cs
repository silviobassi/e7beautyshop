namespace E7BeautyShop.Schedule;

public sealed class ServiceCatalog(ServiceDescription serviceDescription) : Entity
{
    public void Update(Guid id, ServiceDescription? serviceDescription)
    {
        Id = id;
        ServiceDescription = serviceDescription!;
        Validate();
    }

    private ServiceDescription? ServiceDescription { get; set; } = serviceDescription;

    public string? DescriptionName => ServiceDescription?.Name;

    public decimal? DescriptionPrice => ServiceDescription?.Price;

    private void Validate()
    {
        BusinessException.When(Id == Guid.Empty, "Id cannot be empty");
        BusinessException.When(ServiceDescription == null, "Service Description cannot be null");
    }
}