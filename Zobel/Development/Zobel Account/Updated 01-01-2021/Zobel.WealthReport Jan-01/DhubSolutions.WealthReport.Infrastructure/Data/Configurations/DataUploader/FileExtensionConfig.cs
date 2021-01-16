using DhubSolutions.Reports.Domain.Entities.DataUploader;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DhubSolutions.WealthReport.Infrastructure.Data.Configurations.DataUploader
{
    public class FileExtensionConfig : IEntityTypeConfiguration<FileExtension>
    {
        public void Configure(EntityTypeBuilder<FileExtension> builder)
        {
            builder.ToTable("FileExtension", "dbi");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
               .HasColumnName("FileExtensionID")
               .HasMaxLength(100);

            builder.Property(e => e.FileExtensionValue)
                .HasColumnName("FileExtensionValue")
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(e => e.FileExtensionIcon)
                .HasColumnName("FileExtensionIcon");



        }
    }

}
