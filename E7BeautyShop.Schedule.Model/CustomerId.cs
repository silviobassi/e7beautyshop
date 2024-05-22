namespace E7BeautyShop.Schedule;

public class CustomerId
{
    public Guid Id { get; private set; }
    
    public CustomerId(Guid id)
    {
        Validate(id);
        Id = id;
    }

    private static void Validate(Guid id)
    {
        BusinessException.When(id == Guid.Empty, "Id cannot be empty");
    }
}