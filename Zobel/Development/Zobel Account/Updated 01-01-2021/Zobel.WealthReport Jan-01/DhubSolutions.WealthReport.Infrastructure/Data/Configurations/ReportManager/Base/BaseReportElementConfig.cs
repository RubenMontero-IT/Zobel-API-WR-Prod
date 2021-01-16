using DhubSolutions.Core.Domain.Entity;
using DhubSolutions.Reports.Domain.Entities.ReportManager.Base;
using DhubSolutions.WealthReport.Infrastructure.Data.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DhubSolutions.WealthReport.Infrastructure.Data.Configurations.ReportManager.Base
{
    public class BaseReportElementConfig<T> : BaseEntityConfig<T>
        where T : class, IBaseReportElement, IEntity, new()
    {
        public override void Configure(EntityTypeBuilder<T> builder)
        {
            base.Configure(builder);

            builder.Property(e => e.Name)
                .HasMaxLength(100);

            builder.Property(e => e.Code)
                .HasMaxLength(50);

            builder.Property(e => e.DataIndex);

            builder.Property(e => e.CreatedById)
                .HasMaxLength(100);

            builder.Property(e => e.CreationDate)
              .HasColumnType("date");

            builder.Property(e => e.LastModified)
                .HasColumnType("date");

            builder.Property(e => e.Config);

            builder.Property(e => e.Description)
                .HasMaxLength(250);

        }
    }
}
