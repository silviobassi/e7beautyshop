namespace E7BeautyShop.Appointment.Core;

public sealed class BusinessException(string message) : Exception(message)
{
    public static void When(bool condition, string message)
    {
        if (condition) throw new BusinessException(message);
    }
    
}