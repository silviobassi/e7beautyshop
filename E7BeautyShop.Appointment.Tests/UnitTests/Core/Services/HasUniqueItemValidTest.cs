namespace E7BeautyShop.Appointment.Tests.UnitTests.Core.Services;

public class HasUniqueItemValidTest
{
    [Fact]
    public void Should_Check_HasUniqueItem_InList()
    {
        var schedule = ScheduleTestHelper.CreateSchedule();
        var officeHour = ScheduleTestHelper.CreateOfficeHour(2024, 06, 18, 8, 0, 0, 30);
        schedule.AddOfficeHour(officeHour);
        var timeToSchedule = ScheduleTestHelper.CreateOfficeHour(2024, 06, 18, 8, 30, 0, 30);

        ScheduleTestHelper.ValidateSchedule(schedule, timeToSchedule, hasNoItemsValidExpected: false,
            hasUniqueValidExpected: true,
            hasAtLeastTwoValidExpected: false);
    }

    [Fact]
    public void Should_Check_HasUniqueItem_InList_If_CurrentTime_GreaterThan_TimeSchedule()
    {
        var schedule = ScheduleTestHelper.CreateSchedule();
        var currentGreaterTimeToSchedule = ScheduleTestHelper.CreateOfficeHour(2024, 06, 18, 9, 0, 0, 30);
        schedule.AddOfficeHour(currentGreaterTimeToSchedule);
        var timeToSchedule = ScheduleTestHelper.CreateOfficeHour(2024, 06, 18, 8, 0, 0, 30);

        ScheduleTestHelper.ValidateSchedule(schedule, timeToSchedule, hasNoItemsValidExpected: false,
            hasUniqueValidExpected: true,
            hasAtLeastTwoValidExpected: false);
    }

    [Fact]
    public void Should_Check_HasUniqueItem_InList_If_CurrentTime_LessThan_TimeSchedule()
    {
        var schedule = ScheduleTestHelper.CreateSchedule();
        var currentTimeLessThanTimeSchedule = ScheduleTestHelper.CreateOfficeHour(2024, 06, 18, 7, 30, 0, 30);
        schedule.AddOfficeHour(currentTimeLessThanTimeSchedule);
        var timeToSchedule = ScheduleTestHelper.CreateOfficeHour(2024, 06, 18, 8, 10, 0, 30);

        ScheduleTestHelper.ValidateSchedule(schedule, timeToSchedule, hasNoItemsValidExpected: false,
            hasUniqueValidExpected: true,
            hasAtLeastTwoValidExpected: false);
    }

    [Fact]
    public void Should_Check_HasUniqueItem_InList_If_TimeSchedule_BiggerOrEqual_CurrentTimePlusDuration()
    {
        var schedule = ScheduleTestHelper.CreateSchedule();
        var timeScheduleBiggerOrEqualCurrentTimePlusDuration =
            ScheduleTestHelper.CreateOfficeHour(2024, 06, 18, 8, 0, 0, 30);
        schedule.AddOfficeHour(timeScheduleBiggerOrEqualCurrentTimePlusDuration);
        var timeToSchedule = ScheduleTestHelper.CreateOfficeHour(2024, 06, 18, 8, 30, 0, 30);

        ScheduleTestHelper.ValidateSchedule(schedule, timeToSchedule, hasNoItemsValidExpected: false,
            hasUniqueValidExpected: true,
            hasAtLeastTwoValidExpected: false);
    }

    [Fact]
    public void Should_Check_HasUniqueItem_InList_If_TimeSchedulePlusDuration_LessOrEqual_CurrentTime()
    {
        var schedule = ScheduleTestHelper.CreateSchedule();
        var timeSchedulePlusDurationLessOrEqualCurrentTime =
            ScheduleTestHelper.CreateOfficeHour(2024, 06, 18, 9, 0, 0, 30);
        schedule.AddOfficeHour(timeSchedulePlusDurationLessOrEqualCurrentTime);
        var timeToSchedule = ScheduleTestHelper.CreateOfficeHour(2024, 06, 18, 7, 30, 0, 30);

        ScheduleTestHelper.ValidateSchedule(schedule, timeToSchedule, hasNoItemsValidExpected: false,
            hasUniqueValidExpected: true,
            hasAtLeastTwoValidExpected: false);
    }
}