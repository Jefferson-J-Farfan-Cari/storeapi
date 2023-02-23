using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using StoreAPI.Entities.Models;

namespace StoreAPI.Infrastructure.Configuration;

public class ProductConfig  : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        
        builder.HasKey(e => e.IdProduct)
            .HasName("Product_pk");

        builder.Property(e => e.IdProduct);

        builder.Property(e => e.Code)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(50);
        
        builder.Property(e => e.Description)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.Price)
            .IsRequired()
            .HasColumnType("numeric");

        builder.Property(e => e.Stock)
            .IsRequired()
            .HasColumnType("numeric");
        
        builder.Property(e => e.Brand)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(e => e.DueDate).HasColumnType("timestamp");
        
        builder.Property(e => e.LogDateCrate).HasColumnType("timestamp");

        builder.Property(e => e.LogDateModified).HasColumnType("timestamp");
        
        builder.HasOne(d => d.Category)
            .WithMany(p => p.Product)
            .HasForeignKey(d => d.IdCategory)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("Product_IdCategory_fk");
        
        builder.HasOne(d => d.Store)
            .WithMany(p => p.Product)
            .HasForeignKey(d => d.IdStore)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("Product_IdStore_fk");

    }
}