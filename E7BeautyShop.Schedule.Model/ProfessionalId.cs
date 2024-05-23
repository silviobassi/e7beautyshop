namespace E7BeautyShop.Schedule;

public sealed class ProfessionalId
{
    public Guid Id { get; private set; }

    public ProfessionalId(Guid id)
    {
        BusinessException.When(id == Guid.Empty, "Id cannot be empty");
        Id = id;
    }
}