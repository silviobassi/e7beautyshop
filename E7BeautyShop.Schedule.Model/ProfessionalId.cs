namespace E7BeautyShop.Schedule;

public sealed class ProfessionalId : Entity
{
    public ProfessionalId(Guid id)
    {
        Id = id;
        Validate();
    }

    private void Validate() => BusinessNullException.When(Id == Guid.Empty, nameof(Id));
}