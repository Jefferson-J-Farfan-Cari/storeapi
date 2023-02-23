using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using StoreAPI.Entities.Models;

namespace StoreAPI.Infrastructure.Configuration;

public class StoreConfig : IEntityTypeConfiguration<Store>
{
    public void Configure(EntityTypeBuilder<Store> builder)
    {
        builder.HasKey(e => e.IdStore)
            .HasName("Store_pk");

        builder.Property(e => e.IdStore);

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(50);
        
        builder.Property(e => e.Address)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(e => e.Latitude)
            .IsRequired()
            .HasColumnType("numeric");

        builder.Property(e => e.Longitude)
            .IsRequired()
            .HasColumnType("numeric");
        
        builder.Property(e => e.LogDateCrate).HasColumnType("timestamp");

        builder.Property(e => e.LogDateModified).HasColumnType("timestamp");

    }
}