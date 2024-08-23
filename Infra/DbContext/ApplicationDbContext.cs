using Domain.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Infra.DbContext;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : Microsoft.EntityFrameworkCore.DbContext(options: options)
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Product?> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}