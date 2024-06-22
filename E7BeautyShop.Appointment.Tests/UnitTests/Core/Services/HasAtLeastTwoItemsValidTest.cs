using E7BeautyShop.Appointment.Core.Entities;
using E7BeautyShop.Appointment.Core.ObjectsValue;
using E7BeautyShop.Appointment.Core.Services;

namespace E7BeautyShop.Appointment.Tests.UnitTests.Core.Services;

public class HasAtLeastTwoItemsValidTest
{
    [Fact]
    public void Should_Check_HasAtLeastTwoItems_InList()
    {
        var startAt = new DateTime(2024, 06, 18, 8, 0, 0, DateTimeKind.Utc);
        var endAt = new DateTime(2024, 07, 18, 12, 0, 0, DateTimeKind.Utc);

        ProfessionalId? professionalId = Guid.Parse("0bc3810b-f85c-4ea4-84e0-557726a65940");

        var startWeekday = new TimeSpan(8, 0, 0);
        var endWeekday = new TimeSpan(17, 0, 0);
        var startWeekend = new TimeSpan(8, 0, 0);
        var endWeekend = new TimeSpan(12, 0, 0);

        Weekday weekday = (startWeekday, endWeekday);
        Weekend weekend = (startWeekend, endWeekend);

        var schedule = Schedule.Create(startAt, endAt, professionalId, weekday, weekend);
        var officeHour1 = OfficeHour.Create(new DateTime(2024, 06, 18, 8, 0, 0, DateTimeKind.Utc), 30);
        var officeHour2 = OfficeHour.Create(new DateTime(2024, 06, 18, 8, 30, 0, DateTimeKind.Utc), 30);
        schedule.AddOfficeHour(officeHour1);
        schedule.AddOfficeHour(officeHour2);

        var timeToSchedule = OfficeHour.Create(new DateTime(2024, 06, 18, 7, 20, 0, DateTimeKind.Utc), 30);

        var hasNoItemsValid = new HasNoItemsValid(schedule.OfficeHours);
        var hasUniqueValid = new HasUniqueItemValid(schedule.OfficeHours, timeToSchedule);
        var hasAtLeastTwoValid = new HasAtLeastTwoItemsValid(schedule.OfficeHours, timeToSchedule);
        
        Assert.False(hasNoItemsValid.Validate());
        Assert.False(hasUniqueValid.Validate());
        Assert.True(hasAtLeastTwoValid.Validate());
    }

    [Fact]
    public void Should_Check_HasAtLeastTwoItems_InList_When_TimeToSchedule_LessThan_FirstItemList()
    {
        var startAt = new DateTime(2024, 06, 18, 8, 0, 0, DateTimeKind.Utc);
        var endAt = new DateTime(2024, 07, 18, 12, 0, 0, DateTimeKind.Utc);

        ProfessionalId? professionalId = Guid.Parse("0bc3810b-f85c-4ea4-84e0-557726a65940");

        var startWeekday = new TimeSpan(8, 0, 0);
        var endWeekday = new TimeSpan(17, 0, 0);
        var startWeekend = new TimeSpan(8, 0, 0);
        var endWeekend = new TimeSpan(12, 0, 0);

        Weekday weekday = (startWeekday, endWeekday);
        Weekend weekend = (startWeekend, endWeekend);

        var schedule = Schedule.Create(startAt, endAt, professionalId, weekday, weekend);
        var officeHour1 = OfficeHour.Create(new DateTime(2024, 06, 18, 8, 0, 0, DateTimeKind.Utc), 30);
        var officeHour2 = OfficeHour.Create(new DateTime(2024, 06, 18, 8, 30, 0, DateTimeKind.Utc), 30);
        schedule.AddOfficeHour(officeHour1);
        schedule.AddOfficeHour(officeHour2);

        var timeToSchedule = OfficeHour.Create(new DateTime(2024, 06, 18, 7, 30, 0, DateTimeKind.Utc), 30);

        var hasNoItemsValid = new HasNoItemsValid(schedule.OfficeHours);
        var hasUniqueValid = new HasUniqueItemValid(schedule.OfficeHours, timeToSchedule);
        var hasAtLeastTwoValid = new HasAtLeastTwoItemsValid(schedule.OfficeHours, timeToSchedule);
        
        Assert.False(hasNoItemsValid.Validate());
        Assert.False(hasUniqueValid.Validate());
        Assert.True(hasAtLeastTwoValid.Validate());
    }

    [Fact]
    public void Should_Check_HasAtLeastTwo_Items_InList_When_TimeToSchedule_GreaterThan_LastItemList()
    {
        var startAt = new DateTime(2024, 06, 18, 8, 0, 0, DateTimeKind.Utc);
        var endAt = new DateTime(2024, 07, 18, 12, 0, 0, DateTimeKind.Utc);

        ProfessionalId? professionalId = Guid.Parse("0bc3810b-f85c-4ea4-84e0-557726a65940");

        var startWeekday = new TimeSpan(8, 0, 0);
        var endWeekday = new TimeSpan(17, 0, 0);
        var startWeekend = new TimeSpan(8, 0, 0);
        var endWeekend = new TimeSpan(12, 0, 0);

        Weekday weekday = (startWeekday, endWeekday);
        Weekend weekend = (startWeekend, endWeekend);

        var schedule = Schedule.Create(startAt, endAt, professionalId, weekday, weekend);
        var officeHour1 = OfficeHour.Create(new DateTime(2024, 06, 18, 8, 0, 0, DateTimeKind.Utc), 30);
        var officeHour2 = OfficeHour.Create(new DateTime(2024, 06, 18, 8, 30, 0, DateTimeKind.Utc), 30);
        var officeHour3 = OfficeHour.Create(new DateTime(2024, 06, 18, 9, 30, 0, DateTimeKind.Utc), 30);
        schedule.AddOfficeHour(officeHour1);
        schedule.AddOfficeHour(officeHour2);
        schedule.AddOfficeHour(officeHour3);

        var timeToSchedule = OfficeHour.Create(new DateTime(2024, 06, 18, 9, 0, 0, DateTimeKind.Utc), 30);

        var hasNoItemsValid = new HasNoItemsValid(schedule.OfficeHours);
        var hasUniqueValid = new HasUniqueItemValid(schedule.OfficeHours, timeToSchedule);
        var hasAtLeastTwoValid = new HasAtLeastTwoItemsValid(schedule.OfficeHours, timeToSchedule);
        
        Assert.False(hasNoItemsValid.Validate());
        Assert.False(hasUniqueValid.Validate());
        Assert.True(hasAtLeastTwoValid.Validate());
    }
}