namespace E7BeautyShop.Domain.Tests;

public class HourForSchedulingTest
{
   [Fact]
   public void Should_Create_HoursForWeekday()
   {
      var hourScheduling = new HourForScheduling().CreateHourWeekday(new TimeSpan(8, 0, 0));
      Assert.NotNull(hourScheduling);
      Assert.Equal(new TimeSpan(8, 0, 0), hourScheduling.HourWeekday);
   }
   
   [Fact]
   public void Should_Create_HoursForWeekend()
   {
      var hourScheduling = new HourForScheduling().CreateHourWeekend(new TimeSpan(8, 0, 0));
      Assert.NotNull(hourScheduling);
      Assert.Equal(new TimeSpan(8, 0, 0), hourScheduling.HourWeekend);
   }
   
   [Fact]
   public void Should_Create_HoursForHoliday()
   {
      var hourScheduling = new HourForScheduling().CreateHourHoliday(new TimeSpan(8, 0, 0));
      Assert.NotNull(hourScheduling);
      Assert.Equal(new TimeSpan(8, 0, 0), hourScheduling.HourHoliday);
   }
}