namespace E7BeautyShop.Schedule;

public sealed class Weekend(TimeSpan start, TimeSpan end) : WeekDayOrWeekend(start, end);