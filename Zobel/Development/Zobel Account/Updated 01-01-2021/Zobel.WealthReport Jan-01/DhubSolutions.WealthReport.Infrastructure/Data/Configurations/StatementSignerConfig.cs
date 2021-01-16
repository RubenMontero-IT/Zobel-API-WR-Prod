using DhubSolutions.WealthReport.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DhubSolutions.WealthReport.Infrastructure.Data.Configurations
{
    public class StatementSignerConfig : IEntityTypeConfiguration<StatementSigner>
    {
        public void Configure(EntityTypeBuilder<StatementSigner> builder)
        {
            builder.HasKey(e => e.Id);

            builder.ToTable("StatementSigner", "wrp");

            builder.Property(e => e.Id)
                .HasColumnName("StatementSignerID")
                .HasMaxLength(100)
                .ValueGeneratedNever();

            builder.Property(e => e.SignedById)
                .HasColumnName("SignedBy")
                .HasMaxLength(100);

            builder.Property(e => e.SignedDate)
                .HasColumnName("SignedDate")
                .HasColumnType("datetime");

            builder.HasOne(d => d.SignedBy)
                .WithMany()
                .HasForeignKey(e => e.SignedById)
                .HasConstraintName("FK_StatementSigner_User")
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(d => d.SignableStatement)
                .WithMany(p => p.StatementSigners)
                .HasForeignKey(d => d.SignableStatementId)
                .HasConstraintName("FK_StatementSigner_SignableStatement")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
