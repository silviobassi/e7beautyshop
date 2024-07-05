using E7BeautyShop.AgendaService.Domain.Entities;

namespace E7BeautyShop.AgendaService.Domain.Tests.UnitTests.Core;

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