using E7BeautyShop.AgendaService.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace E7BeautyShop.AgendaService.Infra.Context;

public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Catalog> Catalogs { get; set; }
    public DbSet<OfficeHour> OfficeHours { get; set; }
    public DbSet<Agenda> Schedules { get; set; }
    public DbSet<DayRest> DaysRest { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}