using DhubSolutions.WealthReport.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DhubSolutions.WealthReport.Infrastructure.Data.Configurations
{
    public class TransactionTypeConfig : IEntityTypeConfiguration<TransactionType>
    {
        public void Configure(EntityTypeBuilder<TransactionType> builder)
        {
            builder.ToTable("TransactionType", "market");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                 .HasColumnName("TransactionTypeID")
                 .HasMaxLength(100);

            builder.Property(e => e.TransactionTypeName)
                 .IsRequired()
                 .HasMaxLength(100);
        }
    }
}
