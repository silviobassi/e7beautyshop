namespace E7BeautyShop.Schedule.Core.Domain;

public sealed class Weekday(TimeSpan start, TimeSpan end) : WeekDayOrWeekend(start, end);