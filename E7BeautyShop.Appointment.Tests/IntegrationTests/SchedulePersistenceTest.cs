using E7BeautyShop.Appointment.Application.Ports;
using E7BeautyShop.Appointment.Core.Entities;
using E7BeautyShop.Appointment.Core.ObjectsValue;
using Microsoft.Extensions.DependencyInjection;

namespace E7BeautyShop.Appointment.Tests.IntegrationTests
{
    public class SchedulePersistenceTests(TestStartup startup) : IClassFixture<TestStartup>
    {
        private readonly ISchedulePersistencePort _schedulePersistence =
            startup.ServiceProvider.GetRequiredService<ISchedulePersistencePort>();

        [Fact]
        public async Task Should_Persistence_Schedule()
        {
            var startAt = DateTime.Now;
            var endAt = DateTime.Now.AddDays(4);

            var id = Guid.Parse("e2977a5c-f1d6-46fc-8f7e-f78d0dda568c");
            ProfessionalId professionalId = id;
            Weekday weekday = (TimeSpan.FromHours(8), TimeSpan.FromHours(18));
            Weekend weekend = (TimeSpan.FromHours(8), TimeSpan.FromHours(12));

            var schedule = Schedule.Create(startAt, endAt, professionalId, weekday, weekend);

            var dayRest = DayRest.Create(DayOfWeek.Sunday);
            schedule.AddDayRest(dayRest);
            await _schedulePersistence.CreateAsync(schedule);

            var currentSchedule = await _schedulePersistence.GetByIdAsync(schedule.Id);

            Assert.NotNull(currentSchedule);
            Assert.Equal(schedule.Id, currentSchedule.Id);
            Assert.Equal(schedule.StartAt, currentSchedule.StartAt);
            Assert.Equal(schedule.EndAt, currentSchedule.EndAt);
            Assert.Equal(schedule.ProfessionalId, currentSchedule.ProfessionalId);
            Assert.Equal(schedule.Weekday, currentSchedule.Weekday);
            Assert.Equal(schedule.Weekend, currentSchedule.Weekend);
        }
    }
}