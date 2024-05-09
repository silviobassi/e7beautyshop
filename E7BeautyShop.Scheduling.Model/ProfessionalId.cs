namespace E7BeautyShop.Domain;

public class ProfessionalId
{
    public Guid Id { get; private set; }

    public ProfessionalId(Guid id)
    {
        Validate(id);
        Id = id;
    }

    private static void Validate(Guid id)
    {
        ModelBusinessException.When(id == Guid.Empty, "Id cannot be empty");
    }
}