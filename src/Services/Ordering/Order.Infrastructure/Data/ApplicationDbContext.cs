using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Data;
using Ordering.Domain.Models;

namespace Order.Infrastructure.Data;

public class ApplicationDbContext:DbContext,IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
    {
    }

    public DbSet<Customer> Customers => Set<Customer>();

    public DbSet<Product> Products => Set<Product>();

    public DbSet<Ordering.Domain.Models.Order> Orders => Set<Ordering.Domain.Models.Order>();

    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);

    }
}
