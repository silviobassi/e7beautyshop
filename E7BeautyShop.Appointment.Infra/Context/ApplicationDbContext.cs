using E7BeautyShop.Appointment.Core;
using Microsoft.EntityFrameworkCore;

namespace E7BeautyShop.Appointment.Infra.Context;

public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Catalog> Catalogs { get; init; }
    public DbSet<OfficeHour> OfficeHours { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}