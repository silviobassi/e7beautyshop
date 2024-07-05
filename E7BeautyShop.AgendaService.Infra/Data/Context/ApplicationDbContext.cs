using E7BeautyShop.AgendaService.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace E7BeautyShop.AgendaService.Infra.Context;

public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Catalog> Catalogs { get; init; }
    public DbSet<OfficeHour> OfficeHours { get; init; }
    public DbSet<Agenda> Agendas { get; init;}
    public DbSet<DayRest> DaysRest { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}