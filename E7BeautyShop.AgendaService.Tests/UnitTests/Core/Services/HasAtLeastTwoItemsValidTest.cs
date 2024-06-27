using E7BeautyShop.AgendaService.Core.Services;
using E7BeautyShop.AgendaService.Core.Validations;

using static E7BeautyShop.AgendaService.Core.Validations.Messages;

namespace E7BeautyShop.AgendaService.Tests.UnitTests.Core.Services;

public class HasAtLeastTwoItemsValidTest
{
    [Fact]
    public void Should_Return_False_If_Less_Than_Two_Items()
    {
        var schedule = ScheduleTestHelper.CreateSchedule();
        var newTime = ScheduleTestHelper.CreateOfficeHour(2024, 06, 18, 8, 0, 0, 30);

        ScheduleTestHelper.ValidateHasNoItems(schedule, expected: true);
        ScheduleTestHelper.ValidateHasUniqueItem(schedule, newTime, expected: false);
        ScheduleTestHelper.ValidateHasAtLeastTwoItems(schedule, newTime, expected: false);
    }

    [Fact]
    public void
        Should_Return_False_If_TimeToSchedule_GreaterThan_CurrentTime_And_LessThan_Last_Scheduled_Plus_Duration()
    {
        var schedule = ScheduleTestHelper.CreateSchedule();
        var lastScheduled = ScheduleTestHelper.CreateOfficeHour(2024, 06, 18, 8, 0, 0, 30);
        schedule.AddOfficeHour(lastScheduled);
        var newTime = ScheduleTestHelper.CreateOfficeHour(2024, 06, 18, 8, 29, 0, 30);

        ScheduleTestHelper.ValidateHasNoItems(schedule, expected: false);
        ScheduleTestHelper.ValidateHasAtLeastTwoItems(schedule, newTime, expected: false);
    }

    
    [Fact]
    public void Should_ThrowException_When_NewTimePlusDuration_GreaterThan_NexTime()
    {
        var schedule = ScheduleTestHelper.CreateSchedule();
        ScheduleTestHelper.AddOfficeHours(schedule,
            ScheduleTestHelper.CreateOfficeHour(2024, 06, 18, 8, 0, 0, 30),
            ScheduleTestHelper.CreateOfficeHour(2024, 06, 18, 8, 30, 0, 30));

        var newTime = ScheduleTestHelper.CreateOfficeHour(2024, 06, 18, 7, 31, 0, 30);
        var exception = Assert.Throws<BusinessException>(() => new HasAtLeastTwoItemsValid(schedule.OfficeHours, newTime).Validate());
        Assert.Equal(NewTimeBeforeNextTime, exception.Message);
    }

    [Fact]
    public void Should_NotThrowException_When_NewTimePlusDuration_LessOrEqual_NexTime()
    {
        var schedule = ScheduleTestHelper.CreateSchedule();
        ScheduleTestHelper.AddOfficeHours(schedule,
            ScheduleTestHelper.CreateOfficeHour(2024, 06, 18, 8, 0, 0, 30),
            ScheduleTestHelper.CreateOfficeHour(2024, 06, 18, 8, 30, 0, 30));

        var newTime = ScheduleTestHelper.CreateOfficeHour(2024, 06, 18, 7, 20, 0, 30);
        var exception = Record.Exception(() => new HasAtLeastTwoItemsValid(schedule.OfficeHours, newTime).Validate());
        Assert.Null(exception);
    }

    [Fact]
    public void Should_Check_HasAtLeastTwoItems_InList_When_TimeToSchedule_LessThan_FirstItemList()
    {
        var schedule = ScheduleTestHelper.CreateSchedule();
        ScheduleTestHelper.AddOfficeHours(schedule,
            ScheduleTestHelper.CreateOfficeHour(2024, 06, 18, 8, 0, 0, 30),
            ScheduleTestHelper.CreateOfficeHour(2024, 06, 18, 8, 30, 0, 30));

        var timeToSchedule = ScheduleTestHelper.CreateOfficeHour(2024, 06, 18, 7, 30, 0, 30);

        ScheduleTestHelper.ValidateHasNoItems(schedule, expected: false);
        ScheduleTestHelper.ValidateHasUniqueItem(schedule, timeToSchedule, expected: false);
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

        ScheduleTestHelper.ValidateHasNoItems(schedule, expected: false);
        ScheduleTestHelper.ValidateHasUniqueItem(schedule, timeToSchedule, expected: false);
        ScheduleTestHelper.ValidateHasAtLeastTwoItems(schedule, timeToSchedule, expected: true);
    }

    [Fact]
    public void Should_Check_HasAtLeastTwoItems_InList_When_TimeToSchedule_GreaterThan_CurrentTime()
    {
        var schedule = ScheduleTestHelper.CreateSchedule();
        ScheduleTestHelper.AddOfficeHours(schedule,
            ScheduleTestHelper.CreateOfficeHour(2024, 06, 18, 8, 0, 0, 30),
            ScheduleTestHelper.CreateOfficeHour(2024, 06, 18, 8, 30, 0, 30),
            ScheduleTestHelper.CreateOfficeHour(2024, 06, 18, 9, 30, 0, 30));

        var timeToSchedule = ScheduleTestHelper.CreateOfficeHour(2024, 06, 18, 10, 0, 0, 30);

        ScheduleTestHelper.ValidateHasNoItems(schedule, expected: false);
        ScheduleTestHelper.ValidateHasUniqueItem(schedule, timeToSchedule, expected: false);
        ScheduleTestHelper.ValidateHasAtLeastTwoItems(schedule, timeToSchedule, expected: true);
    }

    [Fact]
    public void Validate_ReturnsFalse_WhenNoneOfTheConditionsAreMet()
    {
        var schedule = ScheduleTestHelper.CreateSchedule();
        ScheduleTestHelper.AddOfficeHours(schedule,
            ScheduleTestHelper.CreateOfficeHour(2024, 06, 18, 8, 0, 0, 30));
        
        var timeToSchedule = ScheduleTestHelper.CreateOfficeHour(2024, 06, 18, 10, 0, 0, 30);
        
        ScheduleTestHelper.ValidateHasAtLeastTwoItems(schedule, timeToSchedule, expected: false);
    }
    
    [Fact]
    public void Should_ReturnsTrue_WhenTimeToScheduleIsGreaterThanPrevTimeAndLessThanNextTimeAndPrevTimePlusDurationIsLessThanTimeToSchedule()
    {
        var schedule = ScheduleTestHelper.CreateSchedule();
        
        ScheduleTestHelper.AddOfficeHours(schedule,
            ScheduleTestHelper.CreateOfficeHour(2024, 06, 18, 8, 0, 0, 30),
            ScheduleTestHelper.CreateOfficeHour(2024, 06, 18, 8, 30, 0, 30),
            ScheduleTestHelper.CreateOfficeHour(2024, 06, 18, 9, 30, 0, 30));
        
        var timeToSchedule = ScheduleTestHelper.CreateOfficeHour(2024, 06, 18, 9, 0, 0, 30);
        ScheduleTestHelper.ValidateHasAtLeastTwoItems(schedule, timeToSchedule, expected: true);
    }
}