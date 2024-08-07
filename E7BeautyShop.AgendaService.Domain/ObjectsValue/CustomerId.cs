﻿namespace E7BeautyShop.AgendaService.Domain.ObjectsValue;

public sealed record CustomerId
{
    public Guid? Value { get; }

    public CustomerId()
    {
    }

    private CustomerId(Guid value)
    { 
        Value = value;
        ArgumentException.ThrowIfNullOrEmpty(nameof(Value));
    }

    public static implicit operator CustomerId(Guid value) => new(value);
}