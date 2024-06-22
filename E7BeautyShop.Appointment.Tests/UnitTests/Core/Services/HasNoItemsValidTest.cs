namespace E7BeautyShop.Appointment.Tests.UnitTests.Core.Services;

public class HasNoItemsValidTest
{
    [Fact]
    public void Should_Check_HasNoItems_InList()
    {
        var schedule = ScheduleTestHelper.CreateSchedule();

        var timeToSchedule = ScheduleTestHelper.CreateOfficeHour(2024, 06, 18, 8, 30, 0, 30);

        ScheduleTestHelper.ValidateHasNoItems(schedule, expected: true);
        ScheduleTestHelper.ValidateHasUniqueItem(schedule, timeToSchedule, expected: false);
        ScheduleTestHelper.ValidateHasAtLeastTwoItems(schedule, timeToSchedule, expected: false);
    }
}