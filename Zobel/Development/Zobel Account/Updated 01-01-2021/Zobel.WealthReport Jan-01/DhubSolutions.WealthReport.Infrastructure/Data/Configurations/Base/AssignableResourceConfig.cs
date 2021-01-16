using DhubSolutions.Common.Domain.Entities.Base;
using DhubSolutions.Core.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DhubSolutions.WealthReport.Infrastructure.Data.Configurations.Base
{
    public class AssignableResourceConfig<T> : BaseEntityConfig<T>, IEntityTypeConfiguration<T>
        where T : class, IAssignableResource, IEntity, new()
    {
        public override void Configure(EntityTypeBuilder<T> builder)
        {
            base.Configure(builder);

            builder.Property(e => e.OrganizationRoleId)
                .IsRequired()
                .HasColumnName("OrganizationRoleID")
                .HasMaxLength(100);




        }
    }
}
