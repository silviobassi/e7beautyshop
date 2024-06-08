using E7BeautyShop.Appointment.Core;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E7BeautyShop.Appointment.Infra.EntitiesConfiguration;

public class DayRestConfiguration
{
    public void Configure(EntityTypeBuilder<DayRest> builder)
    {
        builder.HasKey(dr => dr.Id);
        builder.Property(dr => dr.DayOnWeek).IsRequired();
        builder.Property(dr => dr.ScheduleId).IsRequired();
    }
}