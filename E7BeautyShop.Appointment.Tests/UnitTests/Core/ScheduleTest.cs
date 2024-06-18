using E7BeautyShop.Appointment.Core.Entities;
using E7BeautyShop.Appointment.Core.ObjectsValue;
using E7BeautyShop.Appointment.Core.Services;
using E7BeautyShop.Appointment.Core.Validations;
using Xunit.Abstractions;

namespace E7BeautyShop.Appointment.Tests.UnitTests.Core;

public class ScheduleTest(ITestOutputHelper output)
{
    private readonly TimeSpan _startWeekday = new(8, 0, 0);
    private readonly TimeSpan _endWeekday = new(18, 0, 0);
    private readonly TimeSpan _startWeekend = new(8, 0, 0);
    private readonly TimeSpan _endWeekend = new(12, 0, 0);

    [Fact]
    public void Should_AddDayRest_WhenCalled_ShouldAddDayRestToList()
    {
        ProfessionalId professionalId = Guid.NewGuid();
        var weekday = (_startWeekday, _endWeekday);
        var weekend = (_startWeekend, _endWeekend);
        var schedule = Schedule.Create(DateTime.Now, DateTime.Now.AddDays(7), professionalId, weekday, weekend);
        var dayRest = DayRest.Create(DayOfWeek.Monday);

        var officeHour1 = OfficeHour.Create(DateTime.Now, 30);
        var officeHour2 = OfficeHour.Create(DateTime.Now.AddDays(1), 20);
        schedule.AddOfficeHour(officeHour1);
        schedule.AddOfficeHour(officeHour2);

        schedule.AddDayRest(dayRest);

        Assert.NotNull(schedule);
        Assert.Equal(professionalId, schedule.ProfessionalId);
        Assert.NotEmpty(schedule.OfficeHours);
        Assert.Equal(2, schedule.OfficeHours.Count);
    }

    [Fact]
    public void Should_RemoveDayRest_WhenCalled_ShouldRemoveDayRestFromList()
    {
        ProfessionalId professionalId = Guid.NewGuid();
        var weekday = (_startWeekday, _endWeekday);
        var weekend = (_startWeekend, _endWeekend);
        var schedule = Schedule.Create(DateTime.Now, DateTime.Now.AddDays(7), professionalId, weekday, weekend);
        var dayRest = DayRest.Create(DayOfWeek.Monday);

        schedule.AddDayRest(dayRest);
        schedule.RemoveDayRest(dayRest);

        Assert.NotNull(schedule);
        Assert.Equal(professionalId, schedule.ProfessionalId);
        Assert.Empty(schedule.DaysRest);
    }

    [Fact]
    public void IsWeekday_ReturnsFalse_WhenOfficeHourIsOnWeekend()
    {
        var officeHour = OfficeHour.Create(new DateTime(2024, 5, 26, 10, 0, 0, DateTimeKind.Local), 30);

        Weekday weekday = (_startWeekday, _endWeekday);
        Weekend weekend = (_startWeekend, _endWeekend);

        ProfessionalId professionalId = Guid.NewGuid();

        var schedule = Schedule.Create(DateTime.Now, DateTime.Now.AddDays(1), professionalId, weekday, weekend);
        var isWeekday = schedule.IsWeekday(officeHour);

        Assert.False(isWeekday);
    }

    [Fact]
    public void Should_AddOfficeHour_WhenNotDayRest()
    {
        var dateTime = new DateTime(2024, 5, 2, 0, 0, 0, DateTimeKind.Local);

        Weekday weekday = (_startWeekday, _endWeekday);
        Weekend weekend = (_startWeekend, _endWeekend);

        ProfessionalId professionalId = Guid.NewGuid();

        var schedule = Schedule.Create(DateTime.Now, dateTime, professionalId, weekday, weekend);
        var officeHour = OfficeHour.Create(new DateTime(2024, 5, 31, 10, 0, 0, DateTimeKind.Local), 30);
        schedule.AddDayRest(DayRest.Create(DayOfWeek.Friday));
        schedule.AddOfficeHour(officeHour);

        Assert.DoesNotContain(officeHour, schedule.OfficeHours);
    }

    [Fact]
    public void Should_Update_Schedule()
    {
        var officeHour = OfficeHour.Create(new DateTime(2024, 5, 26, 10, 0, 0, DateTimeKind.Local), 30);

        Weekday weekday = (_startWeekday, _endWeekday);
        Weekend weekend = (_startWeekend, _endWeekend);

        ProfessionalId professionalId = Guid.NewGuid();
        var schedule = Schedule.Create(DateTime.Now, DateTime.Now.AddDays(1), professionalId, weekday, weekend);
        var dayRest = DayRest.Create(DayOfWeek.Monday);
        schedule.AddDayRest(dayRest);
        schedule.AddOfficeHour(officeHour);

        var newStartAt = DateTime.Now.AddDays(1);
        var newEndAt = DateTime.Now.AddDays(2);
        var newWeekday = (_startWeekday, _endWeekday);
        var newWeekend = (_startWeekend, _endWeekend);
        var newProfessionalId = Guid.Parse("01b48b42-e04e-4d19-a805-df9a4a56394b");

        schedule.Update(schedule.Id, newStartAt, newEndAt, newProfessionalId, newWeekday, newWeekend);

        Assert.Equal(newStartAt, schedule.StartAt);
        Assert.Equal(newEndAt, schedule.EndAt);
        Assert.Equal(newProfessionalId, schedule.ProfessionalId);
        Assert.Equal(newWeekday, schedule.Weekday);
        Assert.Equal(newWeekend, schedule.Weekend);
        Assert.Single(schedule.DaysRest);
        Assert.Single(schedule.OfficeHours);
    }

    [Fact]
    public void Should_Check_PreviousAndNext_OfficeHour_GivenOfficeHour()
    {
        var dateAndHour = new DateTime(2024, 5, 30, 8, 40, 0, DateTimeKind.Local);
        var officeHour = OfficeHour.Create(dateAndHour, 30);

        var dateAndHour2 = new DateTime(2024, 5, 30, 8, 30, 0, DateTimeKind.Local);
        var officeHour2 = OfficeHour.Create(dateAndHour2, 30);

        var dateAndHour3 = new DateTime(2024, 5, 30, 10, 01, 0, DateTimeKind.Local);
        var officeHour3 = OfficeHour.Create(dateAndHour3, 30);

        var dateAndHour4 = new DateTime(2024, 5, 30, 10, 31, 0, DateTimeKind.Local);
        var officeHour4 = OfficeHour.Create(dateAndHour4, 30);

        var dateAndHour5 = new DateTime(2024, 5, 30, 11, 1, 0, DateTimeKind.Local);
        var officeHour5 = OfficeHour.Create(dateAndHour5, 30);

        Weekday weekday = (_startWeekday, _endWeekday);
        Weekend weekend = (_startWeekend, _endWeekend);

        ProfessionalId professionalId = Guid.NewGuid();

        var schedule = Schedule.Create(DateTime.Now, DateTime.Now.AddDays(1), professionalId, weekday, weekend);
        schedule.AddOfficeHour(officeHour2);
        schedule.AddOfficeHour(officeHour3);
        schedule.AddOfficeHour(officeHour4);
        schedule.AddOfficeHour(officeHour5);
        
        var assemblePreviousNext = new AssemblePreviousNext(officeHour, schedule.OfficeHours);
        var validatorOfficeHour = new ValidatorOfficeHour(officeHour, assemblePreviousNext.PreviousOfficeHour, 
            assemblePreviousNext.NextOfficeHour);
        
        var exceptionCheckPrevious = Assert.Throws<BusinessException>(() => validatorOfficeHour.ValidatePreviousOfficeHour());
        var exceptionCheckNext = Assert.Throws<BusinessException>(() => validatorOfficeHour.ValidateNextOfficeHour());
        
        Assert.Equal("Office hour is already attended", exceptionCheckPrevious.Message);
        Assert.Equal("Office hour cannot be less than 60 minutes between previous and next office hour", 
            exceptionCheckNext.Message);
    }
}