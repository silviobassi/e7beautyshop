namespace E7BeautyShop.Schedule
{
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
            {
                schedule.AddOfficeDay(new OfficeDay(dateAt));
            }
        }

        private static void AddOfficeHoursToDays(Schedule schedule, int intervalBetweenOfficeHours)
        {
            foreach (var day in schedule.OfficeDays)
            {
                var (start, end) = schedule.GetOfficeHours(day);
                AddOfficeHoursToDay(day, start, end, intervalBetweenOfficeHours);
            }
        }

        private static void AddOfficeHoursToDay(OfficeDay day, TimeSpan start, TimeSpan end, int interval)
        {
            for (var timeOfDay = start; timeOfDay <= end; timeOfDay = timeOfDay.Add(TimeSpan.FromMinutes(interval)))
            {
                day.AddOfficeHour(new OfficeHour(timeOfDay));
            }
        }
    }
}