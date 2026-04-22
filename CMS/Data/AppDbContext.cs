using CMS.Models;
using Microsoft.EntityFrameworkCore;

namespace CMS.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<ParkingZona> ParkingZone { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("postgis");

        modelBuilder.Entity<ParkingZona>()
            .Property(p => p.Geometrija)
            .HasColumnType("geometry(geometry,4326)");

        base.OnModelCreating(modelBuilder);
    }
}
