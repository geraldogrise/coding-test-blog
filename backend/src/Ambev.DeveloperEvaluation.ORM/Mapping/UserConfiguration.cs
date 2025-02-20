using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.RegularExpressions;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).HasColumnName("id").HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

        builder.Property(u => u.Username).HasColumnName("username").IsRequired().HasMaxLength(50);
        builder.Property(u => u.Password).HasColumnName("password").IsRequired().HasMaxLength(100);
        builder.Property(u => u.Email).HasColumnName("email").IsRequired().HasMaxLength(100);
        builder.Property(u => u.Phone).HasColumnName("phone").HasMaxLength(20);

        builder.Property(u => u.Status).HasColumnName("status")
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(u => u.Role).HasColumnName("role")
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.OwnsOne(u => u.Name, name =>
        {
            name.Property(n => n.Firstname).HasColumnName("firstname").HasMaxLength(50);
            name.Property(n => n.Lastname).HasColumnName("lastname").HasMaxLength(50);
        });

        // Address (Embedded)
        builder.OwnsOne(u => u.Address, address =>
        {
            address.Property(a => a.City).HasColumnName("city").HasMaxLength(100);
            address.Property(a => a.Street).HasColumnName("street").HasMaxLength(100);
            address.Property(a => a.Number).HasColumnName("number");
            address.Property(a => a.Zipcode).HasColumnName("zipcode").HasMaxLength(20);

            address.OwnsOne(a => a.Geolocation, geo =>
            {
                geo.Property(g => g.Lat).HasColumnName("lat").HasMaxLength(20);
                geo.Property(g => g.Long).HasColumnName("long").HasMaxLength(20);
            });
        });

    }
}
