using DhubSolutions.WealthReport.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DhubSolutions.WealthReport.Infrastructure.Data.Configurations
{
    class SignableStatementConfig : IEntityTypeConfiguration<SignableStatement>
    {
        public void Configure(EntityTypeBuilder<SignableStatement> builder)
        {
            builder.HasKey(e => e.Id);

            builder.ToTable("SignableStatement", "wrp");

            builder.Property(e => e.Id)
                .HasColumnName("SignableStatementID")
                .HasMaxLength(100)
                .ValueGeneratedNever();

            builder.Property(e => e.StatementCategoryId)
                .HasColumnName("StatementCategoryID")
                .HasMaxLength(100);

            builder.Property(e => e.OrganizationId)
                .HasColumnName("OrganizationID")
                .HasMaxLength(100);

            builder.Property(e => e.ReportTemplateId)
                .HasColumnName("TemplateID")
                .HasMaxLength(100);

            builder.HasOne(d => d.StatementCategory)
                .WithMany()
                .HasForeignKey(d => d.StatementCategoryId)
                .HasConstraintName("FK_SignableStatement_StatementCategory")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(d => d.Organization)
                .WithMany()
                .HasForeignKey(e => e.OrganizationId)
                .HasConstraintName("FK_SignableStatement_Organization")
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(d => d.ReportTemplate)
                .WithMany()
                .HasForeignKey(e => e.ReportTemplateId)
                .HasConstraintName("FK_SignableStatement_ReportTemplate")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
