using E7BeautyShop.Appointment.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E7BeautyShop.Appointment.Infra.EntitiesConfiguration;

public class ScheduleConfiguration
{
    public void Configure(EntityTypeBuilder<Schedule> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(s => s.StartAt).IsRequired();
        builder.Property(s => s.EndAt).IsRequired();

        builder.OwnsOne(s => s.Weekday, wd =>
        {
            wd.Property(w => w.StartAt).HasColumnName("WeekdayStartAt").IsRequired();
            wd.Property(w => w.EndAt).HasColumnName("WeekdayEndAt").IsRequired();
        });

        builder.OwnsOne(s => s.Weekend, we =>
        {
            we.Property(w => w.StartAt).HasColumnName("WeekendStartAt").IsRequired();
            we.Property(w => w.EndAt).HasColumnName("WeekendEndAt").IsRequired();
        });
        
        builder.OwnsOne(s => s.Professional, pi =>
        {
            pi.Property(p => p.Id).HasColumnName("ProfessionalId").IsRequired();
        });

        builder.HasMany(s => s.OfficeHours)
            .WithOne()
            .HasForeignKey(oh => oh.ScheduleId);

        builder.HasMany(s => s.DaysRest)
            .WithOne()
            .HasForeignKey(dr => dr.ScheduleId);
    }
}