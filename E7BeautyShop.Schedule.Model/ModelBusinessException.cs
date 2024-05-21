namespace E7BeautyShop.Schedule;

public static class ModelBusinessException
{
    public static void When(bool condition, string message)
    {
        if (condition)
            throw new BusinessException(message);
    }
}