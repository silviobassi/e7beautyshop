using E7BeautyShop.Appointment.Core.DomainEvents;
using E7BeautyShop.Appointment.Core.Entities;
using E7BeautyShop.Appointment.Core.ObjectsValue;
using E7BeautyShop.Appointment.Core.Validations;
using Xunit.Abstractions;

namespace E7BeautyShop.Appointment.Tests.UnitTests.Core;

public class OfficeHourTest(ITestOutputHelper output)
{
    [Fact]
    public void Should_ReserveTimeForTheCustomer()
    {
        var dateAndHour = new DateTime(2024, 5, 30, 8, 0, 0, DateTimeKind.Local);
        var id = Guid.NewGuid();
        var officeHour = new OfficeHour(new ReserveRegisteredEvent());
        var serviceDescription = ("ServiceName", 10);
        var catalog = Catalog.Create(serviceDescription);
        CustomerId customerId = id;
        officeHour.ReserveTimeForTheCustomer(dateAndHour, customerId, catalog);

        Assert.NotNull(officeHour);
        Assert.Equal(dateAndHour, officeHour.DateAndHour);
        Assert.Equal(id, officeHour.CustomerId?.Value);
        Assert.Equal(catalog, officeHour.Catalog);
    }


    [Fact]
    public void Should_CreateOfficeHour()
    {
        var dateAndHour = new DateTime(2024, 5, 30, 10, 0, 0, DateTimeKind.Local);
        var officeHour = OfficeHour.Create(dateAndHour, 30);
        Assert.NotNull(officeHour);
        Assert.Equal(dateAndHour, officeHour.DateAndHour);
        Assert.Equal(30, officeHour.Duration);
    }

    [Fact]
    public void Should_ThrowException_When_TimeOfDayIsInvalid()
    {
        var dateAndHour = new DateTime(default, DateTimeKind.Local);

        var exception = Assert.Throws<ArgumentNullException>(() => OfficeHour.Create(dateAndHour, 30));
        Assert.Equal("Value cannot be null. (Parameter 'DateAndHour')", exception.Message);
    }

    [Fact]
    public void Should_Cancel_OfficeHour()
    {
        var dateAndHour = new DateTime(2024, 5, 30, 8, 0, 0, DateTimeKind.Local);
        var officeHour = OfficeHour.Create(dateAndHour, 30);
        officeHour.Cancel();

        Assert.False(officeHour.IsAvailable);
    }

    [Fact]
    public void Should_BeAvailable_AfterCreation()
    {
        var dateAndHour = new DateTime(2024, 5, 30, 8, 0, 0, DateTimeKind.Local);
        var officeHour = OfficeHour.Create(dateAndHour, 30);
        officeHour.Cancel();

        Assert.False(officeHour.IsAvailable);

        officeHour.Attend();
        Assert.True(officeHour.IsAvailable);
    }

    [Fact]
    public void Should_ReserveCancel_SetsCustomerIdToNull_WhenOfficeHourIsAvailable()
    {
        var dateAndHour = new DateTime(2024, 5, 30, 8, 0, 0, DateTimeKind.Local);
        var officeHour = new OfficeHour(new ReserveRegisteredEvent());
        var serviceDescription = ("ServiceName", 10);
        var catalog = Catalog.Create(serviceDescription);
        CustomerId customerId = Guid.NewGuid();
        officeHour.ReserveTimeForTheCustomer(dateAndHour, customerId, catalog);
        officeHour.ReserveCancel();

        Assert.Null(officeHour.CustomerId?.Value);
        Assert.True(officeHour.IsAvailable);
    }

    [Fact]
    public void Should_ReserveCancel_ThrowsBusinessException_WhenOfficeHourIsNotAvailable()
    {
        var dateAndHour = new DateTime(2024, 5, 30, 10, 0, 0, DateTimeKind.Local);
        var officeHour = OfficeHour.Create(dateAndHour, 30);

        var exception = Assert.Throws<BusinessException>(() => officeHour.ReserveCancel());

        Assert.Equal("OfficeHour is already attended", exception.Message);
    }

    [Fact]
    public void Should_ReserveCancel_ThrowsBusinessException_WhenOfficeHourHasNoCustomer()
    {
        var dateAndHour = new DateTime(2024, 5, 30, 10, 0, 0, DateTimeKind.Local);
        var officeHour = OfficeHour.Create(dateAndHour, 30);
        officeHour.Cancel();
        var exception = Assert.Throws<ArgumentNullException>(() => officeHour.ReserveCancel());
        Assert.Equal("Value cannot be null. (Parameter 'CustomerId')", exception.Message);
    }

    [Fact]
    public void Should_CreateCustomerRegisteredEvent()
    {
        var dateReserve = new DateTime(2024, 5, 30, 8, 0, 0, DateTimeKind.Local);
        const decimal priceService = 100;
        CustomerId customerId = Guid.NewGuid();
        const string serviceName = "ServiceName";
        var factory = new ReserveRegisteredEvent();
        var registeredEvent = factory.Create(customerId.Value, dateReserve, serviceName, priceService);

        Assert.NotNull(registeredEvent);
        Assert.NotEqual(Guid.Empty, registeredEvent.Id);
        Assert.NotEqual(DateTime.MinValue, registeredEvent.OccuredOn);
        Assert.Equal(customerId.Value, registeredEvent.CustomerId);
        Assert.Equal(dateReserve, registeredEvent.ReserveDateAndHour);
        Assert.Equal(serviceName, registeredEvent.ServiceName);
        Assert.Equal(priceService, registeredEvent.PriceService);
    }

    [Fact]
    public void Should_ReserveTimeForTheCustomer_Should_UpdatePropertiesAndFireEvent()
    {
        var reserveDateAndHour = new DateTime(2024, 5, 30, 14, 0, 0, DateTimeKind.Local);
        CustomerId customerId = Guid.NewGuid();
        var officeHour = new OfficeHour(new ReserveRegisteredEvent());
        const string serviceName = "ServiceName";
        ServiceDescription serviceDescription = (serviceName, 10);
        var catalog = Catalog.Create(serviceDescription);
        var eventFired = false;

        officeHour.OnDomainEventOccured += domainEvent =>
        {
            if (domainEvent is not ReserveRegisteredEvent reserveEvent) return;
            eventFired = true;
            Assert.Equal(customerId.Value, reserveEvent.CustomerId);
            Assert.Equal(reserveDateAndHour, reserveEvent.ReserveDateAndHour);
            Assert.Equal(serviceName, reserveEvent.ServiceName);
            Assert.Equal(10, reserveEvent.PriceService);
        };

        officeHour.ReserveTimeForTheCustomer(reserveDateAndHour, customerId, catalog);

        Assert.Equal(reserveDateAndHour, officeHour.DateAndHour);
        Assert.Equal(customerId.Value, officeHour.CustomerId?.Value);
        Assert.False(officeHour.IsAvailable);
        Assert.True(eventFired);
    }

    [Fact]
    public void Should_ThrowException_When_ReservedRegisteredEventFactory_IsNotInitialized()
    {
        var officeHour = new OfficeHour();
        var reserveDateAndHour = DateTime.Now;
        const string serviceName = "ServiceName";
        CustomerId customerId = Guid.NewGuid();
        var serviceDescription = (serviceName, 10);
        var catalog = Catalog.Create(serviceDescription);

        var exception = Assert.Throws<InvalidOperationException>(
            () => officeHour.ReserveTimeForTheCustomer(reserveDateAndHour, customerId, catalog));
        Assert.Equal("Reserved registered event factory is not initialized.", exception.Message);
    }

    [Fact]
    public void AddDuration_ShouldAddDurationToDateTime()
    {
        var initialDateTime = new DateTime(2022, 1, 1, 10, 0, 0, DateTimeKind.Local); // 10:00 AM
        const int duration = 30;
        var officeHour = OfficeHour.Create(initialDateTime, duration);

        var result = officeHour.PlusDuration();
        var expectedDateTime = new DateTime(2022, 1, 1, 10, 30, 0, DateTimeKind.Local); // 10:30 AM
        Assert.Equal(expectedDateTime, result);
    }
    
    [Fact]
    public void Should_ThrowException_WhenDuration_LessThan_MinimumDuration()
    {
        var dateAndHour = new DateTime(2024, 5, 30, 10, 0, 0, DateTimeKind.Local);
        var exception = Assert.Throws<BusinessException>(() => OfficeHour.Create(dateAndHour, 20));
        Assert.Equal($"Duration cannot be less than {OfficeHour.MinimumDuration} minutes", exception.Message);
    }
    
    [Fact]
    public void Should_ThrowNotException_WhenDuration_BiggerOrEqual_MinimumDuration()
    {
        var dateAndHour = new DateTime(2024, 5, 30, 10, 0, 0, DateTimeKind.Local);
        var exception = Record.Exception(() => OfficeHour.Create(dateAndHour, 30));
        Assert.Null(exception);
    }
    
}