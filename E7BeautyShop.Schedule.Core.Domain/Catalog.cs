namespace E7BeautyShop.Schedule.Core.Domain;

public sealed class Catalog(ServiceDescription serviceDescription) : Entity
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
        BusinessNullException.When(Id == Guid.Empty, nameof(Id));
        BusinessNullException.When(ServiceDescription is null, nameof(ServiceDescription));
    }
}