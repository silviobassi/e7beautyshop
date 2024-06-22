namespace E7BeautyShop.Appointment.Tests.UnitTests.Core.Services;

public class HasAtLeastTwoItemsValidTest
{
    [Fact]
    public void Should_Check_HasAtLeastTwoItems_InList()
    {
        var schedule = ScheduleTestHelper.CreateSchedule();
        ScheduleTestHelper.AddOfficeHours(schedule,
            ScheduleTestHelper.CreateOfficeHour(2024, 06, 18, 8, 0, 0, 30),
            ScheduleTestHelper.CreateOfficeHour(2024, 06, 18, 8, 30, 0, 30));

        var timeToSchedule = ScheduleTestHelper.CreateOfficeHour(2024, 06, 18, 7, 20, 0, 30);

        ScheduleTestHelper.ValidateSchedule(schedule, timeToSchedule, hasNoItemsValidExpected: false,
            hasUniqueValidExpected: false, hasAtLeastTwoValidExpected: true);
    }

    [Fact]
    public void Should_Check_HasAtLeastTwoItems_InList_When_TimeToSchedule_LessThan_FirstItemList()
    {
        var schedule = ScheduleTestHelper.CreateSchedule();
        ScheduleTestHelper.AddOfficeHours(schedule,
            ScheduleTestHelper.CreateOfficeHour(2024, 06, 18, 8, 0, 0, 30),
            ScheduleTestHelper.CreateOfficeHour(2024, 06, 18, 8, 30, 0, 30));

        var timeToSchedule = ScheduleTestHelper.CreateOfficeHour(2024, 06, 18, 7, 30, 0, 30);

        ScheduleTestHelper.ValidateSchedule(schedule, timeToSchedule, hasNoItemsValidExpected: false,
            hasUniqueValidExpected: false, hasAtLeastTwoValidExpected: true);
    }

    [Fact]
    public void Should_Check_HasAtLeastTwoItems_InList_When_TimeToSchedule_GreaterThan_LastItemList()
    {
        var schedule = ScheduleTestHelper.CreateSchedule();
        ScheduleTestHelper.AddOfficeHours(schedule,
            ScheduleTestHelper.CreateOfficeHour(2024, 06, 18, 8, 0, 0, 30),
            ScheduleTestHelper.CreateOfficeHour(2024, 06, 18, 8, 30, 0, 30),
            ScheduleTestHelper.CreateOfficeHour(2024, 06, 18, 9, 30, 0, 30));

        var timeToSchedule = ScheduleTestHelper.CreateOfficeHour(2024, 06, 18, 9, 0, 0, 30);

        ScheduleTestHelper.ValidateSchedule(schedule, timeToSchedule, hasNoItemsValidExpected: false,
            hasUniqueValidExpected: false, hasAtLeastTwoValidExpected: true);
    }
}