using E7BeautyShop.AgendaService.Core.Services;
using E7BeautyShop.AgendaService.Core.Validations;
using Xunit.Abstractions;
using static E7BeautyShop.AgendaService.Core.Validations.Messages;

namespace E7BeautyShop.AgendaService.Tests.UnitTests.Core.Services;

public class HasAtLeastTwoItemsValidTest(ITestOutputHelper output)
{
    [Fact]
    public void Should_NotThrowException_When_AgendaEqualToOne()
    {
        var schedule = AgendaTestHelper.CreateSchedule();
        AgendaTestHelper.AddOfficeHours(schedule,
            AgendaTestHelper.CreateOfficeHour(2024, 06, 18, 8, 0, 0, 30));

        var newTime = AgendaTestHelper.CreateOfficeHour(2024, 06, 18, 9, 1, 0, 30);
        
        var hasAtLeastTwoItemsValid = new HasAtLeastTwoItemsValid(schedule.OfficeHours, newTime);
        var exception = Record.Exception(() => hasAtLeastTwoItemsValid.Validate());
        Assert.Null(exception);
    }
    
    [Fact]
    public void Should_NotThrowException_When_AgendaEqualToZero()
    {
        var schedule = AgendaTestHelper.CreateSchedule();

        var newTime = AgendaTestHelper.CreateOfficeHour(2024, 06, 18, 9, 1, 0, 30);
        
        var hasAtLeastTwoItemsValid = new HasAtLeastTwoItemsValid(schedule.OfficeHours, newTime);
        var exception = Record.Exception(() => hasAtLeastTwoItemsValid.Validate());
        Assert.Null(exception);
    }

    [Fact]
    public void Should_ThrowException_When_NewTimePlusDuration_GreaterThan_NexTime()
    {
        var agenda = AgendaTestHelper.CreateSchedule();
        AgendaTestHelper.AddOfficeHours(agenda,
            AgendaTestHelper.CreateOfficeHour(2024, 06, 18, 8, 0, 0, 30),
            AgendaTestHelper.CreateOfficeHour(2024, 06, 18, 8, 30, 0, 30));

        var newTime = AgendaTestHelper.CreateOfficeHour(2024, 06, 18, 7, 31, 0, 30);
        var exception =
            Assert.Throws<BusinessException>(
                () => new HasAtLeastTwoItemsValid(agenda.OfficeHours, newTime).Validate());
        Assert.Equal(NewTimeBeforeNextTime, exception.Message);
    }

    [Fact]
    public void Should_NotThrowException_When_NewTimePlusDuration_LessOrEqual_NexTime()
    {
        var agenda = AgendaTestHelper.CreateSchedule();
        AgendaTestHelper.AddOfficeHours(agenda,
            AgendaTestHelper.CreateOfficeHour(2024, 06, 18, 8, 0, 0, 30),
            AgendaTestHelper.CreateOfficeHour(2024, 06, 18, 8, 30, 0, 30));

        var newTime = AgendaTestHelper.CreateOfficeHour(2024, 06, 18, 7, 20, 0, 30);
        var exception = Record.Exception(() => new HasAtLeastTwoItemsValid(agenda.OfficeHours, newTime).Validate());
        Assert.Null(exception);
    }


    [Fact]
    public void
        Should_NotThrowException_When_NewTimePlusDuration_LessOrEqual_Next_And_PrevPlusDuration_LessOrEqual_NewTime()
    {
        var schedule = AgendaTestHelper.CreateSchedule();
        AgendaTestHelper.AddOfficeHours(schedule,
            AgendaTestHelper.CreateOfficeHour(2024, 06, 18, 8, 0, 0, 30),
            AgendaTestHelper.CreateOfficeHour(2024, 06, 18, 8, 30, 0, 30),
            AgendaTestHelper.CreateOfficeHour(2024, 06, 18, 9, 30, 0, 30));

        var newTime = AgendaTestHelper.CreateOfficeHour(2024, 06, 18, 9, 0, 0, 30);
        var hasAtLeastTwoItemsValid = new HasAtLeastTwoItemsValid(schedule.OfficeHours, newTime);
        var exception = Record.Exception(() => hasAtLeastTwoItemsValid.Validate());
        Assert.Null(exception);
    }

    [Fact]
    public void
        Should_ThrowException_When_NewTimePlusDuration_GreaterThan_Next_And_PrevPlusDuration_GreaterThan_NewTime()
    {
        var schedule = AgendaTestHelper.CreateSchedule();
        AgendaTestHelper.AddOfficeHours(schedule,
            AgendaTestHelper.CreateOfficeHour(2024, 06, 18, 8, 0, 0, 30),
            AgendaTestHelper.CreateOfficeHour(2024, 06, 18, 8, 30, 0, 30),
            AgendaTestHelper.CreateOfficeHour(2024, 06, 18, 9, 30, 0, 30));

        var newTime = AgendaTestHelper.CreateOfficeHour(2024, 06, 18, 9, 1, 0, 30);
        var hasAtLeastTwoItemsValid = new HasAtLeastTwoItemsValid(schedule.OfficeHours, newTime);
        var exception = Assert.Throws<BusinessException>(() => hasAtLeastTwoItemsValid.Validate());
        Assert.Equal(NewTimeBetweenPrevNext, exception.Message);
    }

    [Fact]
    public void Should_ThrowException_When_NewTime_LessThan_LastTimePlusDuration()
    {
        var agenda = AgendaTestHelper.CreateSchedule();
        AgendaTestHelper.AddOfficeHours(agenda,
            AgendaTestHelper.CreateOfficeHour(2024, 06, 18, 8, 0, 0, 30),
            AgendaTestHelper.CreateOfficeHour(2024, 06, 18, 8, 30, 0, 30),
            AgendaTestHelper.CreateOfficeHour(2024, 06, 18, 9, 40, 0, 30));

        var newTime = AgendaTestHelper.CreateOfficeHour(2024, 06, 18, 10, 0, 0, 30);

        var hasAtLeastTwoItemsValid = new HasAtLeastTwoItemsValid(agenda.OfficeHours, newTime);
        var exception = Assert.Throws<BusinessException>(() => hasAtLeastTwoItemsValid.Validate());
        Assert.Equal(NewTimeAfterPrevTime, exception.Message);
    }

    [Fact]
    public void Should_NotThrowException_When_NewTime_BiggerOrEqual_LastTimePlusDuration()
    {
        var agenda = AgendaTestHelper.CreateSchedule();
        AgendaTestHelper.AddOfficeHours(agenda,
            AgendaTestHelper.CreateOfficeHour(2024, 06, 18, 8, 0, 0, 30),
            AgendaTestHelper.CreateOfficeHour(2024, 06, 18, 8, 30, 0, 30),
            AgendaTestHelper.CreateOfficeHour(2024, 06, 18, 9, 30, 0, 30));

        var newTime = AgendaTestHelper.CreateOfficeHour(2024, 06, 18, 10, 0, 0, 30);

        var hasAtLeastTwoItemsValid = new HasAtLeastTwoItemsValid(agenda.OfficeHours, newTime);
        var exception = Record.Exception(() => hasAtLeastTwoItemsValid.Validate());
        Assert.Null(exception);
    }
}