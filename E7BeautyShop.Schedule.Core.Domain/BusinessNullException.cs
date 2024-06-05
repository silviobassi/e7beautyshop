namespace E7BeautyShop.Schedule.Core.Domain;

public sealed class BusinessNullException(string message) : Exception(message)
{
    public static void When(bool condition, string message)
    {
        if (condition) throw new ArgumentNullException(message);
    }
}