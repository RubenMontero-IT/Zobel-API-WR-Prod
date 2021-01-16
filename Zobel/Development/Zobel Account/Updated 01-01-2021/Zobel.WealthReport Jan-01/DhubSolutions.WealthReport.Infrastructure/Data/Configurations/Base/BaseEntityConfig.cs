using DhubSolutions.Core.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DhubSolutions.WealthReport.Infrastructure.Data.Configurations.Base
{
    public class BaseEntityConfig<T> : IEntityTypeConfiguration<T> where T : class, IEntity, new()
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(e => e.Id)
                .IsRequired()
                .HasColumnName("ID")
                .HasMaxLength(100);
        }
    }
}
