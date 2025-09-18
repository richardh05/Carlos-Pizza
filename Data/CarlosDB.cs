using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Carlos_Pizza.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Carlos_Pizza.Data;

public class CheckoutCustomer
{
    [Key]
    [StringLength((50))]
    public string Email { get; set; }
    [StringLength((50))]
    public string Name { get; set; }
    public int BasketId { get; set; }
}

public class Basket
{
    [Key]
    public int BasketId { get; set; }
}

public class BasketItem
{
    [Required]
    public int StockId { get; set; }
    [Required]
    public int BasketId {get; set;}
    [Required]
    public int Quantity { get; set; }
}

public class CarlosDB : IdentityDbContext
{
    public CarlosDB(DbContextOptions<CarlosDB> options)
        : base(options) { }
    

    //public DbSet<MenuItem> MenuItems { get; set; }
    public DbSet<Carlos_Pizza.Models.MenuItem> MenuItems { get; set; } = default!;
    public DbSet<CheckoutCustomer> CheckoutCustomers { get; set; } = default!;
    public DbSet<Basket> Baskets { get; set; } = default!;
    public DbSet<BasketItem> BasketItems { get; set; } = default!;
    public DbSet<OrderHistory> OrderHistories { get; set; } = default!;
    public DbSet<OrderItems> OrderItems { get; set; } = default!;
    
    [NotMapped]
    public DbSet<CheckoutItem> CheckoutItems { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<BasketItem>().HasKey(t => new { t.StockId , t.BasketId });
        modelBuilder.Entity<CheckoutItem>()
            .Property(c => c.Price)
            .HasPrecision(18, 2);
}
}