namespace E7BeautyShop.Domain;

public class Customer
{
    public Guid Id { get; private set; }
    
    public Customer(Guid id)
    {
        Id = id;
    }
}