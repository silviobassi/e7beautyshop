using E7BeautyShop.Appointment.Core;
using E7BeautyShop.Appointment.Core.Entities;

namespace E7BeautyShop.Appointment.Tests.Core;

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