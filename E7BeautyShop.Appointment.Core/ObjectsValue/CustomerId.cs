﻿namespace E7BeautyShop.Appointment.Core.ObjectsValue;

public sealed record CustomerId
{
    public Guid? Value { get; }

    public CustomerId()
    {
    }

    private CustomerId(Guid value) => Value = value;

    public static implicit operator CustomerId(Guid value) => new(value);
}