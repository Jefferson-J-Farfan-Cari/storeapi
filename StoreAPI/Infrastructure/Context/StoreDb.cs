using StoreAPI.Entities.Models;
using Microsoft.EntityFrameworkCore;
using StoreAPI.Infrastructure.Configuration;

namespace StoreAPI.Infrastructure.Context;

public class StoreDb : DbContext
{
    
    public StoreDb()
    {
    }
    
    public StoreDb(DbContextOptions<StoreDb> options) : base(options)
    {
        
    }
    
    public virtual DbSet<Category> Category { get; set; }
    public virtual DbSet<Product> Product { get; set; }
    public virtual DbSet<Store> Store { get; set; }
    public virtual DbSet<User> User { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql(
                "Host=containers-us-west-126.railway.app;Port=6204;Database=store;User Id=postgres;Password=j7lZ0VNnZgu29EmvHTE2"
                );
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");
        
        modelBuilder.ApplyConfiguration(new CategoryConfig());
        modelBuilder.ApplyConfiguration(new ProductConfig());
        modelBuilder.ApplyConfiguration(new StoreConfig());
        modelBuilder.ApplyConfiguration(new UserConfig());
        
    }
}