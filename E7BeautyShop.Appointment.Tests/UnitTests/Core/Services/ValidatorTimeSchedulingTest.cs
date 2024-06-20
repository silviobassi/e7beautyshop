using E7BeautyShop.Appointment.Core.Entities;
using E7BeautyShop.Appointment.Core.ObjectsValue;
using E7BeautyShop.Appointment.Core.Services;
using E7BeautyShop.Appointment.Core.Validations;

namespace E7BeautyShop.Appointment.Tests.UnitTests.Core.Services;

public class ValidatorTimeSchedulingTest
{
    [Fact]
    public void Should_Validate_OneOnlyTime_When_IsScheduled_Previous()
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