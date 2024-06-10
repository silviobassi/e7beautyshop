namespace E7BeautyShop.Appointment.Core;

public sealed class BusinessNullException(string message) : Exception(message)
{
    public static void When(bool condition, string message)
    {
        if (condition) throw new ArgumentNullException(message);
    }
}