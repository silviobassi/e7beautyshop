namespace E7BeautyShop.Domain.Tests;

public class AppointmentTest
{
    private readonly ServiceCatalog _serviceCatalog = new(new ServiceDescription("Corte de cabelo", 30.0M));
    private readonly Professional _professional = new(Guid.NewGuid());
    private readonly Customer _customer = new(Guid.NewGuid());

    [Fact]
    public void Should_CreateAppointment()
    {
        var dateAndHour = DateTime.Now.AddMinutes(1);
        var appointment = new Appointment.Builder(dateAndHour).Build();
        Assert.Equal(dateAndHour, appointment.DateAndHour);
        Assert.True(appointment.IsAvailable);
        Assert.NotNull(appointment);
    }
    
    [Fact]
    public void Should_CreateAppointment_WithServiceCatalog()
    {
        var dateAndHour = DateTime.Now.AddMinutes(1);
        var appointment = new Appointment.Builder(dateAndHour)
            .WithServiceCatalog(_serviceCatalog)
            .Build();
        Assert.Equal(_serviceCatalog, appointment.ServiceCatalog);
    }
    
    [Fact]
    public void Should_CreateAppointment_WithServiceCatalogAndCustomer()
    {
        var dateAndHour = DateTime.Now.AddMinutes(1);
        var appointment = new Appointment.Builder(dateAndHour)
            .WithServiceCatalog(_serviceCatalog)
            .WithCustomer(_customer)
            .Build();
        Assert.Equal(_serviceCatalog, appointment.ServiceCatalog);
        Assert.Equal(_customer, appointment.Customer);
    }
    
    [Fact]
    public void Should_CreateAppointment_WithServiceCatalogAndProfessional()
    {
        var dateAndHour = DateTime.Now.AddMinutes(1);
        var appointment = new Appointment.Builder(dateAndHour)
            .WithServiceCatalog(_serviceCatalog)
            .WithProfessional(_professional)
            .Build();
        Assert.Equal(_serviceCatalog, appointment.ServiceCatalog);
        Assert.Equal(_professional, appointment.Professional);
    }
    
    [Fact]
    public void Should_CreateAppointment_WithCustomer()
    {
        var dateAndHour = DateTime.Now.AddMinutes(1);
        var appointment = new Appointment.Builder(dateAndHour)
            .WithCustomer(_customer)
            .Build();
        Assert.Equal(_customer, appointment.Customer);
    }
    
    [Fact]
    public void Should_CreateAppointment_WithProfessional()
    {
        var dateAndHour = DateTime.Now.AddMinutes(1);
        var appointment = new Appointment.Builder(dateAndHour)
            .WithProfessional(_professional)
            .Build();
        Assert.Equal(_professional, appointment.Professional);
    }
    
    [Fact]
    public void Should_CreateAppointment_WithServiceCatalogAndCustomerAndProfessional()
    {
        var dateAndHour = DateTime.Now.AddMinutes(1);
        var appointment = new Appointment.Builder(dateAndHour)
            .WithServiceCatalog(_serviceCatalog)
            .WithCustomer(_customer)
            .WithProfessional(_professional)
            .Build();
        Assert.Equal(_serviceCatalog, appointment.ServiceCatalog);
        Assert.Equal(_customer, appointment.Customer);
        Assert.Equal(_professional, appointment.Professional);
    }
    
   
    [Fact]
    public void Should_NotCreateAppointment_WithPastDate()
    {
        var dateAndHour = DateTime.Now.AddDays(-1);
        var appointment = new Appointment.Builder(dateAndHour).Build();
        var exception = Assert.Throws<ArgumentException>(() => appointment.ValidateDateAndHour());
        Assert.Equal("Date and hour must be in the future", exception.Message);
    }

    [Fact]
    public void Should_NotThrowsExceptions_WithFutureDate()
    {
        var dateAndHour = DateTime.Now.AddDays(1);
        var appointment = new Appointment.Builder(dateAndHour).Build();
        var exception = Record.Exception(() => appointment.ValidateDateAndHour());
        Assert.Null(exception);
    }

    [Fact]
    public void Should_ScheduleAppointment()
    {
        var dateAndHour = DateTime.Now.AddDays(1);
        var appointment = new Appointment.Builder(dateAndHour)
            .WithServiceCatalog(_serviceCatalog)
            .WithCustomer(_customer)
            .WithProfessional(_professional)
            .Build();
        appointment.Schedule();
        Assert.Equal(SchedulingState.Scheduled, appointment.State);
        Assert.False(appointment.IsAvailable);
    }
    
    [Fact]
    public void Schedule_ThrowsException_When_ServiceCatalogIsNull()
    {
        var appointment = new Appointment.Builder(DateTime.Now.AddDays(1)).Build();
        var exception = Assert.Throws<ArgumentException>(() => appointment.Schedule());
        Assert.Equal("Service catalog is required", exception.Message);
    }
    
    [Fact]
    public void Schedule_ThrowsException_When_ProfessionalIsNull()
    {
        var appointment = new Appointment.Builder(DateTime.Now.AddDays(1))
            .WithServiceCatalog(new ServiceCatalog(new ServiceDescription("Corte de cabelo", 30.0M)))
            .Build();
        var exception = Assert.Throws<ArgumentException>(() => appointment.Schedule());
        Assert.Equal("Professional is required", exception.Message);
    }
    
    [Fact]
    public void Schedule_ThrowsException_When_ProfessionalIdIsEmpty()
    {
        var appointment = new Appointment.Builder(DateTime.Now.AddDays(1))
            .WithServiceCatalog(new ServiceCatalog(new ServiceDescription("Corte de cabelo", 30.0M)))
            .WithProfessional(new Professional(Guid.Empty))
            .Build();
        var exception = Assert.Throws<ArgumentException>(() => appointment.Schedule());
        Assert.Equal("Professional is required", exception.Message);
    }
    
    [Fact]
    public void Schedule_ThrowsException_When_CustomerIsNull()
    {
        var appointment = new Appointment.Builder(DateTime.Now.AddDays(1))
            .WithServiceCatalog(new ServiceCatalog(new ServiceDescription("Corte de cabelo", 30.0M)))
            .WithProfessional(new Professional(Guid.NewGuid()))
            .Build();
        var exception = Assert.Throws<ArgumentException>(() => appointment.Schedule());
        Assert.Equal("Customer is required", exception.Message);
    }
    
    [Fact]
    public void Schedule_ThrowsException_When_CustomerIdIsEmpty()
    {
        var appointment = new Appointment.Builder(DateTime.Now.AddDays(1))
            .WithServiceCatalog(new ServiceCatalog(new ServiceDescription("Corte de cabelo", 30.0M)))
            .WithProfessional(new Professional(Guid.NewGuid()))
            .WithCustomer(new Customer(Guid.Empty))
            .Build();
        var exception = Assert.Throws<ArgumentException>(() => appointment.Schedule());
        Assert.Equal("Customer is required", exception.Message);
    }
    
    [Fact]
    public void Schedule_Succeeds_When_AllFieldsAreValid()
    {
        var appointment = new Appointment.Builder(DateTime.Now.AddDays(1))
            .WithServiceCatalog(new ServiceCatalog(new ServiceDescription("Corte de cabelo", 30.0M)))
            .WithProfessional(new Professional(Guid.NewGuid()))
            .WithCustomer(new Customer(Guid.NewGuid()))
            .Build();
        var exception = Record.Exception(() => appointment.Schedule());
        Assert.Null(exception);
    }
    
    [Fact]
    public void Should_ThrowsExceptions_DataAndHourInPast()
    {
        var dateAndHour = DateTime.Now.AddDays(-1);
        var appointment = new Appointment.Builder(dateAndHour).Build();
        var exception = Assert.Throws<ArgumentException>(() => appointment.Schedule());
        Assert.Equal("Date and hour must be in the future", exception.Message);
    }

    [Fact]
    public void Should_CancelAppointment()
    {
        var appointment = new Appointment.Builder(DateTime.Now.AddDays(1)).Build();
        appointment.Cancel();
        Assert.Equal(SchedulingState.Cancelled, appointment.State);
    }

    [Fact]
    public void Should_NotCancel_AppointmentNotScheduled()
    {
        var appointment = new Appointment.Builder(DateTime.Now.AddDays(-1)).Build();
        var exception = Assert.Throws<ArgumentException>(() => appointment.Cancel());
        Assert.Equal("Cannot cancel an appointment in the past", exception.Message);
    }

    [Fact]
    public void Should_MakeAppointmentAvailable()
    {
        var appointment = new Appointment.Builder(DateTime.Now.AddDays(1))
            .WithServiceCatalog(_serviceCatalog)
            .WithCustomer(_customer)
            .WithProfessional(_professional)
            .Build();
        appointment.Schedule();
        appointment.ToMakeAvailable();
        Assert.True(appointment.IsAvailable);
    }

    [Fact]
    public void Should_NotMakeAppointmentAvailable_AppointmentAlreadyAvailable()
    {
        var appointment = new Appointment.Builder(DateTime.Now.AddDays(1)).Build();
        var exception = Assert.Throws<ArgumentException>(() => appointment.ToMakeAvailable());
        Assert.Equal("Appointment is already available", exception.Message);
    }

    [Fact]
    public void Should_NotMakeAppointmentAvailable_AppointmentNotScheduled()
    {
        var appointment = new Appointment.Builder(DateTime.Now.AddDays(-1)).Build();
        var exception = Assert.Throws<ArgumentException>(() => appointment.ToMakeAvailable());
        Assert.Equal("Cannot make an appointment available in the past", exception.Message);
    }
}