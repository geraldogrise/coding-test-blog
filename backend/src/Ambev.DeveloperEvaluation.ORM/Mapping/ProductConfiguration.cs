using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("products");

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnName("id")
                   .HasColumnType("uuid")
                   .HasDefaultValueSql("gen_random_uuid()");

            builder.Property(p => p.Title).HasColumnName("title")
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(p => p.Price).HasColumnName("price")
                   .IsRequired()
                   .HasColumnType("decimal(10,2)");

            builder.Property(p => p.Description).HasColumnName("description")
                   .HasMaxLength(500);

            builder.Property(p => p.Category).HasColumnName("category")
                   .HasMaxLength(50);

            builder.Property(p => p.Image).HasColumnName("image")
                   .HasMaxLength(255);

            builder.OwnsOne(p => p.Rating, rating =>
            {
                rating.Property(r => r.Rate).HasColumnName("rate")
                      .HasColumnType("double precision");

                rating.Property(r => r.Count).HasColumnName("count")
                      .HasColumnType("int");
            });
        }
    }
}
