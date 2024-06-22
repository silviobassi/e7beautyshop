﻿using E7BeautyShop.Appointment.Core.Entities;
using E7BeautyShop.Appointment.Core.ObjectsValue;
using E7BeautyShop.Appointment.Core.Services;

namespace E7BeautyShop.Appointment.Tests.UnitTests.Core.Services;

public static class ScheduleTestHelper
{
    public static Schedule CreateSchedule()
    {
        var startAt = new DateTime(2024, 06, 18, 8, 0, 0, DateTimeKind.Utc);
        var endAt = new DateTime(2024, 07, 18, 12, 0, 0, DateTimeKind.Utc);
        ProfessionalId? professionalId = Guid.Parse("0bc3810b-f85c-4ea4-84e0-557726a65940");

        var weekday = (new TimeSpan(8, 0, 0), new TimeSpan(17, 0, 0));
        var weekend = (new TimeSpan(8, 0, 0), new TimeSpan(12, 0, 0));

        return Schedule.Create(startAt, endAt, professionalId, weekday, weekend);
    }

    public static OfficeHour CreateOfficeHour(int year, int month, int day, int hour, int minute, int second, int duration)
    {
        return OfficeHour.Create(new DateTime(year, month, day, hour, minute, second, DateTimeKind.Utc), duration);
    }

    public static void AddOfficeHours(Schedule schedule, params OfficeHour[] officeHours)
    {
        foreach (var officeHour in officeHours)
        {
            schedule.AddOfficeHour(officeHour);
        }
    }

    public static void ValidateHasNoItems(Schedule schedule, bool expected)
    {
        var hasNoItemsValid = new HasNoItemsValid(schedule.OfficeHours);
        Assert.Equal(expected, hasNoItemsValid.Validate());
    }

    public static void ValidateHasUniqueItem(Schedule schedule, OfficeHour timeToSchedule, bool expected)
    {
        var hasUniqueValid = new HasUniqueItemValid(schedule.OfficeHours, timeToSchedule);
        Assert.Equal(expected, hasUniqueValid.Validate());
    }

    public static void ValidateHasAtLeastTwoItems(Schedule schedule, OfficeHour timeToSchedule, bool expected)
    {
        var hasAtLeastTwoValid = new HasAtLeastTwoItemsValid(schedule.OfficeHours, timeToSchedule);
        Assert.Equal(expected, hasAtLeastTwoValid.Validate());
    }
}