using E7BeautyShop.Appointment.Core;
using E7BeautyShop.Appointment.Core.Entities;
using E7BeautyShop.Appointment.Core.ObjectsValue;
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
            sd.Property(d => d.Name)
                .HasColumnName("Description_Name")
                .HasMaxLength(ServiceDescription.MaxNameLength)
                .IsRequired();
            
            sd.Property(d => d.Price)
                .HasColumnName("Description_Price")
                .HasColumnType("decimal(5,2)")
                .IsRequired();
        });

    }
}