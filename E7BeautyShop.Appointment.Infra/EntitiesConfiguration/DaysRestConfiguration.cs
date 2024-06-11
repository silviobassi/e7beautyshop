using E7BeautyShop.Appointment.Core;
using E7BeautyShop.Appointment.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E7BeautyShop.Appointment.Infra.EntitiesConfiguration;

public class DaysRestConfiguration: IEntityTypeConfiguration<DayRest>
{
    public void Configure(EntityTypeBuilder<DayRest> builder)
    {
        builder.HasKey(dr => dr.Id);
        builder.Property(dr => dr.DayOnWeek).IsRequired();
    }
}