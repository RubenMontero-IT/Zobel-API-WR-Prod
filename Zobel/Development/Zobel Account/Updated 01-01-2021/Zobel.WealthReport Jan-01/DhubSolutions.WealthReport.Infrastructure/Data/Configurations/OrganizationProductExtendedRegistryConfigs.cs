using DhubSolutions.WealthReport.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DhubSolutions.WealthReport.Infrastructure.Data.Configurations
{
    public class OrganizationProductExtendedRegistryConfigs : IEntityTypeConfiguration<OrganizationProductExtendedRegistry>
    {
        public void Configure(EntityTypeBuilder<OrganizationProductExtendedRegistry> builder)
        {
            builder.ToTable("OrganizationProductExtendedRegistry", "invpder");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
            .HasColumnName("OrgProductExtendedRegistryID")
            .HasMaxLength(100);

            builder.HasIndex(e => new { e.ProductID, e.OrganizationID, e.Date, e.MainCurrencyId })
                .HasName("IX_OrganizationProductExtendedRegistry")
                .IsUnique();

            builder.Property(e => e.BaseCurrencyId)
                .HasColumnName("BaseCurrency")
                .HasColumnType("char(3)")
                .IsUnicode(false);

            builder.Property(e => e.BloombergID)
                .HasMaxLength(100);

            builder.Property(e => e.CUSIP)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Date).HasColumnType("date");

            builder.Property(e => e.ISIN)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.ITDPL)
                .HasMaxLength(10);

            builder.Property(e => e.ITDPLFX)
                .HasMaxLength(10);

            builder.Property(e => e.ITDRoR)
                .HasMaxLength(10);

            builder.Property(e => e.ITDVol)
                .HasMaxLength(10);

            builder.Property(e => e.MainCurrencyId)
                .HasColumnName("MainCurrency")
                .HasColumnType("char(3)")
                .IsRequired();

            builder.Property(e => e.OrganizationID)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.ProductID)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.SEDOL)
                .HasMaxLength(100);

            builder.Property(e => e.Ticker)
                .HasMaxLength(100);

            builder.Property(e => e.VAMI)
                .HasMaxLength(10);

            builder.Property(e => e.Vol)
                .HasMaxLength(10);

            builder.HasOne(d => d.BaseCurrency)
                .WithMany(p => p.OrganizationProductExtendedRegistryBaseCurrencies)
                .HasForeignKey(d => d.BaseCurrencyId)
                .HasConstraintName("FK_OrganizationProductExtendedRegistry_Currency");

            builder.HasOne(d => d.MainCurrency)
                .WithMany(p => p.OrganizationProductExtendedRegistryMainCurrencies)
                .HasForeignKey(d => d.MainCurrencyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrganizationProductExtendedRegistry_Currency1");

            builder.HasOne(d => d.OrganizationProductRegistry)
                .WithMany(p => p.OrganizationProductExtendedRegistries)
                .HasPrincipalKey(p => new { p.ProductID, p.OrganizationID, p.Date })
                .HasForeignKey(d => new { d.ProductID, d.OrganizationID, d.Date })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrganizationProductExtendedRegistry_OrganizationProductRegistry");
        }
    }
}
