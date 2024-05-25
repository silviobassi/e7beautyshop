namespace E7BeautyShop.Schedule;

public sealed class ProfessionalId
{
    public Guid Id { get; private set; }

    public ProfessionalId(Guid id)
    {
        Id = id;
        Validate();
    }

    private void Validate() => BusinessException.When(Id == Guid.Empty, "Id cannot be empty");
}