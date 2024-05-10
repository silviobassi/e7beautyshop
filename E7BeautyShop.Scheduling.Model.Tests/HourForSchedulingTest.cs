namespace E7BeautyShop.Domain.Tests;

public class HourForSchedulingTest
{
   [Fact]
   public void Should_Create_HoursForScheduling()
   {
      HourForScheduling hourScheduling = new (new TimeSpan(8, 0, 0));
      Assert.NotNull(hourScheduling);
      Assert.Equal(new TimeSpan(8, 0, 0), hourScheduling.Hour);
   }
}