namespace E7BeautyShop.Schedule.Tests;

public class OfficeDayTest
{
    private static Weekend Weekend
    {
        get
        {
            var weekend = new Weekend(new TimeSpan(8, 0, 0), new TimeSpan(12, 0, 0));
            return weekend;
        }
    }

    private static Weekday Weekday
    {
        get
        {
            var weekday = new Weekday(new TimeSpan(8, 0, 0), new TimeSpan(18, 0, 0));
            return weekday;
        }
    }

    [Fact]
    public void Update_WithValidParameters_ShouldUpdateProperties()
    {
        var startAt = DateTime.Now;
        var officeDay = new OfficeDay(startAt, Weekday, Weekend, DayOfWeek.Monday);
        var newId = Guid.NewGuid();
        var newStartAt = startAt.AddDays(1);
        var newDayRest = DayOfWeek.Tuesday;
        var newWeekday = new Weekday(new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0));
        var newWeekend = new Weekend(new TimeSpan(9, 0, 0), new TimeSpan(12, 0, 0));

        officeDay.Update(newId, newStartAt, newWeekday, newWeekend, newDayRest);

        Assert.Equal(newId, officeDay.Id);
        Assert.Equal(newStartAt, officeDay.StartAt);
    }

    [Fact]
    public void Update_WithEmptyId_ShouldThrowBusinessException()
    {
        var officeDay = new OfficeDay(DateTime.Now, Weekday,Weekend, DayOfWeek.Monday);
        var exception = Assert.Throws<BusinessException>(() =>
            officeDay.Update(Guid.Empty, DateTime.Now, Weekday,Weekend, DayOfWeek.Tuesday));
        Assert.Equal("Id is required", exception.Message);
    }

    [Fact]
    public void Generate_WhenCalledOnWeekday_AddsOfficeHourToWeekday()
    {
        var officeDay = new OfficeDay(
            new DateTime(2024, 5, 23, 0, 0, 0, DateTimeKind.Local),
            Weekday, Weekend, DayOfWeek.Sunday);
        for (var i = Weekday.StartAt; i <= Weekday.EndAt; i += TimeSpan.FromMinutes(30))
        {
            var officeHour = new OfficeHour { Hour = i };
            officeDay.AddOfficeHour(officeHour);
        }

        Assert.Equal(21, officeDay.OfficeHours.Count);
    }


    [Fact]
    public void Generate_WhenCalledOnWeekend_AddsOfficeHourToWeekend()
    {
        var officeDay = new OfficeDay(
            new DateTime(2024, 5, 25, 0, 0, 0, DateTimeKind.Local),
            Weekday, Weekend, DayOfWeek.Sunday);
        for (var i = Weekend.StartAt; i <= Weekend.EndAt; i += TimeSpan.FromMinutes(30))
        {
            var officeHour = new OfficeHour { Hour = i };
            officeDay.AddOfficeHour(officeHour);
        }

        Assert.Equal(9, officeDay.OfficeHours.Count);
    }


    [Fact]
    public void Generate_CreateSchedule()
    {
        var startAt = new DateTime(2024, 5, 25, 0, 0, 0, DateTimeKind.Local);
        var officeDays = GenerateOfficeDays(startAt);
        Assert.Equal(30, officeDays.Count);
    }

        [Fact]
        public void Cancel_WhenIsAttendingIsTrue_ShouldSetIsAttendingToFalse()
        {
            var officeDay = new OfficeDay(
            new DateTime(2024, 5, 23, 0, 0, 0, DateTimeKind.Local),
            Weekday, Weekend, DayOfWeek.Sunday);
            officeDay.Cancel();
            Assert.False(officeDay.IsAttending);
        }

        [Fact]
        public void Cancel_WhenIsAttendingIsFalse_ShouldThrowModelBusinessException()
        {
            var officeDay = new OfficeDay(
            new DateTime(2024, 5, 23, 0, 0, 0, DateTimeKind.Local),
            Weekday, Weekend, DayOfWeek.Sunday);
            officeDay.Cancel();

            var exception = Assert.Throws<BusinessException>(() => officeDay.Cancel());
            Assert.Equal("Day is already canceled", exception.Message);
        }

    [Fact]
    public void Attend_ShouldSetIsAttendingToTrue()
    {
        var officeDay = new OfficeDay(
            new DateTime(2024, 5, 23, 0, 0, 0, DateTimeKind.Local),
            Weekday, Weekend, DayOfWeek.Sunday);
        officeDay.Cancel();
        officeDay.Attend();
        Assert.True(officeDay.IsAttending);
    }

    [Fact]
    public void Attend_WhenIsAttendingIsTrue_ShouldThrowModelBusinessException()
    {
        var officeDay = new OfficeDay(
            new DateTime(2024, 5, 23, 0, 0, 0, DateTimeKind.Local),
            Weekday, Weekend, DayOfWeek.Sunday);

        var exception = Assert.Throws<BusinessException>(() => officeDay.Attend());
        Assert.Equal("Day is already attending", exception.Message);
    }
    
    [Fact]
    public void AddOfficeHour_WhenCalledOnWeekday_AddsOfficeHourToWeekday()
    {
        var weekday = new Weekday(new TimeSpan(8, 0, 0), new TimeSpan(18, 0, 0));
        var officeDay = new OfficeDay(new DateTime(2024, 5, 20, 0, 0, 0, DateTimeKind.Local), weekday, Weekend, DayOfWeek.Sunday);
        var officeHour = new OfficeHour();
        officeDay.AddOfficeHour(officeHour);
        Assert.Contains(officeHour, officeDay.OfficeHours);
    }

    [Fact]
    public void AddOfficeHour_WhenCalledOnWeekend_AddsOfficeHourToWeekend()
    {
        var weekend = new Weekend(new TimeSpan(10, 0, 0), new TimeSpan(16, 0, 0));
        var officeDay = new OfficeDay(new DateTime(2024, 5, 25, 0, 0, 0, DateTimeKind.Local), Weekday, weekend, DayOfWeek.Sunday);
        var officeHour = new OfficeHour();
        officeDay.AddOfficeHour(officeHour);
        Assert.Contains(officeHour, officeDay.OfficeHours);
    }

    [Fact]
    public void AddOfficeHour_WhenCalledOnDayRest_DoesNotAddOfficeHour()
    {
        var weekday = new Weekday(new TimeSpan(8, 0, 0), new TimeSpan(18, 0, 0));
        var officeDay = new OfficeDay(new DateTime(2024, 5, 26, 0, 0, 0, DateTimeKind.Local), weekday, Weekend, DayOfWeek.Sunday);
        var officeHour = new OfficeHour();
        officeDay.AddOfficeHour(officeHour);
        Assert.DoesNotContain(officeHour, officeDay.OfficeHours);
    }


    private static List<OfficeDay> GenerateOfficeDays(DateTime startAt)
    {
        List<OfficeDay> officeDays = [];
        for (var d = 0; d < 30; d++)
        {
            var officeDay = new OfficeDay(startAt.AddDays(d), Weekday, Weekend, DayOfWeek.Sunday);
            officeDays.Add(officeDay);

            var start = officeDay.IsNotWeekday ? Weekend.StartAt : Weekday.StartAt;
            var end = officeDay.IsNotWeekday ? Weekend.EndAt : Weekday.EndAt;

            for (var i = start; i <= end; i += TimeSpan.FromMinutes(30))
            {
                var officeHour = new OfficeHour { Hour = i };
                officeDay.AddOfficeHour(officeHour);
            }
        }

        return officeDays;
    }
}