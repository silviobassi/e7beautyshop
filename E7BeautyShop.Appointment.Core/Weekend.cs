namespace E7BeautyShop.Appointment.Core;

public sealed class Weekend(TimeSpan start, TimeSpan end) : WeekDayOrWeekend(start, end);