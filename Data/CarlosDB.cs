using Carlos_Pizza.Models;
using Microsoft.EntityFrameworkCore;

namespace Carlos_Pizza.Data;

public class CarlosDB : DbContext
{
    public CarlosDB(DbContextOptions<CarlosDB> options)
        : base(options)
    {
    }

    //public DbSet<MenuItem> MenuItems { get; set; }
    public DbSet<Carlos_Pizza.Models.MenuItem> MenuItems { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MenuItem>().ToTable("MenuItem");
    }
}