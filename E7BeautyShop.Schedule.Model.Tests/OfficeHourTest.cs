namespace E7BeautyShop.Schedule.Tests;

public class OfficeHourTest
{
    [Fact]
    private void Should_CreateOfficeHour()
    {
        var timeOfDay = new TimeSpan(8, 0, 0);
        var id = Guid.NewGuid();
        var officeHour = new OfficeHour();
        officeHour.ReserveTimeForTheCustomer(timeOfDay, new CustomerId(id));
        Assert.NotNull(officeHour);
        Assert.Equal(timeOfDay, officeHour.TimeOfDay);
        Assert.Equal(id, officeHour.CustomerId!.Id);
    }

    [Fact]
    private void Should_ThrowException_When_TimeOfDayIsInvalid()
    {
        Exception exception = Assert.Throws<BusinessException>(() =>
            new OfficeHour().CreateOfficeHour(new TimeSpan(0, 0, 0)));
        Assert.Equal("TimeOfDay cannot be empty", exception.Message);
    }

    [Fact]
    private void Should_Cancel_OfficeHour()
    {
        var officeHour = new OfficeHour();
        officeHour.CreateOfficeHour(new TimeSpan(8, 0, 0));
        
        /*Implementar neste método validação para quando houver a existência de cliente
          O atendimento não pode ser cancelado quando já tiver cliente agendado
          Somente o cliente pode cancelar (method: CancelledCustomer) o atendimento quando o horário já estiver reservado*/
        officeHour.Cancel();
        
        Assert.False(officeHour.IsAvailable);
    }

    [Fact]
    private void Should_BeAvailable_AfterCreation()
    {
        var officeHour = new OfficeHour();
        officeHour.CreateOfficeHour(new TimeSpan(8, 0, 0));
        officeHour.Cancel();
        Assert.False(officeHour.IsAvailable);
        officeHour.Attend();
        Assert.True(officeHour.IsAvailable);
    }

    [Fact]
    public void CustomerCancelled_SetsCustomerIdToNull_WhenOfficeHourIsAvailable()
    {
        var officeHourId = Guid.NewGuid();
        var officeHour = new OfficeHour();
        officeHour.ReserveTimeForTheCustomer(new TimeSpan(8, 0, 0), new CustomerId(Guid.NewGuid()));
        officeHour.CustomerCancelled(officeHourId);
        Assert.Null(officeHour.CustomerId);
        Assert.Equal(officeHourId, officeHour.Id);
        Assert.True(officeHour.IsAvailable);
    }


    [Fact]
    public void CustomerCancelled_ThrowsBusinessException_WhenOfficeHourIsNotAvailable()
    {
        var officeHour = new OfficeHour();
        officeHour.CreateOfficeHour(new TimeSpan(10, 0, 0));
        var exception = Assert.Throws<BusinessException>(() => officeHour.CustomerCancelled(Guid.NewGuid()));
        Assert.Equal("OfficeHour is already attended", exception.Message);
    }

    [Fact]
    public void CustomerCancelled_ThrowsBusinessException_WhenOfficeHourHasNoCustomer()
    {
        var officeHour = new OfficeHour();
        officeHour.CreateOfficeHour(new TimeSpan(10, 0, 0));
        officeHour.Cancel();
        var exception = Assert.Throws<BusinessException>(() => officeHour.CustomerCancelled(Guid.NewGuid()));
        Assert.Equal("OfficeHour has no customer", exception.Message);
    }
}