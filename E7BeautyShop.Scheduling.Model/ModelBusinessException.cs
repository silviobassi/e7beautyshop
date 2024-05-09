namespace E7BeautyShop.Domain;

public abstract class ModelBusinessException
{
    
    public static void When(bool condition, string message)
    {
        if (condition)
            throw new BusinessException(message);
    }
}