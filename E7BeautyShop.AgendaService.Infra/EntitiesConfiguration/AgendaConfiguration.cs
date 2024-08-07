﻿using E7BeautyShop.AgendaService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E7BeautyShop.AgendaService.Infra.EntitiesConfiguration;

public class AgendaConfiguration : IEntityTypeConfiguration<Agenda>
{
    public void Configure(EntityTypeBuilder<Agenda> builder)
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
            .HasForeignKey(dr => dr.AgendaId)
            .IsRequired();

        builder.HasMany(s => s.OfficeHours)
            .WithOne()
            .HasForeignKey(oh => oh.AgendaId)
            .IsRequired();
    }
}