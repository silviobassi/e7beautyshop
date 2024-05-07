namespace E7BeautyShop.Domain;

public abstract class DomainBusinessException : Exception
{
    public static void When(bool condition, string message)
    {
        if (condition)
            throw new ArgumentException(message);
    }
}