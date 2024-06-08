using E7BeautyShop.Appointment.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E7BeautyShop.Appointment.Infra.EntitiesConfiguration;

public class CatalogConfiguration : IEntityTypeConfiguration<Catalog>
{

    public void Configure(EntityTypeBuilder<Catalog> builder)
    {
        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.DescriptionName)
            .HasColumnName("DescriptionName")
            .IsRequired();
        builder.Property(c => c.DescriptionPrice)
            .HasColumnName("DescriptionPrice")
            .IsRequired();
    }
}