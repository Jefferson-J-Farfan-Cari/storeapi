using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using StoreAPI.Entities.Models;

namespace StoreAPI.Infrastructure.Configuration;

public class CategoryConfig : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        
        builder.HasKey(e => e.IdCategory)
            .HasName("Category_pk");

        builder.Property(e => e.IdCategory);

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(50);
        
        builder.Property(e => e.Description)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.LogDateCrate).HasColumnType("timestamp");

        builder.Property(e => e.LogDateModified).HasColumnType("timestamp");

    }
}