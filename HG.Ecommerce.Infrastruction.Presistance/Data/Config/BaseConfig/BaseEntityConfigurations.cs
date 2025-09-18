using HG.Ecommerce.Core.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace CoursesManagmentSystem.Infrastructure.Presistance.Data.Config.BaseConfig
{
    class BaseEntityConfigurations<TEntity, TKey> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity<TKey> where TKey : IEquatable<TKey>
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd();
        }
    }
}
