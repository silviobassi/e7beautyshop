using E7BeautyShop.Appointment.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E7BeautyShop.Appointment.Infra.EntitiesConfiguration;

public class OfficeHourConfiguration : IEntityTypeConfiguration<OfficeHour>
{
    public void Configure(EntityTypeBuilder<OfficeHour> builder)
    {
        builder.HasKey(oh => oh.Id);
        builder.Property(oh => oh.DateAndHour).IsRequired();
        builder.Property(oh => oh.IsAvailable).IsRequired();
        
        builder.OwnsOne(oh => oh.CustomerId, ci =>
        {
            ci.Property(i => i.Value).HasColumnName("Customer_Value").IsRequired(false);
        });

        builder.HasOne(oh => oh.Catalog)
            .WithMany()
            .HasForeignKey(oh => oh.CatalogId)
            .IsRequired(false);
    }
}