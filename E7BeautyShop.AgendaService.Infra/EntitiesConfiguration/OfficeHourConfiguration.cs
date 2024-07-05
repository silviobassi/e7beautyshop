using E7BeautyShop.AgendaService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E7BeautyShop.AgendaService.Infra.EntitiesConfiguration;

public class OfficeHourConfiguration : IEntityTypeConfiguration<OfficeHour>
{
    public void Configure(EntityTypeBuilder<OfficeHour> builder)
    {
        builder.HasKey(oh => oh.Id);
        builder.Property(oh => oh.DateAndHour).IsRequired();
        builder.Property(oh => oh.IsAvailable).IsRequired();
        
        builder.OwnsOne(oh => oh.CustomerId, ci =>
        {
            ci.Property(i => i.Value).HasColumnName("Customer_Id").IsRequired(false);
        });

        builder.HasOne(oh => oh.Catalog)
            .WithMany()
            .HasForeignKey(oh => oh.CatalogId);
    }
}