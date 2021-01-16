using DhubSolutions.WealthReport.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DhubSolutions.WealthReport.Infrastructure.Data.Configurations
{
    public class CapitalTransactionTypeConfig : IEntityTypeConfiguration<CapitalTransactionType>
    {
        public void Configure(EntityTypeBuilder<CapitalTransactionType> builder)
        {
            builder.ToTable("CapitalTransactionType", "market");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                 .HasColumnName("CapitalTransactionTypeID")
                 .HasMaxLength(100);

            builder.Property(e => e.CapitalTransactionTypeName)
                 .IsRequired()
                 .HasMaxLength(100);
        }
    }
}