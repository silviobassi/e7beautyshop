﻿namespace E7BeautyShop.Appointment.Core;

public sealed class CustomerId : Entity
{
    public new Guid? Id { get; internal set; }
    public CustomerId(Guid? id)
    {
        Id = id;
    }
}