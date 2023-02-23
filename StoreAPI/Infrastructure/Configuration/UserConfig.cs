using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using StoreAPI.Entities.Models;

namespace StoreAPI.Infrastructure.Configuration;

public class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        
        builder.HasKey(e => e.IdUser)
            .HasName("User_pk");

        builder.Property(e => e.IdUser);

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(50);
        
        builder.Property(e => e.FatherLN)
            .IsRequired()
            .HasMaxLength(50);
        
        builder.Property(e => e.MotherLN)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.DocumentType)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(e => e.Document)
            .IsRequired()
            .HasMaxLength(8);

        builder.Property(e => e.Email)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.Password)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.LogDateCrate).HasColumnType("timestamp");

        builder.Property(e => e.LogDateModified).HasColumnType("timestamp");

    }
}