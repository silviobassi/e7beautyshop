using E7BeautyShop.AgendaService.Domain.Entities;

namespace E7BeautyShop.AgendaService.Domain.Tests.UnitTests.Core;

public class SchedulingStateTest
{
    [Fact]
    public void Should_SchedulingState_Cancelled()
    {
        const SchedulingState state = SchedulingState.Cancelled;
        Assert.Equal("Cancelled", state.ToString());
    }
    
    [Fact]
    public void Should_SchedulingState_Scheduled()
    {
        const SchedulingState state = SchedulingState.Scheduled;
        Assert.Equal("Scheduled", state.ToString());
    }
}