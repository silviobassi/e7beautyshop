using E7BeautyShop.AgendaService.Core.Entities;
using E7BeautyShop.AgendaService.Core.Services;
using E7BeautyShop.AgendaService.Core.Validations;
using Xunit.Abstractions;
using static E7BeautyShop.AgendaService.Core.Validations.Messages;

namespace E7BeautyShop.AgendaService.Tests.UnitTests.Core;

public class AgendaTest(ITestOutputHelper output)
{
    private readonly TimeSpan _startWeekday = new(8, 0, 0);
    private readonly TimeSpan _endWeekday = new(18, 0, 0);
    private readonly TimeSpan _startWeekend = new(8, 0, 0);
    private readonly TimeSpan _endWeekend = new(12, 0, 0);

    [Fact]
    public void Should_CreateAgenda_WithValidParameters()
    {
        var startAt = DateTime.Now;
        var endAt = DateTime.Now.AddDays(7);
        var professionalId = Guid.NewGuid();
        var weekday = (_startWeekday, _endWeekday);
        var weekend = (_startWeekend, _endWeekend);

        var schedule = Agenda.Create(startAt, endAt, professionalId, weekday, weekend);

        Assert.NotNull(schedule);
        Assert.Equal(startAt, schedule.StartAt);
        Assert.Equal(endAt, schedule.EndAt);
        Assert.Equal(professionalId, schedule.ProfessionalId);
        Assert.Equal(weekday, schedule.Weekday);
        Assert.Equal(weekend, schedule.Weekend);
    }

    [Fact]
    public void Should_CreateAgenda_Between_Period()
    {
        var startAt = new DateTime(2024, 07, 04, 8, 0, 0, DateTimeKind.Utc);
        var endAt = startAt.AddDays(7);
        var professionalId = Guid.NewGuid();
        var weekday = (_startWeekday, _endWeekday);
        var weekend = (_startWeekend, _endWeekend);

        var agenda = Agenda.Create(startAt, endAt, professionalId, weekday, weekend);
        agenda.AddDayRest(DayRest.Create(DayOfWeek.Monday));
        
        var agendaHoursGenerator = new AgendaWorkingHoursGenerator(agenda);
        agendaHoursGenerator.Generate();

        Assert.NotNull(agenda);
        Assert.Equal(startAt, agenda.StartAt);
        Assert.Equal(endAt, agenda.EndAt);
        Assert.Equal(professionalId, agenda.ProfessionalId);
        Assert.Equal(weekday, agenda.Weekday);
        Assert.Equal(weekend, agenda.Weekend);

        Assert.NotEmpty(agenda.OfficeHours);
        Assert.NotEmpty(agenda.DaysRest);

        foreach (var time in agenda.OfficeHours)
        {
            if (!time.IsAvailable) continue;
            output.WriteLine(time.DateAndHour.ToString());
        }
    }

    [Fact]
    public void Should_ThrowException_WhenCreatingAgendaWithInvalidParameters()
    {
        var startAt = DateTime.MinValue;
        var endAt = DateTime.MinValue;
        var professionalId = Guid.NewGuid();
        var weekday = (_startWeekday, _endWeekday);
        var weekend = (_startWeekend, _endWeekend);

        var exception = Assert.Throws<BusinessException>(() =>
            Agenda.Create(startAt, endAt, professionalId, weekday, weekend));

        Assert.Equal(StartAtTooLow, exception.Message);
    }

    [Fact]
    public void Should_AddOfficeHour_WhenNotDayRest()
    {
        var startAt = new DateTime(2024, 06, 18, 8, 0, 0, DateTimeKind.Utc);
        var endAt = startAt.AddDays(7);
        var professionalId = Guid.NewGuid();
        var weekday = (_startWeekday, _endWeekday);
        var weekend = (_startWeekend, _endWeekend);
        var schedule = Agenda.Create(startAt, endAt, professionalId, weekday, weekend);
        var officeHour = OfficeHour.Create(startAt, 30);

        schedule.AddOfficeHour(officeHour);

        Assert.Contains(officeHour, schedule.OfficeHours);
    }

    [Fact]
    public void Should_IsNotAvailable_WhenDayRest()
    {
        var startAt = new DateTime(2024, 06, 18, 8, 0, 0, DateTimeKind.Utc);
        var endAt = startAt.AddDays(7);
        var professionalId = Guid.NewGuid();
        var weekday = (_startWeekday, _endWeekday);
        var weekend = (_startWeekend, _endWeekend);
        var schedule = Agenda.Create(startAt, endAt, professionalId, weekday, weekend);
        var officeHour = OfficeHour.Create(startAt, 30);
        var dayRest = DayRest.Create(startAt.DayOfWeek);

        schedule.AddDayRest(dayRest);
        schedule.AddOfficeHour(officeHour);

        Assert.False(officeHour.IsAvailable);
    }

    [Fact]
    public void Should_UpdateAgenda_WithValidParameters()
    {
        var startAt = new DateTime(2024, 06, 18, 8, 0, 0, DateTimeKind.Utc);
        var endAt = startAt.AddDays(7);
        var professionalId = Guid.NewGuid();
        var weekday = (_startWeekday, _endWeekday);
        var weekend = (_startWeekend, _endWeekend);
        var schedule = Agenda.Create(startAt, endAt, professionalId, weekday, weekend);

        var newStartAt = startAt.AddDays(1);
        var newEndAt = startAt.AddDays(8);
        var newProfessionalId = Guid.NewGuid();
        var newWeekday = (_startWeekday, _endWeekday);
        var newWeekend = (_startWeekend, _endWeekend);

        schedule.Update(schedule.Id, newStartAt, newEndAt, newProfessionalId, newWeekday, newWeekend);

        Assert.Equal(newStartAt, schedule.StartAt);
        Assert.Equal(newEndAt, schedule.EndAt);
        Assert.Equal(newProfessionalId, schedule.ProfessionalId);
        Assert.Equal(newWeekday, schedule.Weekday);
        Assert.Equal(newWeekend, schedule.Weekend);
    }
}