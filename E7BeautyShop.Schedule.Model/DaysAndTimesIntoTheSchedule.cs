namespace E7BeautyShop.Schedule;

public static class DaysAndTimesIntoTheSchedule
{
    public static void Build(Schedule schedule, int intervalBetweenOfficeHours)
    {
        AddOfficeDaysToSchedule(schedule);
        AddOfficeHoursToDays(schedule, intervalBetweenOfficeHours);
    }

    private static void AddOfficeDaysToSchedule(Schedule schedule)
    {
        for (var dateAt = schedule.StartAt; dateAt <= schedule.EndAt; dateAt = dateAt.AddDays(1))
            schedule.AddOfficeDay(new OfficeDay(dateAt));
    }

    private static void AddOfficeHoursToDays(Schedule schedule, int intervalBetweenOfficeHours)
    {
        foreach (var day in from day in schedule.OfficeDays let isWeekday = Schedule.IsWeekday(day) select day)
        {
            var (start, end) = schedule.GetOfficeHours(day);
            for (var timeOfDay = start;
                 timeOfDay <= end;
                 timeOfDay = timeOfDay.Add(TimeSpan.FromMinutes(intervalBetweenOfficeHours)))
            {
                day.AddOfficeHour(new OfficeHour(timeOfDay));
            }
        }
    }
}