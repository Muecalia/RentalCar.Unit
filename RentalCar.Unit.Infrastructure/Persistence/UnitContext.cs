using Microsoft.EntityFrameworkCore;
using RentalCar.Unit.Core.Entities;

namespace RentalCar.Unit.Infrastructure.Persistence;

public class UnitContext : DbContext
{
    public UnitContext (DbContextOptions<UnitContext> options) : base(options) { }
    
    public DbSet<Units> Units { get; set; } 
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Units>(e =>
        {
            e.HasKey(u => u.Id);

            e.Property<string>(u => u.Name)
                .IsRequired()
                .HasMaxLength(50);
            
            e.Property<string>(u => u.Address)
                .IsRequired()
                .HasMaxLength(100);
            
            e.Property<string>(u => u.Phone)
                .HasMaxLength(25);
            
            e.HasIndex(c => c.Name)
                .IsUnique();
        });
    }
}