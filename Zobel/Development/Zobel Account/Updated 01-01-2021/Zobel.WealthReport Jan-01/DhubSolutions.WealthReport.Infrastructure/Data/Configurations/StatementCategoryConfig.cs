using DhubSolutions.WealthReport.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DhubSolutions.WealthReport.Infrastructure.Data.Configurations
{
    public class StatementCategoryConfig : IEntityTypeConfiguration<StatementCategory>
    {
        public void Configure(EntityTypeBuilder<StatementCategory> builder)
        {
            builder.HasKey(e => e.Id);

            builder.ToTable("StatementCategory", "wrp");

            builder.Property(e => e.Id)
                .HasColumnName("StatementCategoryID")
                .HasMaxLength(100)
                .ValueGeneratedNever();

            builder.Property(e => e.StatementCategoryName)
                .HasColumnName("StatementCategoryName")
                .HasMaxLength(100);

            builder.Property(e => e.Description)
                .HasColumnName("Description")
                .HasMaxLength(100);
        }
    }
}
