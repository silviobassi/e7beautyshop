using E7BeautyShop.Appointment.Core.Entities;
using E7BeautyShop.Appointment.Core.ObjectsValue;
using E7BeautyShop.Appointment.Core.Services;
using E7BeautyShop.Appointment.Core.Validations;

namespace E7BeautyShop.Appointment.Tests.UnitTests.Core.Services;

public class ValidatorTimeSchedulingTest
{
    [Fact]
    public void Should_Check_Has_Unique_Item_InList()
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

        var validatorTime = new ValidatorTimeScheduling(schedule.OfficeHours, timeToSchedule);
        var validate = validatorTime.Validate();
        Assert.True(validate);
    }

    [Fact]
    public void Should_Check_Has_Unique_Item_InList_If_CurrentTime_GreaterThan_TimeSchedule()
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

        var validatorTime = new ValidatorTimeScheduling(schedule.OfficeHours, timeToSchedule);
        var validate = validatorTime.Validate();
        Assert.True(validate);
    }

    [Fact]
    public void Should_Check_Has_Unique_Item_InList_If_CurrentTime_LessThan_TimeSchedule()
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

        var timeToSchedule = OfficeHour.Create(new DateTime(2024, 06, 18, 8, 0, 0, DateTimeKind.Utc), 30);

        var validatorTime = new ValidatorTimeScheduling(schedule.OfficeHours, timeToSchedule);
        var validate = validatorTime.Validate();
        Assert.True(validate);
    }

    [Fact]
    public void Should_Check_Has_Unique_Item_InList_If_CurrentTime_LessEqual_TimeSchedule()
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
        var currentTimeLessEqualTimeSchedule =
            OfficeHour.Create(new DateTime(2024, 06, 18, 8, 0, 0, DateTimeKind.Utc), 30);
        schedule.AddOfficeHour(currentTimeLessEqualTimeSchedule);

        var timeToSchedule = OfficeHour.Create(new DateTime(2024, 06, 18, 8, 0, 0, DateTimeKind.Utc), 30);

        var validatorTime = new ValidatorTimeScheduling(schedule.OfficeHours, timeToSchedule);
        var exception = Assert.Throws<BusinessException>(() => validatorTime.Validate());
        Assert.Equal("Time to schedule can not be equal to the first or last time", exception.Message);
    }

    [Fact]
    public void Should_Check_Has_Unique_Item_InList_If_TimeSchedule_BiggerOrEqual_CurrentTimePlusDuration()
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
        var currentTimeBiggerOrEqualToCurrentTime
            = OfficeHour.Create(new DateTime(2024, 06, 18, 8, 0, 0, DateTimeKind.Utc), 30);
        schedule.AddOfficeHour(currentTimeBiggerOrEqualToCurrentTime);

        var timeToSchedule = OfficeHour.Create(new DateTime(2024, 06, 18, 8, 30, 0, DateTimeKind.Utc), 30);

        var validatorTime = new ValidatorTimeScheduling(schedule.OfficeHours, timeToSchedule);
        Assert.True(validatorTime.Validate());
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