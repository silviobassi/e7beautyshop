namespace E7BeautyShop.AgendaService.Domain.ObjectsValue;

public sealed record ProfessionalId
{
    public Guid Value { get; }

    public ProfessionalId()
    {
    }

    private ProfessionalId(Guid value)
    {
        Value = value;
        Validate();
    }

    private void Validate() => ArgumentException.ThrowIfNullOrEmpty(nameof(Value));

    public static implicit operator ProfessionalId(Guid value) => new(value);
}