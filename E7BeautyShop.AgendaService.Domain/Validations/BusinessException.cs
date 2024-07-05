namespace E7BeautyShop.AgendaService.Domain.Validations;

public sealed class BusinessException(string message) : Exception(message)
{
    public static void ThrowIf(bool condition, string message)
    {
        if (condition) throw new BusinessException(message);
    }
    
}