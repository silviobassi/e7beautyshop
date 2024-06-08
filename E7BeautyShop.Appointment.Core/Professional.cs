namespace E7BeautyShop.Appointment.Core;

public sealed class Professional : Entity
{
    public Professional(Guid id)
    {
        Id = id;
        Validate();
    }

    private void Validate() => BusinessNullException.When(Id == Guid.Empty, nameof(Id));
}