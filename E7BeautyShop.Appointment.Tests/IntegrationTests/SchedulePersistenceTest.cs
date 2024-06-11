using E7BeautyShop.Appointment.Application.Ports;
using E7BeautyShop.Appointment.Core.Entities;
using E7BeautyShop.Appointment.Core.ObjectsValue;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace E7BeautyShop.Appointment.Tests.IntegrationTests
{
    public class SchedulePersistenceTests : IClassFixture<TestStartup>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SchedulePersistenceTests(TestStartup startup)
        {
            _unitOfWork = startup.ServiceProvider.GetRequiredService<IUnitOfWork>();
        }

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
            _unitOfWork.SchedulePersistence.Create(schedule);
            await _unitOfWork.Commit();

            var currentSchedule = await _unitOfWork.SchedulePersistence.Get(x => x.Id == schedule.Id);

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
