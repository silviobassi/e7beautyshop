﻿namespace E7BeautyShop.Appointment.Core;

public sealed class Catalog : Entity
{
    public ServiceDescription? ServiceDescription { get; private set; }

    public Catalog()
    {
    }

    private Catalog(ServiceDescription? serviceDescription)
    {
        Id = Guid.NewGuid();
        ServiceDescription = serviceDescription;
        Validate();
    }

    public static Catalog Create(ServiceDescription? serviceDescription) => new(serviceDescription);


    public string? DescriptionName => ServiceDescription?.Name;

    public decimal? DescriptionPrice => ServiceDescription?.Price;

    public void Update(Guid id, ServiceDescription? serviceDescription)
    {
        Id = id;
        ServiceDescription = serviceDescription!;
        Validate();
    }


    private void Validate()
    {
        BusinessException.When(Id == Guid.Empty, nameof(Id));
        BusinessException.When(ServiceDescription is null, nameof(ServiceDescription));
    }
}