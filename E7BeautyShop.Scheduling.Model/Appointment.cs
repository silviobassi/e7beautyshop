namespace E7BeautyShop.Domain;

public class Appointment : Entity
{
    public DateTime DateAndHour { get; private set; }
    public ServiceCatalog? ServiceCatalog { get; private set; }
    public CustomerId? Customer { get; private set; }
    public ProfessionalId? Professional { get; private set; }
    public bool IsAvailable { get; private set; } = true;
    public SchedulingState State { get; private set; }

    private Appointment(Builder builder)
    {
        Validate.DateAndHour(builder.DateAndHour);
        DateAndHour = builder.DateAndHour;
        ServiceCatalog = builder.ServiceCatalog;
        Customer = builder.Customer;
        Professional = builder.Professional;
    }

    public void Schedule()
    {
        Validate.DateAndHour(DateAndHour);
        Validate.ServiceCatalog(ServiceCatalog);
        Validate.ProfessionalId(Professional);
        Validate.CustomerId(Customer);
        State = SchedulingState.Scheduled;
        IsAvailable = !IsAvailable;
    }

    public void Cancel()
    {
        Validate.DateAndHour(DateAndHour);
        State = SchedulingState.Cancelled;
    }

    public void ToMakeAvailable()
    {
        Validate.DateAndHour(DateAndHour);
        Validate.IsAvailable(IsAvailable);
        IsAvailable = !IsAvailable;
    }

    public class Builder(DateTime dateAndHour)
    {
        public DateTime DateAndHour { get; } = dateAndHour;
        public ServiceCatalog? ServiceCatalog { get; private set; }
        public CustomerId? Customer { get; private set; }
        public ProfessionalId? Professional { get; private set; }

        public Builder WithServiceCatalog(ServiceCatalog? serviceCatalog)
        {
            ServiceCatalog = serviceCatalog;
            return this;
        }

        public Builder WithCustomer(CustomerId customerId)
        {
            Customer = customerId;
            return this;
        }

        public Builder WithProfessional(ProfessionalId professionalId)
        {
            Professional = professionalId;
            return this;
        }

        public Appointment Build()
        {
            return new Appointment(this);
        }
    }
}