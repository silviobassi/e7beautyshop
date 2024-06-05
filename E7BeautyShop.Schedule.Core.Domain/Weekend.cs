namespace E7BeautyShop.Schedule.Core.Domain;

public sealed class Weekend(TimeSpan start, TimeSpan end) : WeekDayOrWeekend(start, end);