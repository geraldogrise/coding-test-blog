using Blog.Data.Core.Extensions;
using Blog.Domain.Aggreagates.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Data.Core
{
    public class MappingCore<T> : EntityTypeConfiguration<T> where T : EntityCore<T>
    {
        public override void Map(EntityTypeBuilder<T> builder)
        {
            builder.Ignore(x => x.CascadeMode);
            builder.Ignore(x => x.ClassLevelCascadeMode);
            builder.Ignore(x => x.RuleLevelCascadeMode);
            builder.Ignore(x => x.ValidationResult);
    

        }
    }
}
