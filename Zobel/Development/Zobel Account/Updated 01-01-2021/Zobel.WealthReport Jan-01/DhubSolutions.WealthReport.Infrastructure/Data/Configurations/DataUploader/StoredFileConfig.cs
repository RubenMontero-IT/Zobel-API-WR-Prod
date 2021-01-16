using DhubSolutions.Reports.Domain.Entities.DataUploader;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DhubSolutions.WealthReport.Infrastructure.Data.Configurations.DataUploader
{
    public class StoredFileConfig : IEntityTypeConfiguration<StoredFile>
    {
        public void Configure(EntityTypeBuilder<StoredFile> builder)
        {
            builder.ToTable("StoredFile", "dbi");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
             .HasColumnName("StoredFileID")
             .HasMaxLength(100);


            builder.Property(e => e.FileName)
                .HasColumnName("FileName")
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(e => e.Description)
                .HasColumnName("Description")
                .HasMaxLength(250);

            builder.Property(e => e.Period)
                .HasColumnName("PeriodID")
                .HasMaxLength(7)
                .IsRequired();

            builder.Property(e => e.FileExtensionId)
                .HasColumnName("FileExtensionID")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(e => e.OrganizationId)
                .HasColumnName("OrganizationID")
                .IsRequired();

            builder.Property(e => e.UploadedById)
                .HasColumnName("UploadedByID")
                .IsRequired();

            builder.HasOne(d => d.UploadedBy)
                .WithMany()
                .HasForeignKey(d => d.UploadedById)
                .HasConstraintName("FK_StoredFiles_UploadedBy");

            builder.HasOne(d => d.organization)
                .WithMany()
                .HasForeignKey(d => d.OrganizationId)
                .HasConstraintName("FK_StoredFiles_Organization");

            builder.HasOne(d => d.FileExtension)
                .WithMany()
                .HasForeignKey(d => d.FileExtensionId)
                .HasConstraintName("FK_StoredFiles_FileExtension");
        }
    }

}
