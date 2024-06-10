using E7BeautyShop.Appointment.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E7BeautyShop.Appointment.Infra.EntitiesConfiguration;

public class CatalogConfiguration : IEntityTypeConfiguration<Catalog>
{
    public void Configure(EntityTypeBuilder<Catalog> builder)
    {
        builder.HasKey(c => c.Id);

        builder.OwnsOne(c => c.ServiceDescription)
            .Property(sd => sd.Name)
            .HasColumnName("Description_Name")
            .IsRequired();

        builder.OwnsOne(c => c.ServiceDescription)
            .Property(sd => sd.Price)
            .HasColumnName("Description_Price")
            .IsRequired();
    }
}