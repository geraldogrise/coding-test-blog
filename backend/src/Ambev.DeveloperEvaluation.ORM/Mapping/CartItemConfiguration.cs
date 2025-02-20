using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.ToTable("cartitems");
            builder.HasKey(ci => ci.Id);
            builder.Property(ci => ci.Id).HasColumnName("id").HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");
            builder.Property(ci => ci.ProductId).HasColumnName("productid");
            builder.Property(ci => ci.CartId).HasColumnName("cartid");
            builder.HasOne(ci => ci.Cart).WithMany(c => c.Items).HasForeignKey(ci => ci.CartId);
            builder.HasOne(ci => ci.Product).WithMany().HasForeignKey(ci => ci.ProductId);
            builder.Property(ci => ci.Quantity).HasColumnName("quantity").IsRequired();
        }
    }
}
