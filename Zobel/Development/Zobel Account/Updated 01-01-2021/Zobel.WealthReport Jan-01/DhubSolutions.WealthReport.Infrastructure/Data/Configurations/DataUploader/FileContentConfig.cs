using DhubSolutions.Reports.Domain.Entities.DataUploader;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DhubSolutions.WealthReport.Infrastructure.Data.Configurations.DataUploader
{
    public class FileContentConfig : IEntityTypeConfiguration<FileContent>
    {
        public void Configure(EntityTypeBuilder<FileContent> builder)
        {
            builder.ToTable("FileContent", "dbi");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("FileContentID")
                .HasMaxLength(100);

            builder.Property(e => e.UploadDate)
                .HasColumnName("UploadDate")
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(e => e.Content)
                .HasColumnName("FileContent")
                .IsRequired();

            builder.Property(e => e.StoredFileId)
                .HasColumnName("StoredFileID")
                .IsRequired();

            builder.HasOne(d => d.StoredFile)
                .WithMany()
                .HasForeignKey(d => d.StoredFileId)
                .HasConstraintName("FK_FileContents_StoredFile");

        }
    }

}
