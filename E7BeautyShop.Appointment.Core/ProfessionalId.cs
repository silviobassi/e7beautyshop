namespace E7BeautyShop.Appointment.Core;

public sealed class ProfessionalId
{
    public Guid Value { get; }

    public ProfessionalId()
    {
    }

    public ProfessionalId(Guid value)
    {
        Value = value;
        Validate();
    }

    private void Validate() => BusinessNullException.When(Value == Guid.Empty, nameof(Value));
}