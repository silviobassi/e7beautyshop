﻿using E7BeautyShop.Appointment.Core.Entities;
using E7BeautyShop.Appointment.Core.ObjectsValue;
using E7BeautyShop.Appointment.Core.Services;

namespace E7BeautyShop.Appointment.Tests.UnitTests.Core.Services;

public class HasUniqueItemValidTest
{
    [Fact]
    public void Should_Check_HasUniqueItem_InList()
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
        schedule.AddOfficeHour(officeHour1);

        var timeToSchedule = OfficeHour.Create(new DateTime(2024, 06, 18, 8, 30, 0, DateTimeKind.Utc), 30);

        var hasNoItemsValid = new HasNoItemsValid(schedule.OfficeHours);
        var hasUniqueValid = new HasUniqueItemValid(schedule.OfficeHours, timeToSchedule);
        var hasAtLeastTwoValid = new HasAtLeastTwoItemsValid(schedule.OfficeHours, timeToSchedule);
        
        Assert.False(hasNoItemsValid.Validate());
        Assert.True(hasUniqueValid.Validate());
        Assert.False(hasAtLeastTwoValid.Validate());
    }

    [Fact]
    public void Should_Check_HasUniqueItem_InList_If_CurrentTime_GreaterThan_TimeSchedule()
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
        var currentGreaterTimeToSchedule = OfficeHour.Create(new DateTime(2024, 06, 18, 9, 0, 0, DateTimeKind.Utc), 30);
        schedule.AddOfficeHour(currentGreaterTimeToSchedule);

        var timeToSchedule = OfficeHour.Create(new DateTime(2024, 06, 18, 8, 0, 0, DateTimeKind.Utc), 30);

        var hasNoItemsValid = new HasNoItemsValid(schedule.OfficeHours);
        var hasUniqueValid = new HasUniqueItemValid(schedule.OfficeHours, timeToSchedule);
        var hasAtLeastTwoValid = new HasAtLeastTwoItemsValid(schedule.OfficeHours, timeToSchedule);
        
        Assert.False(hasNoItemsValid.Validate());
        Assert.True(hasUniqueValid.Validate());
        Assert.False(hasAtLeastTwoValid.Validate());
    }

    [Fact]
    public void Should_Check_HasUniqueItem_InList_If_CurrentTime_LessThan_TimeSchedule()
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
        var currentTimeLessThanTimeSchedule =
            OfficeHour.Create(new DateTime(2024, 06, 18, 7, 30, 0, DateTimeKind.Utc), 30);
        schedule.AddOfficeHour(currentTimeLessThanTimeSchedule);

        var timeToSchedule = OfficeHour.Create(new DateTime(2024, 06, 18, 8, 10, 0, DateTimeKind.Utc), 30);

        var hasNoItemsValid = new HasNoItemsValid(schedule.OfficeHours);
        var hasUniqueValid = new HasUniqueItemValid(schedule.OfficeHours, timeToSchedule);
        var hasAtLeastTwoValid = new HasAtLeastTwoItemsValid(schedule.OfficeHours, timeToSchedule);
        
        Assert.False(hasNoItemsValid.Validate());
        Assert.True(hasUniqueValid.Validate());
        Assert.False(hasAtLeastTwoValid.Validate());
    }

    [Fact]
    public void Should_Check_Has_UniqueItem_InList_If_TimeSchedule_BiggerOrEqual_CurrentTimePlusDuration()
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
        var timeScheduleBiggerOrEqualCurrentTimePlusDuration
            = OfficeHour.Create(new DateTime(2024, 06, 18, 8, 0, 0, DateTimeKind.Utc), 30);
        schedule.AddOfficeHour(timeScheduleBiggerOrEqualCurrentTimePlusDuration);

        var timeToSchedule = OfficeHour.Create(new DateTime(2024, 06, 18, 8, 30, 0, DateTimeKind.Utc), 30);

        var hasNoItemsValid = new HasNoItemsValid(schedule.OfficeHours);
        var hasUniqueValid = new HasUniqueItemValid(schedule.OfficeHours, timeToSchedule);
        var hasAtLeastTwoValid = new HasAtLeastTwoItemsValid(schedule.OfficeHours, timeToSchedule);
        
        Assert.False(hasNoItemsValid.Validate());
        Assert.True(hasUniqueValid.Validate());
        Assert.False(hasAtLeastTwoValid.Validate());
    }

    [Fact]
    public void Should_Check_HasUnique_Item_InList_If_TimeSchedulePlusDuration_LessOrEqual_CurrentTime()
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
        var timeSchedulePlusDurationLessOrEqualCurrentTime
            = OfficeHour.Create(new DateTime(2024, 06, 18, 9, 0, 0, DateTimeKind.Utc), 30);
        schedule.AddOfficeHour(timeSchedulePlusDurationLessOrEqualCurrentTime);

        var timeToSchedule = OfficeHour.Create(new DateTime(2024, 06, 18, 7, 30, 0, DateTimeKind.Utc), 30);

        var hasNoItemsValid = new HasNoItemsValid(schedule.OfficeHours);
        var hasUniqueValid = new HasUniqueItemValid(schedule.OfficeHours, timeToSchedule);
        var hasAtLeastTwoValid = new HasAtLeastTwoItemsValid(schedule.OfficeHours, timeToSchedule);
        
        Assert.False(hasNoItemsValid.Validate());
        Assert.True(hasUniqueValid.Validate());
        Assert.False(hasAtLeastTwoValid.Validate());
    }
}