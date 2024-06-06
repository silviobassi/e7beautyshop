namespace E7BeautyShop.Appointment.Core;

public sealed class Weekday(TimeSpan start, TimeSpan end) : WeekDayOrWeekend(start, end);