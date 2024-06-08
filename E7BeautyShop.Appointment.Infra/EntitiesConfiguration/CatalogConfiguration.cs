using E7BeautyShop.Appointment.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E7BeautyShop.Appointment.Infra.EntitiesConfiguration;

public class CatalogConfiguration : IEntityTypeConfiguration<Catalog>
{

    public void Configure(EntityTypeBuilder<Catalog> builder)
    {
        builder.HasKey(c => c.Id);
        
        builder.OwnsOne(c => c.ServiceDescription, sd =>
        {
            sd.Property(s => s.Name).HasColumnName("DescriptionName").IsRequired();
            sd.Property(s => s.Price).HasColumnName("DescriptionPrice").IsRequired();
        });
    }
}