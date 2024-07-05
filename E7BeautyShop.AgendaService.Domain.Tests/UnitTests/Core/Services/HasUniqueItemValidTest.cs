using E7BeautyShop.AgendaService.Domain.Services;
using E7BeautyShop.AgendaService.Domain.Validations;

namespace E7BeautyShop.AgendaService.Domain.Tests.UnitTests.Core.Services;

public class HasUniqueItemValidTest
{
    
    [Fact]
    public void Should_NotThrowException_When_Agenda_GreaterThanOne()
    {
        var schedule = AgendaTestHelper.CreateSchedule();
        var agenda1 = AgendaTestHelper.CreateOfficeHour(2024, 06, 18, 9, 0, 0, 30);
        var agenda2 = AgendaTestHelper.CreateOfficeHour(2024, 06, 18, 9, 30, 0, 30);
        schedule.AddOfficeHour(agenda1);
        schedule.AddOfficeHour(agenda2);
        
        var newTime = AgendaTestHelper.CreateOfficeHour(2024, 06, 18, 8, 31, 0, 30);

        var hasUniqueItemValid = new HasUniqueItemValid(schedule.OfficeHours, newTime);
        var exception = Record.Exception(() => hasUniqueItemValid.Validate());
        Assert.Null(exception);
    }
    
    [Fact]
    public void Should_NotThrowException_When_Agenda_EqualToZero()
    {
        var schedule = AgendaTestHelper.CreateSchedule();
        var newTime = AgendaTestHelper.CreateOfficeHour(2024, 06, 18, 8, 31, 0, 30);

        var hasUniqueItemValid = new HasUniqueItemValid(schedule.OfficeHours, newTime);
        var exception = Record.Exception(() => hasUniqueItemValid.Validate());
        Assert.Null(exception);
    }

    
    [Fact]
    public void Should_ThrowException_When_TimeToSchedulePlusDuration_GreaterThan_CurrentTime()
    {
        var schedule = AgendaTestHelper.CreateSchedule();
        var currentGreaterTimeToSchedule = AgendaTestHelper.CreateOfficeHour(2024, 06, 18, 9, 0, 0, 30);
        schedule.AddOfficeHour(currentGreaterTimeToSchedule);
        var timeToSchedule = AgendaTestHelper.CreateOfficeHour(2024, 06, 18, 8, 31, 0, 30);

        var hasUniqueItemValid = new HasUniqueItemValid(schedule.OfficeHours, timeToSchedule);
        var exception = Assert.Throws<BusinessException>(() => hasUniqueItemValid.Validate());
        Assert.Equal(Messages.NewTimeBefore, exception.Message);
    }
    
    [Fact]
    public void Should_NotThrowException_When_TimeToSchedulePlusDuration_LessOrEqual_CurrentTime()
    {
        var schedule = AgendaTestHelper.CreateSchedule();
        var currentGreaterTimeToSchedule = AgendaTestHelper.CreateOfficeHour(2024, 06, 18, 9, 0, 0, 30);
        schedule.AddOfficeHour(currentGreaterTimeToSchedule);
        var timeToSchedule = AgendaTestHelper.CreateOfficeHour(2024, 06, 18, 8, 30, 0, 30);

        var hasUniqueItemValid = new HasUniqueItemValid(schedule.OfficeHours, timeToSchedule);
        var exception = Record.Exception(() => hasUniqueItemValid.Validate());
        Assert.Null(exception);
    }

    [Fact]
    public void Should_ThrowException_When_TimeSchedule_LessThan_CurrentTimePlusDuration()
    {
        var schedule = AgendaTestHelper.CreateSchedule();
        var timeScheduleBiggerOrEqualCurrentTimePlusDuration =
            AgendaTestHelper.CreateOfficeHour(2024, 06, 18, 8, 10, 0, 30);
        schedule.AddOfficeHour(timeScheduleBiggerOrEqualCurrentTimePlusDuration);
        var timeToSchedule = AgendaTestHelper.CreateOfficeHour(2024, 06, 18, 8, 30, 0, 30);

        var hasUniqueItemValid = new HasUniqueItemValid(schedule.OfficeHours, timeToSchedule);
        var exception = Assert.Throws<BusinessException>(() => hasUniqueItemValid.Validate());
        Assert.Equal(Messages.NewTimeAfter, exception.Message);
    }
    
    [Fact]
    public void Should_NotThrowException_When_TimeSchedule_BiggerOrEqual_CurrentTimePlusDuration()
    {
        var schedule = AgendaTestHelper.CreateSchedule();
        var timeScheduleBiggerOrEqualCurrentTimePlusDuration =
            AgendaTestHelper.CreateOfficeHour(2024, 06, 18, 8, 0, 0, 30);
        schedule.AddOfficeHour(timeScheduleBiggerOrEqualCurrentTimePlusDuration);
        var timeToSchedule = AgendaTestHelper.CreateOfficeHour(2024, 06, 18, 8, 30, 0, 30);

        var hasUniqueItemValid = new HasUniqueItemValid(schedule.OfficeHours, timeToSchedule);
        var exception = Record.Exception(() => hasUniqueItemValid.Validate());
        Assert.Null(exception);
    }
}