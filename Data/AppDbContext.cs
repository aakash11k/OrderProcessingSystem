using Microsoft.EntityFrameworkCore;
using OrderProcessingSystem.Models;

namespace OrderProcessingSystem.Data;

public class AppDbContext : DbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>()
            .ToTable(tb => tb.HasTrigger("TR_Order_Insert"));
    }
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Order> Orders { get; set; }

    public DbSet<AuditLog> AuditLogs { get; set; }

    public DbSet<WebhookLog> WebhookLogs { get; set; }
}



