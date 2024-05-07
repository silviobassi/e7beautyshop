using System.Collections.Immutable;

namespace E7BeautyShop.Domain;

public class Appointment : Entity
{
    public DateTime DateAndHour { get; private set; }
    public ServiceCatalog? ServiceCatalog { get; private set; }
    public Customer? Customer { get; private set; }
    public Professional? Professional { get; private set; }
    public bool IsAvailable { get; private set; } = true;
    public SchedulingState State { get; private set; }

    private Appointment(Builder builder)
    {
        DateAndHour = builder.DateAndHour;
        ServiceCatalog = builder.ServiceCatalog;
        Customer = builder.Customer;
        Professional = builder.Professional;
        
    }

    public void ValidateDateAndHour()
    {
        DomainBusinessException.When(
            DateAndHour < DateTime.Now, "Date and hour must be in the future");
    }
    
    public void Schedule()
    {
        ValidadeSchedule();
        IsAvailable = !IsAvailable;
        State = SchedulingState.Scheduled;
    }

    public void Cancel()
    {
        DomainBusinessException.When(
            DateAndHour < DateTime.Now, "Cannot cancel an appointment in the past");
        State = SchedulingState.Cancelled;
    }

    public void ToMakeAvailable()
    {
        DomainBusinessException.When(
            DateAndHour < DateTime.Now, "Cannot make an appointment available in the past");
        DomainBusinessException.When(IsAvailable, "Appointment is already available");
        IsAvailable = !IsAvailable;
    }

    private void ValidadeSchedule()
    {
        DomainBusinessException.When(
            DateAndHour < DateTime.Now, "Date and hour must be in the future");
        DomainBusinessException.When(State == SchedulingState.Scheduled, "Appointment is already scheduled");
        DomainBusinessException.When(ServiceCatalog == null, "Service catalog is required");
        DomainBusinessException.When(Professional == null, "Professional is required");
        DomainBusinessException.When(
            Professional != null && Professional.Id == Guid.Empty, "Professional is required");
        DomainBusinessException.When(Customer == null, "Customer is required");
        DomainBusinessException.When(Customer != null && Customer.Id == Guid.Empty,
            "Customer is required");
    }

    public class Builder(DateTime dateAndHour)
    {
        public DateTime DateAndHour { get; } = dateAndHour;
        public ServiceCatalog? ServiceCatalog { get; private set; }
        public Customer? Customer { get; private set; }
        public Professional? Professional { get; private set; }

        public Builder WithServiceCatalog(ServiceCatalog? serviceCatalog)
        {
            ServiceCatalog = serviceCatalog;
            return this;
        }

        public Builder WithCustomer(Customer customer)
        {
            Customer = customer;
            return this;
        }

        public Builder WithProfessional(Professional professional)
        {
            Professional = professional;
            return this;
        }

        public Appointment Build()
        {
            return new Appointment(this);
        }
    }
}