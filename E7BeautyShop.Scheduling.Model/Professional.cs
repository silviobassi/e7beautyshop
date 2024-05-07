namespace E7BeautyShop.Domain;

public class Professional
{
    public Guid Id { get; private set; }
    
    public Professional(Guid id)
    {
        Id = id;
    }
}