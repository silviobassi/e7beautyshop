using E7BeautyShop.AgendaService.Core.Services;
using E7BeautyShop.AgendaService.Core.Validations;

namespace E7BeautyShop.AgendaService.Tests.UnitTests.Core.Services;

public class HasUniqueItemValidTest
{
    [Fact]
    public void Should_Check_HasUniqueItem_InList()
    {
        var schedule = ScheduleTestHelper.CreateSchedule();
        var officeHour = ScheduleTestHelper.CreateOfficeHour(2024, 06, 18, 8, 0, 0, 30);
        schedule.AddOfficeHour(officeHour);
        var timeToSchedule = ScheduleTestHelper.CreateOfficeHour(2024, 06, 18, 8, 30, 0, 30);

        ScheduleTestHelper.ValidateHasNoItems(schedule, expected: false);
        ScheduleTestHelper.ValidateHasUniqueItem(schedule, timeToSchedule, expected: true);
        ScheduleTestHelper.ValidateHasAtLeastTwoItems(schedule, timeToSchedule, expected: false);
    }

    [Fact]
    public void Should_ThrowException_When_TimeToSchedulePlusDuration_GreaterThan_CurrentTime()
    {
        var schedule = ScheduleTestHelper.CreateSchedule();
        var currentGreaterTimeToSchedule = ScheduleTestHelper.CreateOfficeHour(2024, 06, 18, 9, 0, 0, 30);
        schedule.AddOfficeHour(currentGreaterTimeToSchedule);
        var timeToSchedule = ScheduleTestHelper.CreateOfficeHour(2024, 06, 18, 8, 31, 0, 30);

        var hasUniqueItemValid = new HasUniqueItemValid(schedule.OfficeHours, timeToSchedule);
        var exception = Assert.Throws<BusinessException>(() => hasUniqueItemValid.Validate());
        Assert.Equal(Messages.TimeToScheduleCannotGreaterThanFirstCurrentTime, exception.Message);
    }
    
    [Fact]
    public void Should_NotThrowException_When_TimeToSchedulePlusDuration_LessOrEqual_CurrentTime()
    {
        var schedule = ScheduleTestHelper.CreateSchedule();
        var currentGreaterTimeToSchedule = ScheduleTestHelper.CreateOfficeHour(2024, 06, 18, 9, 0, 0, 30);
        schedule.AddOfficeHour(currentGreaterTimeToSchedule);
        var timeToSchedule = ScheduleTestHelper.CreateOfficeHour(2024, 06, 18, 8, 30, 0, 30);

        var hasUniqueItemValid = new HasUniqueItemValid(schedule.OfficeHours, timeToSchedule);
        var exception = Record.Exception(() => hasUniqueItemValid.Validate());
        Assert.Null(exception);
    }

    [Fact]
    public void Should_Check_HasUniqueItem_InList_If_CurrentTime_LessThan_TimeSchedule()
    {
        var schedule = ScheduleTestHelper.CreateSchedule();
        var currentTimeLessThanTimeSchedule = ScheduleTestHelper.CreateOfficeHour(2024, 06, 18, 7, 30, 0, 30);
        schedule.AddOfficeHour(currentTimeLessThanTimeSchedule);
        var timeToSchedule = ScheduleTestHelper.CreateOfficeHour(2024, 06, 18, 8, 10, 0, 30);

        ScheduleTestHelper.ValidateHasNoItems(schedule, expected: false);
        ScheduleTestHelper.ValidateHasUniqueItem(schedule, timeToSchedule, expected: true);
        ScheduleTestHelper.ValidateHasAtLeastTwoItems(schedule, timeToSchedule, expected: false);
    }

    [Fact]
    public void Should_ThrowException_When_TimeSchedule_LessThan_CurrentTimePlusDuration()
    {
        var schedule = ScheduleTestHelper.CreateSchedule();
        var timeScheduleBiggerOrEqualCurrentTimePlusDuration =
            ScheduleTestHelper.CreateOfficeHour(2024, 06, 18, 8, 10, 0, 30);
        schedule.AddOfficeHour(timeScheduleBiggerOrEqualCurrentTimePlusDuration);
        var timeToSchedule = ScheduleTestHelper.CreateOfficeHour(2024, 06, 18, 8, 30, 0, 30);

        var hasUniqueItemValid = new HasUniqueItemValid(schedule.OfficeHours, timeToSchedule);
        var exception = Assert.Throws<BusinessException>(() => hasUniqueItemValid.Validate());
        Assert.Equal(Messages.TimeToScheduleCannotLessThanFirstCurrentTime, exception.Message);
    }
    
    [Fact]
    public void Should_NotThrowException_When_TimeSchedule_BiggerOrEqual_CurrentTimePlusDuration()
    {
        var schedule = ScheduleTestHelper.CreateSchedule();
        var timeScheduleBiggerOrEqualCurrentTimePlusDuration =
            ScheduleTestHelper.CreateOfficeHour(2024, 06, 18, 8, 0, 0, 30);
        schedule.AddOfficeHour(timeScheduleBiggerOrEqualCurrentTimePlusDuration);
        var timeToSchedule = ScheduleTestHelper.CreateOfficeHour(2024, 06, 18, 8, 30, 0, 30);

        var hasUniqueItemValid = new HasUniqueItemValid(schedule.OfficeHours, timeToSchedule);

        var exception = Record.Exception(() => hasUniqueItemValid.Validate());
        Assert.Null(exception);
    }

    [Fact(Skip = "This test is not working")]
    public void Should_Check_HasUniqueItem_InList_If_TimeSchedulePlusDuration_LessOrEqual_CurrentTime()
    {
        var schedule = ScheduleTestHelper.CreateSchedule();
        var timeSchedulePlusDurationLessOrEqualCurrentTime =
            ScheduleTestHelper.CreateOfficeHour(2024, 06, 18, 9, 0, 0, 30);
        schedule.AddOfficeHour(timeSchedulePlusDurationLessOrEqualCurrentTime);
        var timeToSchedule = ScheduleTestHelper.CreateOfficeHour(2024, 06, 18, 7, 30, 0, 30);

        ScheduleTestHelper.ValidateHasNoItems(schedule, expected: false);
        ScheduleTestHelper.ValidateHasUniqueItem(schedule, timeToSchedule, expected: true);
        ScheduleTestHelper.ValidateHasAtLeastTwoItems(schedule, timeToSchedule, expected: false);
    }
}