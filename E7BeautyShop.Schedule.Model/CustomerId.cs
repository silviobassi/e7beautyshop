namespace E7BeautyShop.Schedule;

public sealed class CustomerId: Entity
{
    public CustomerId(Guid id)
    {
        Id = id;
        Validate();
    }

    private void Validate() => BusinessException.When(Id == Guid.Empty, "Id cannot be empty");
}