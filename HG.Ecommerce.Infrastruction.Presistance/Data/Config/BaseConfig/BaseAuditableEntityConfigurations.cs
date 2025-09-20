using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using HG.Ecommerce.Core.Common;

namespace HG.Ecommerce.Infrastruction.Presistance.Data.Config.BaseConfig
{
    public class BaseAuditableEntityConfigurations<TEnitity, TKey> : IEntityTypeConfiguration<TEnitity>
 where TEnitity : BaseAduitableEntity<TKey>
 where TKey : IEquatable<TKey>
    {
        public void Configure(EntityTypeBuilder<TEnitity> builder)
        {
            builder.Property(e => EF.Property<string>(e, "CreatedBy"))
                .HasMaxLength(50)
                .IsRequired(false);
            builder.Property(e => EF.Property<string>(e, "UpdatedBy"))
                .HasMaxLength(50)
                .IsRequired(false);
        }
    }
}
