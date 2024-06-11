using E7BeautyShop.Appointment.Core;
using E7BeautyShop.Appointment.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace E7BeautyShop.Appointment.Infra.Context;

public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Catalog> Catalogs { get; set; }
    public DbSet<OfficeHour> OfficeHours { get; set; }
    public DbSet<Schedule> Schedules { get; set; }
    public DbSet<DayRest> DaysRest { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}