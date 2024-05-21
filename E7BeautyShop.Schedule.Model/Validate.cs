namespace E7BeautyShop.Domain;

public abstract class Validate
{
    public static void DateAndHour(DateTime dateAndHour)
    {
        ModelBusinessException.When(
            dateAndHour < DateTime.Now, "Date and hour must be in the future");
    }

    public static void CustomerId(CustomerId? customer)
    {
        ModelBusinessException.When(customer == null, "Customer is required");
        ModelBusinessException.When(customer != null && customer.Id == Guid.Empty, "Customer is required");
    }

    public static void ProfessionalId(ProfessionalId? professional)
    {
        ModelBusinessException.When(professional == null, "Professional is required");
        ModelBusinessException.When(professional?.Id == Guid.Empty, "Professional is required");
    }

    public static void ServiceCatalog(ServiceCatalog? serviceCatalog)
    {
        ModelBusinessException.When(serviceCatalog == null, "Service catalog is required");
    }

    public static void IsAvailable(bool isAvailable)
    {
        ModelBusinessException.When(isAvailable, "Appointment is already available");
    }
}