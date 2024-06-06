using E7BeautyShop.Appointment.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E7BeautyShop.Appointment.Infra.EntitiesConfiguration;

public class CatalogConfiguration : IEntityTypeConfiguration<Catalog>
{

    public void Configure(EntityTypeBuilder<Catalog> builder)
    {
        builder.HasKey(c => c.Id);

        builder.OwnsOne(sd => sd.ServiceDescription, cb =>
        {
            cb.Property(sd => sd.Name).HasColumnName("DescriptionName").IsRequired();
            cb.Property(sd => sd.Price).HasColumnName("DescriptionPrice").IsRequired();
        });
    }
}