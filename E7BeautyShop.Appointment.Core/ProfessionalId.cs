namespace E7BeautyShop.Appointment.Core;

public sealed class ProfessionalId : Entity
{
    public ProfessionalId(Guid id)
    {
        Id = id;
        Validate();
    }

    private void Validate() => BusinessNullException.When(Id == Guid.Empty, nameof(Id));
}