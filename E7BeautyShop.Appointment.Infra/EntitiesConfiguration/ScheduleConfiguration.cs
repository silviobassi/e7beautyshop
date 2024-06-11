using E7BeautyShop.Appointment.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E7BeautyShop.Appointment.Infra.EntitiesConfiguration;

public class ScheduleConfiguration : IEntityTypeConfiguration<Schedule>
{
    public void Configure(EntityTypeBuilder<Schedule> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(s => s.StartAt).IsRequired();
        builder.Property(s => s.EndAt).IsRequired();

        builder.OwnsOne(s => s.Weekday, wd =>
        {
            wd.Property(w => w.StartAt).HasColumnName("Weekday_StartAt").IsRequired();
            wd.Property(w => w.EndAt).HasColumnName("Weekday_EndAt").IsRequired();
        });

        builder.OwnsOne(s => s.Weekend, we =>
        {
            we.Property(w => w.StartAt).HasColumnName("Weekend_StartAt").IsRequired();
            we.Property(w => w.EndAt).HasColumnName("Weekend_EndAt").IsRequired();
        });

        builder.OwnsOne(s => s.ProfessionalId, p =>
        {
            p.Property(pr => pr.Value).HasColumnName("Professional_Id").IsRequired();
        });
        

        builder.HasMany(s => s.DaysRest)
            .WithOne()
            .HasForeignKey(dr => dr.ScheduleId)
            .IsRequired();

        builder.HasMany(s => s.OfficeHours)
            .WithOne()
            .HasForeignKey(oh => oh.ScheduleId)
            .IsRequired();
    }
}