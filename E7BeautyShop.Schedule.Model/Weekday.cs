namespace E7BeautyShop.Schedule;

public sealed class Weekday(TimeSpan start, TimeSpan end) : WeekDayOrWeekend(start, end);