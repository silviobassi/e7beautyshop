namespace E7BeautyShop.Domain.Tests;

public class AppointmentTest
{
    private readonly ServiceCatalog _serviceCatalog = new(new ServiceDescription("Corte de cabelo", 30.0M));
    private readonly ProfessionalId _professionalId = new(Guid.NewGuid());
    private readonly CustomerId _customerId = new(Guid.NewGuid());

    [Fact]
    public void Should_BuildAppointment_WithValidInputs()
    {
        var dateAndHour = DateTime.Now.AddHours(1);

        var builder = new Appointment.Builder(dateAndHour)
            .WithServiceCatalog(_serviceCatalog)
            .WithCustomer(_customerId)
            .WithProfessional(_professionalId);

        var appointment = builder.Build();

        Assert.Equal(dateAndHour, appointment.DateAndHour);
        Assert.Equal(_serviceCatalog, appointment.ServiceCatalog);
        Assert.Equal(_customerId, appointment.Customer);
        Assert.Equal(_professionalId, appointment.Professional);
        Assert.Equal(_professionalId.Id, appointment.Professional?.Id);
    }

    [Fact]
    public void Should_ThrowException_When_BuildingSchedulingAppointment_Without_ServiceCatalog()
    {
        var dateAndHour = DateTime.Now.AddDays(1);
        var builder = new Appointment.Builder(dateAndHour)
            .WithCustomer(_customerId)
            .WithProfessional(_professionalId);
        Assert.Throws<BusinessException>(() => builder.Build().Schedule());
    }

    [Fact]
    public void Should_ThrowException_When_BuildingSchedulingAppointment_Without_Customer()
    {
        var dateAndHour = DateTime.Now.AddDays(1);
        var builder = new Appointment.Builder(dateAndHour)
            .WithServiceCatalog(_serviceCatalog)
            .WithProfessional(_professionalId);
        Assert.Throws<BusinessException>(() => builder.Build().Schedule());
    }

    [Fact]
    public void Should_ThrowException_When_BuildingSchedulingAppointment_Without_Professional()
    {
        var dateAndHour = DateTime.Now.AddDays(1);
        var builder = new Appointment.Builder(dateAndHour)
            .WithServiceCatalog(_serviceCatalog)
            .WithCustomer(_customerId);
        Assert.Throws<BusinessException>(() => builder.Build().Schedule());
    }

    [Fact]
    public void Should_ThrowException_When_SchedulingAppointment_With_DateAndHourInThePast()
    {
        var dateAndHour = DateTime.Now.AddHours(-1);
        var appointment = new Appointment.Builder(dateAndHour)
            .WithServiceCatalog(_serviceCatalog)
            .WithCustomer(_customerId)
            .WithProfessional(_professionalId);
        Assert.Throws<BusinessException>(() => appointment.Build().Schedule());
    }
    
    [Fact]
    public void Should_ScheduleAppointment_WithValidInputs()
    {
        var dateAndHour = DateTime.Now.AddDays(1);
        var builder = new Appointment.Builder(dateAndHour)
            .WithServiceCatalog(_serviceCatalog)
            .WithCustomer(_customerId)
            .WithProfessional(_professionalId);
        var appointment = builder.Build();
        appointment.Schedule();
        Assert.Equal(SchedulingState.Scheduled, appointment.State);
        Assert.False(appointment.IsAvailable);
    }

    [Fact]
    public void Should_ScheduleAppointment_Just_DateAndHour()
    {
        var dateAndHour = DateTime.Now.AddDays(1);
        var appointment = new Appointment.Builder(dateAndHour).Build();
        Assert.Equal(dateAndHour, appointment.DateAndHour);
    }
    
    [Fact]
    public void Should_ThrowException_When_DateAndHourIsInThePast()
    {
        var dateAndHour = DateTime.Now.AddHours(-1);
        Assert.Throws<BusinessException>(() => new Appointment.Builder(dateAndHour).Build());
    }
    
    [Fact]
    public void Should_CancelAppointment_When_AppointmentIsScheduled()
    {
        var dateAndHour = DateTime.Now.AddDays(1);
        var builder = new Appointment.Builder(dateAndHour)
            .WithServiceCatalog(_serviceCatalog)
            .WithCustomer(_customerId)
            .WithProfessional(_professionalId);
        var appointment = builder.Build();
        appointment.Cancel();
        Assert.Equal(SchedulingState.Cancelled, appointment.State);
    }

    [Fact]
    public void Should_ThrowException_When_Cancelling_AppointmentWithPastDate()
    {
        var dateAndHour = DateTime.Now.AddDays(-1);
        var builder = new Appointment.Builder(dateAndHour)
            .WithServiceCatalog(_serviceCatalog)
            .WithCustomer(_customerId)
            .WithProfessional(_professionalId);
        Assert.Throws<BusinessException>(() => builder.Build().Cancel());
    }
    
    [Fact]
    public void Should_MakeAppointmentAvailable_WhenAppointmentIsScheduled()
    {
        var dateAndHour = DateTime.Now.AddDays(1);
        
        var builder = new Appointment.Builder(dateAndHour)
            .WithServiceCatalog(_serviceCatalog)
            .WithCustomer(_customerId)
            .WithProfessional(_professionalId);
        var appointment = builder.Build();
        appointment.Schedule();
        appointment.ToMakeAvailable();
        Assert.True(appointment.IsAvailable);
    }

    [Fact]
    public void Should_ThrowException_When_MakingPastAppointmentAvailable()
    {
        var dateAndHour = DateTime.Now.AddDays(-1);
        var builder = new Appointment.Builder(dateAndHour)
            .WithServiceCatalog(_serviceCatalog)
            .WithCustomer(_customerId)
            .WithProfessional(_professionalId);
        Assert.Throws<BusinessException>(() => builder.Build().ToMakeAvailable());
    }

    [Fact]
    public void Should_ThrowException_When_MakingAlreadyAvailableAppointmentAvailable()
    {
        var dateAndHour = DateTime.Now.AddDays(1);
        var builder = new Appointment.Builder(dateAndHour)
            .WithServiceCatalog(_serviceCatalog)
            .WithCustomer(_customerId)
            .WithProfessional(_professionalId);
        var appointment = builder.Build();
        Assert.Throws<BusinessException>(() => appointment.ToMakeAvailable());
    }
}