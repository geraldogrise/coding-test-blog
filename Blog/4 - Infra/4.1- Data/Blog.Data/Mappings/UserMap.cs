using Azure;
using Blog.Data.Core;
using Blog.Domain.Aggreagates.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Data.Mappings
{
    public class UserMap : MappingCore<User>
    {
        public override void Map(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("TBL_USER");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName(@"ID_USER").IsRequired().HasColumnType("int");
            builder.Property(x => x.Name).HasColumnName(@"TXT_NAME").IsRequired().IsUnicode(false).HasColumnType("varchar").HasMaxLength(50);
            builder.Property(x => x.Email).HasColumnName(@"TXT_EMAIL").IsRequired().IsUnicode(false).HasColumnType("varchar").HasMaxLength(50);
            builder.Property(x => x.Login).HasColumnName(@"TXT_LOGIN").IsRequired().IsUnicode(false).HasColumnType("varchar").HasMaxLength(20);
            builder.Property(x => x.Password).HasColumnName(@"TXT_PASSWORD").IsRequired().IsUnicode(false).HasColumnType("varchar").HasMaxLength(20);
            base.Map(builder);
        }
    }
}
