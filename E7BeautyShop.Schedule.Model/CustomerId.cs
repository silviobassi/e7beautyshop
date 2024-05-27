namespace E7BeautyShop.Schedule;

public sealed class CustomerId
{
    public Guid Id { get; private set; }

    public CustomerId(Guid id)
    {
        Id = id;
        Validate();
    }

    private void Validate() => BusinessException.When(Id == Guid.Empty, "Id cannot be empty");
}