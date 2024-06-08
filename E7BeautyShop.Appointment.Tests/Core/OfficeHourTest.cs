﻿using E7BeautyShop.Appointment.Core;
using Xunit.Abstractions;

namespace E7BeautyShop.Appointment.Tests.Core;

public class OfficeHourTest(ITestOutputHelper output)
{
    [Fact]
    public void Should_CreateOfficeHour()
    {
        var dateAndHour = new DateTime(2024, 5, 30, 8, 0, 0, DateTimeKind.Local);
        var id = Guid.NewGuid();
        var officeHour = new OfficeHour(new ReserveRegisteredEvent());
        var serviceDescription = new ServiceDescription("ServiceName", 10);
        var catalog = new Catalog(serviceDescription);
        officeHour.ReserveTimeForTheCustomer(dateAndHour, new Customer(id), catalog);

        Assert.NotNull(officeHour);
        Assert.Equal(dateAndHour, officeHour.DateAndHour);
        Assert.Equal(id, officeHour.CustomerId?.Id);
        Assert.Equal(catalog, officeHour.Catalog);
    }

    [Fact]
    public void Should_ThrowException_When_TimeOfDayIsInvalid()
    {
        var dateAndHour = new DateTime(default, DateTimeKind.Local);

        var exception = Assert.Throws<ArgumentNullException>(() => new OfficeHour().CreateOfficeHour(dateAndHour));
        Assert.Equal("Value cannot be null. (Parameter 'DateAndHour')", exception.Message);
    }

    [Fact]
    public void Should_Cancel_OfficeHour()
    {
        var dateAndHour = new DateTime(2024, 5, 30, 8, 0, 0, DateTimeKind.Local);
        var officeHour = new OfficeHour();
        officeHour.CreateOfficeHour(dateAndHour);
        officeHour.Cancel();

        Assert.False(officeHour.IsAvailable);
    }

    [Fact]
    public void Should_BeAvailable_AfterCreation()
    {
        var dateAndHour = new DateTime(2024, 5, 30, 8, 0, 0, DateTimeKind.Local);
        var officeHour = new OfficeHour();
        officeHour.CreateOfficeHour(dateAndHour);
        officeHour.Cancel();

        Assert.False(officeHour.IsAvailable);

        officeHour.Attend();
        Assert.True(officeHour.IsAvailable);
    }

    [Fact]
    public void Should_ReserveCancel_SetsCustomerIdToNull_WhenOfficeHourIsAvailable()
    {
        var officeHourId = Guid.NewGuid();
        var dateAndHour = new DateTime(2024, 5, 30, 8, 0, 0, DateTimeKind.Local);
        var officeHour = new OfficeHour(new ReserveRegisteredEvent());
        var serviceDescription = new ServiceDescription("ServiceName", 10);
        var catalog = new Catalog(serviceDescription);
        officeHour.ReserveTimeForTheCustomer(dateAndHour, new Customer(Guid.NewGuid()), catalog);
        officeHour.ReserveCancel(officeHourId);

        Assert.Null(officeHour.CustomerId?.Id);
        Assert.Equal(officeHourId, officeHour.Id);
        Assert.True(officeHour.IsAvailable);
    }

    [Fact]
    public void Should_ReserveCancel_ThrowsBusinessException_WhenOfficeHourIsNotAvailable()
    {
        var officeHour = new OfficeHour();
        var dateAndHour = new DateTime(2024, 5, 30, 10, 0, 0, DateTimeKind.Local);
        officeHour.CreateOfficeHour(dateAndHour);

        var exception = Assert.Throws<BusinessException>(() => officeHour.ReserveCancel(Guid.NewGuid()));

        Assert.Equal("OfficeHour is already attended", exception.Message);
    }

    [Fact]
    public void Should_ReserveCancel_ThrowsBusinessException_WhenOfficeHourHasNoCustomer()
    {
        var dateAndHour = new DateTime(2024, 5, 30, 10, 0, 0, DateTimeKind.Local);
        var officeHour = new OfficeHour();
        officeHour.CreateOfficeHour(dateAndHour);
        officeHour.Cancel();

        var exception = Assert.Throws<BusinessException>(() => officeHour.ReserveCancel(Guid.NewGuid()));

        Assert.Equal("OfficeHour has no customer", exception.Message);
    }

    [Fact]
    public void Should_CreateCustomerRegisteredEvent()
    {
        var dateReserve = new DateTime(2024, 5, 30, 8, 0, 0, DateTimeKind.Local);
        const decimal priceService = 100;
        var customerId = Guid.NewGuid();
        const string serviceName = "ServiceName";
        var factory = new ReserveRegisteredEvent();
        var registeredEvent = factory.Create(customerId, dateReserve, serviceName, priceService);

        Assert.NotNull(registeredEvent);
        Assert.NotEqual(Guid.Empty, registeredEvent.Id);
        Assert.NotEqual(DateTime.MinValue, registeredEvent.OccuredOn);
        Assert.Equal(customerId, registeredEvent.CustomerId);
        Assert.Equal(dateReserve, registeredEvent.ReserveDateAndHour);
        Assert.Equal(serviceName, registeredEvent.ServiceName);
        Assert.Equal(priceService, registeredEvent.PriceService);
    }

    [Fact]
    public void Should_ReserveTimeForTheCustomer_Should_UpdatePropertiesAndFireEvent()
    {
        var reserveDateAndHour = new DateTime(2024, 5, 30, 14, 0, 0, DateTimeKind.Local);
        var customerId = new Customer(Guid.NewGuid());
        var officeHour = new OfficeHour(new ReserveRegisteredEvent());
        const string serviceName = "ServiceName";
        var serviceDescription = new ServiceDescription(serviceName, 10);
        var catalog = new Catalog(serviceDescription);
        var eventFired = false;

        officeHour.OnDomainEventOccured += domainEvent =>
        {
            if (domainEvent is not ReserveRegisteredEvent reserveEvent) return;
            eventFired = true;
            Assert.Equal(customerId.Id, reserveEvent.CustomerId);
            Assert.Equal(reserveDateAndHour, reserveEvent.ReserveDateAndHour);
            Assert.Equal(serviceName, reserveEvent.ServiceName);
            Assert.Equal(10, reserveEvent.PriceService);
        };

        officeHour.ReserveTimeForTheCustomer(reserveDateAndHour, customerId, catalog);

        Assert.Equal(reserveDateAndHour, officeHour.DateAndHour);
        Assert.Equal(customerId.Id, officeHour.CustomerId?.Id);
        Assert.False(officeHour.IsAvailable);
        Assert.True(eventFired);
    }

    [Fact]
    public void Should_ThrowException_When_ReservedRegisteredEventFactory_IsNotInitialized()
    {
        var officeHour = new OfficeHour();
        var reserveDateAndHour = DateTime.Now;
        const string serviceName = "ServiceName";
        var customerId = new Customer(Guid.NewGuid());
        var serviceDescription = new ServiceDescription(serviceName, 10);
        var catalog = new Catalog(serviceDescription);
        
        var exception = Assert.Throws<InvalidOperationException>(
            () => officeHour.ReserveTimeForTheCustomer(reserveDateAndHour, customerId, catalog));
        Assert.Equal("Reserved registered event factory is not initialized.", exception.Message);
    }
}