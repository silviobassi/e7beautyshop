using E7BeautyShop.Appointment.Core.Entities;

namespace E7BeautyShop.Appointment.Tests.UnitTests.Core;

public class DayRestTest
{
    [Fact]
    private void Should_CreateDayRest()
    {
        const DayOfWeek dayOnWeek = DayOfWeek.Sunday;
        var dayRest = DayRest.Create(dayOnWeek);
        Assert.NotNull(dayRest);
        Assert.Equal(dayOnWeek, dayRest.DayOnWeek);
    }
}