using Azure;
using Blog.Data.Core;
using Blog.Domain.Aggreagates.Posts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Data.Mappings
{
    public class PostMap : MappingCore<Post>
    {
        public override void Map(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("TBL_POST");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName(@"ID_POST").IsRequired().HasColumnType("int");
            builder.Property(x => x.Name).HasColumnName(@"TXT_NAME").IsRequired().IsUnicode(false).HasColumnType("varchar").HasMaxLength(50);
            builder.Property(x => x.Description).HasColumnName(@"TXT_DESCRIPTION").IsRequired().IsUnicode(false).HasColumnType("varchar").HasMaxLength(400);

            builder.Property(x => x.Id_user).HasColumnName(@"ID_USER").IsRequired().HasColumnType("int");
            builder.HasOne(x => x.User).WithMany(y => y.PostList).HasForeignKey(x => x.Id_user);
            base.Map(builder);
        }
    }
}
