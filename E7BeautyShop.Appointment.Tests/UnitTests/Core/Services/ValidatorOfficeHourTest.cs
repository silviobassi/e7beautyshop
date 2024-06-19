using E7BeautyShop.Appointment.Core.Entities;
using E7BeautyShop.Appointment.Core.ObjectsValue;
using E7BeautyShop.Appointment.Core.Services;

namespace E7BeautyShop.Appointment.Tests.UnitTests.Core.Services;

public class ValidatorOfficeHourTest
{
    [Fact]
    public void Should_Check_If_Only_One_OfficeHour_Scheduled()
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
        var officeHour = OfficeHour.Create(new DateTime(2024, 06, 18, 8, 0, 0, DateTimeKind.Utc), 30);
        schedule.AddOfficeHour(officeHour);

        var checkScheduleManagement = new ValidatorOfficeHour(schedule.OfficeHours, officeHour);
        var result = checkScheduleManagement.IsOnlyOneOfficeHourScheduled;
        Assert.True(result);
    }

    [Fact]
    public void Should_Check_If_Previous_OfficeHour_Scheduled()
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
        var officeHour = OfficeHour.Create(new DateTime(2024, 06, 18, 8, 0, 0, DateTimeKind.Utc), 30);
        schedule.AddOfficeHour(officeHour);

        var officeHour2 = OfficeHour.Create(new DateTime(2024, 06, 18, 8, 30, 0, DateTimeKind.Utc), 30);
        
        var checkScheduleManagement = new ValidatorOfficeHour(schedule.OfficeHours, officeHour2);
        var result2 = checkScheduleManagement.IsPreviousOfficeHourScheduled();
        Assert.True(result2);
    }

    [Fact]
    public void Should_Check_If_Next_OfficeHour_Scheduled()
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
        var officeHour = OfficeHour.Create(new DateTime(2024, 06, 18, 8, 30, 0, DateTimeKind.Utc), 30);
        schedule.AddOfficeHour(officeHour);

        var officeHour2 = OfficeHour.Create(new DateTime(2024, 06, 18, 8, 0, 0, DateTimeKind.Utc), 30);

        var checkScheduleManagement = new ValidatorOfficeHour(schedule.OfficeHours, officeHour2);
        var result2 = checkScheduleManagement.IsNextOfficeHourScheduled();
        Assert.True(result2);
    }

    [Fact]
    public void Should_Check_If_OfficeHour_GreaterThan_OfficeHour_And_Duration_Scheduled()
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
        var officeHour = OfficeHour.Create(new DateTime(2024, 06, 18, 8, 0, 0, DateTimeKind.Utc), 30);
        schedule.AddOfficeHour(officeHour);

        var officeHour2 = OfficeHour.Create(new DateTime(2024, 06, 18, 8, 30, 0, DateTimeKind.Utc), 30);

        var checkScheduleManagement = new ValidatorOfficeHour(schedule.OfficeHours, officeHour2);
        var result2 = checkScheduleManagement.IsOfficeHourGreaterThanCurrentOfficeHour();
        Assert.True(result2);

    }
    
    [Fact]
    public void Should_Check_If_OfficeHour_And_Duration_LessThan_OfficeHour_Scheduled()
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
        var officeHour = OfficeHour.Create(new DateTime(2024, 06, 18, 8, 0, 0, DateTimeKind.Utc), 30);
        schedule.AddOfficeHour(officeHour);

        var officeHour2 = OfficeHour.Create(new DateTime(2024, 06, 18, 7, 30, 0, DateTimeKind.Utc), 30);

        var checkScheduleManagement = new ValidatorOfficeHour(schedule.OfficeHours, officeHour2);
        var result2 = checkScheduleManagement.IsOfficeHourLessThanCurrentOfficeHour();
        Assert.True(result2);
    }
    
    [Fact]
    public void Should_Check_If_Is_Two_OfficeHour_Scheduled()
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

        var checkScheduleManagement = new ValidatorOfficeHour(schedule.OfficeHours, officeHour1);
        var result = checkScheduleManagement.IsTwoOfficeHourScheduled;
        Assert.True(result);
    }
    
    [Fact]
    public void Should_Get_OfficeHour_Previous_And_Next()
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
        var officeHour2 = OfficeHour.Create(new DateTime(2024, 06, 18, 9, 30, 0, DateTimeKind.Utc), 30);
        schedule.AddOfficeHour(officeHour1);
        schedule.AddOfficeHour(officeHour2);

        var officeHour3 = OfficeHour.Create(new DateTime(2024, 06, 18, 8, 30, 0, DateTimeKind.Utc), 30);

        var checkScheduleManagement = new ValidatorOfficeHour(schedule.OfficeHours, officeHour3);

        Assert.Equal(officeHour1, checkScheduleManagement.PreviousTimeScheduled);
        Assert.Equal(officeHour2, checkScheduleManagement.NextTimeScheduled);
    }
    
    [Fact]
    public void Should_Check_If_Interval_Between_Previous_Next_IsBiggerOrEqual_60_Minutes()
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
        var officeHour2 = OfficeHour.Create(new DateTime(2024, 06, 18, 9, 30, 0, DateTimeKind.Utc), 30);
        schedule.AddOfficeHour(officeHour1);
        schedule.AddOfficeHour(officeHour2);

        var officeHour3 = OfficeHour.Create(new DateTime(2024, 06, 18, 8, 30, 0, DateTimeKind.Utc), 30);

        var checkScheduleManagement = new ValidatorOfficeHour(schedule.OfficeHours, officeHour3);

        Assert.Equal(officeHour1, checkScheduleManagement.PreviousTimeScheduled);
        Assert.Equal(officeHour2, checkScheduleManagement.NextTimeScheduled);

        var isBiggerOrEqual60Minutes = checkScheduleManagement.IsBiggerOrEqualTo;
        
        Assert.True(isBiggerOrEqual60Minutes);

    }
    
    /*
     * List<OfficeHour> officeHours =
        [
            OfficeHour.Create(new DateTime(2024, 06, 18, 8, 0, 0, DateTimeKind.Utc), 30),
            OfficeHour.Create(new DateTime(2024, 06, 18, 8, 30, 0, DateTimeKind.Utc), 30),
            OfficeHour.Create(new DateTime(2024, 06, 18, 9, 0, 0, DateTimeKind.Utc), 30),
            OfficeHour.Create(new DateTime(2024, 06, 18, 9, 30, 0, DateTimeKind.Utc), 30),
        ];
     */
}