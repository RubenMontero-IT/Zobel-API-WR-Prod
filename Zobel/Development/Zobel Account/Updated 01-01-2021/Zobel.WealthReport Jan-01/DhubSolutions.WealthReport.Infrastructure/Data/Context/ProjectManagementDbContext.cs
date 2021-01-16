using DhubSolutions.Common.Domain.Entities.Admin;
using DhubSolutions.Common.Domain.Entities.Application;
using DhubSolutions.WealthReport.Infrastructure.Data.Configurations;
using DhubSolutions.WealthReport.Infrastructure.Data.Configurations.Admin;
using DhubSolutions.WealthReport.Infrastructure.Data.Configurations.DataUploader;
using DhubSolutions.WealthReport.Infrastructure.Data.Configurations.ReportManager;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DhubSolutions.WealthReport.Infrastructure.Data.Context
{
    public class ProjectManagementDbContext : IdentityDbContext<User>
    {
        public ProjectManagementDbContext(DbContextOptions<ProjectManagementDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            CustomConfig();

            void CustomConfig()
            {
                modelBuilder.ApplyConfiguration(new FileContentConfig());
                modelBuilder.ApplyConfiguration(new FileExtensionConfig());
                modelBuilder.ApplyConfiguration(new StoredFileConfig());

                modelBuilder.ApplyConfiguration(new ReportConfig());
                modelBuilder.ApplyConfiguration(new ReportElementConfig());
                modelBuilder.ApplyConfiguration(new ReportTemplateConfig());
                modelBuilder.ApplyConfiguration(new ReportTemplateElementConfig());

                modelBuilder.ApplyConfiguration(new ReportPermissionConfig());
                modelBuilder.ApplyConfiguration(new ReportTemplatePermissionConfig());
                modelBuilder.ApplyConfiguration(new ReportElementPermissionConfig());
                modelBuilder.ApplyConfiguration(new ReportTemplateElementPermissionConfig());

                modelBuilder.ApplyConfiguration(new ReportTemplateOrganizationsConfig());
                //modelBuilder.ApplyConfiguration(new WealthReportTemplateConfig());


                modelBuilder.ApplyConfiguration(new ReportTemplateActivePeriodConfig());

                modelBuilder.ApplyConfiguration(new SystemGroupConfig());
                modelBuilder.ApplyConfiguration(new SystemGroupMemberShipConfig());

                modelBuilder.ApplyConfiguration(new ConnectionByOrganizationConfig());
                modelBuilder.ApplyConfiguration(new TemplateSettingsConfig());

                modelBuilder.ApplyConfiguration(new StatementCategoryConfig());
                modelBuilder.ApplyConfiguration(new SignableStatementConfig());
                modelBuilder.ApplyConfiguration(new StatementSignerConfig());

                modelBuilder.Entity<AppSetting>(entity =>
                {
                    entity.HasKey(e => e.Id)
                        .HasName("PK_AppSettings");

                    entity.ToTable("AppSetting", "app");

                    entity.Property(e => e.Id)
                        .HasColumnName("SettingID")
                        .HasMaxLength(100)
                        .ValueGeneratedNever();

                    entity.Property(e => e.Appid)
                        .IsRequired()
                        .HasColumnName("APPID")
                        .HasMaxLength(100);

                    entity.Property(e => e.SettingName)
                        .IsRequired()
                        .HasMaxLength(50);

                    entity.HasOne(d => d.App)
                        .WithMany(p => p.AppSetting)
                        .HasForeignKey(d => d.Appid)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_AppSetting_Apps");
                });

                modelBuilder.Entity<AppSettingRoleVOrg>(entity =>
                {
                    entity.ToTable("AppSettingRoleVOrg", "app");

                    entity.HasIndex(e => new { e.Rvid, e.Orgid, e.SettingId })
                        .HasName("IX_AppSettingRoleVOrg")
                        .IsUnique();

                    entity.Property(e => e.Id)
                        .HasColumnName("AppSettingRoleVOrgID")
                        .HasMaxLength(100)
                        .ValueGeneratedNever();

                    entity.Property(e => e.Orgid)
                        .IsRequired()
                        .HasColumnName("ORGID")
                        .HasMaxLength(100);

                    entity.Property(e => e.Rvid)
                        .IsRequired()
                        .HasColumnName("RVID")
                        .HasMaxLength(100);

                    entity.Property(e => e.SettingId)
                        .IsRequired()
                        .HasColumnName("SettingID")
                        .HasMaxLength(100);

                    entity.Property(e => e.Value).IsRequired();

                    entity.HasOne(d => d.Setting)
                        .WithMany(p => p.AppSettingRoleVorg)
                        .HasForeignKey(d => d.SettingId)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_AppSettingRoleVOrg_AppSetting");

                    entity.HasOne(d => d.OrganizationRole)
                        .WithMany(p => p.AppSettingRoleVOrg)
                        .HasPrincipalKey(p => new { p.Rvid, p.OrganizationId })
                        .HasForeignKey(d => new { d.Rvid, d.Orgid })
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_AppSettingRoleVOrg_OrganizationRole");
                });

                modelBuilder.Entity<AppSettingsByUser>(entity =>
                {
                    entity.ToTable("AppSettingsByUser", "app");

                    entity.HasIndex(e => new { e.SettingId, e.Userid })
                        .HasName("IX_AppSettingsByUser")
                        .IsUnique();

                    entity.Property(e => e.Id)
                        .HasColumnName("AppSettingsByUserID")
                        .HasMaxLength(100)
                        .ValueGeneratedNever();

                    entity.Property(e => e.SettingId)
                        .IsRequired()
                        .HasColumnName("SettingID")
                        .HasMaxLength(100);

                    entity.Property(e => e.Userid)
                        .IsRequired()
                        .HasColumnName("USERID")
                        .HasMaxLength(100);

                    entity.Property(e => e.Value).IsRequired();

                    entity.HasOne(d => d.Setting)
                        .WithMany(p => p.AppSettingsByUser)
                        .HasForeignKey(d => d.SettingId)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_AppSettingsByUser_AppSetting");

                    entity.HasOne(d => d.User)
                        .WithMany(p => p.AppSettingsByUser)
                        .HasForeignKey(d => d.Userid)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_AppSettingsByUser_User");
                });

                modelBuilder.Entity<AppSettingsMV>(entity =>
                {
                    entity.HasKey(e => new { e.SettingId, e.Id });

                    entity.ToTable("AppSettingsMV", "app");

                    entity.Property(e => e.SettingId)
                        .HasColumnName("SettingID")
                        .HasMaxLength(100);

                    entity.Property(e => e.Id)
                        .HasColumnName("AppSettingsMVID")
                        .HasMaxLength(100);

                    entity.Property(e => e.Value)
                        .IsRequired()
                        .IsUnicode(false);

                    entity.HasOne(d => d.Setting)
                        .WithMany(p => p.AppSettingsMV)
                        .HasForeignKey(d => d.SettingId)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_AppSettingsMV_AppSetting");
                });

                modelBuilder.Entity<Apps>(entity =>
                {
                    entity.HasKey(e => e.Id);

                    entity.ToTable("Apps", "app");

                    entity.Property(e => e.Id)
                        .HasColumnName("APPID")
                        .HasMaxLength(100)
                        .ValueGeneratedNever();

                    entity.Property(e => e.AlternativeName).HasMaxLength(250);

                    entity.Property(e => e.AppCode)
                        .IsRequired()
                        .HasMaxLength(50);

                    entity.Property(e => e.AppLogo).HasMaxLength(250);

                    entity.Property(e => e.AppName)
                        .IsRequired()
                        .HasMaxLength(50);

                    entity.Property(e => e.AppUrl)
                        .HasColumnName("AppURL")
                        .HasMaxLength(250);

                    entity.Property(e => e.Description).HasMaxLength(250);
                });

                ///Revisar esta Configuracion
                modelBuilder.Entity<AppMaxPermission>(entity =>
                {
                    entity.HasKey(e => new { e.Id, e.Userid, e.MaxPermission });

                    entity.ToTable("AppMaxPermission", "app");

                    entity.Property(e => e.Id).HasColumnName("APPID");

                    entity.Property(e => e.Userid).HasColumnName("USERID");

                    entity.HasOne(d => d.App)
                        .WithMany(p => p.AppMaxPermission)
                        .HasForeignKey(d => d.Id)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_AppMaxPermission_Apps");

                    entity.HasOne(d => d.MaxPermissionNavigation)
                        .WithMany(p => p.AppMaxPermission)
                        .HasForeignKey(d => d.MaxPermission)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_AppMaxPermission_Permission");
                });

                modelBuilder.Entity<CommentedResource>(entity =>
                {
                    entity.ToTable("CommentedResource", "app");

                    entity.HasIndex(e => new { e.ResourceId, e.CommentId, e.FeatureId })
                        .HasName("IX_CommentedResource")
                        .IsUnique();

                    entity.Property(e => e.Id)
                        .HasColumnName("CommentedResourceID")
                        .HasMaxLength(100)
                        .ValueGeneratedNever();

                    entity.Property(e => e.CommentId)
                        .IsRequired()
                        .HasColumnName("CommentID")
                        .HasMaxLength(100);

                    entity.Property(e => e.Description).HasMaxLength(150);

                    entity.Property(e => e.FeatureId)
                        .HasColumnName("FeatureID")
                        .HasMaxLength(100);

                    entity.Property(e => e.ResourceId)
                        .IsRequired()
                        .HasColumnName("ResourceID")
                        .HasMaxLength(100);

                    //entity.HasOne(d => d.ProjectComment)
                    //    .WithMany(p => p.CommentedResources)
                    //    .HasForeignKey(d => d.CommentId)
                    //    .OnDelete(DeleteBehavior.ClientSetNull)
                    //    .HasConstraintName("FK_CommentedResource_Comment");

                    entity.HasOne(d => d.Feature)
                        .WithMany(p => p.CommentedResources)
                        .HasForeignKey(d => d.FeatureId)
                        .HasConstraintName("FK_CommentedResource_Feature");

                    entity.HasOne(d => d.Resource)
                        .WithMany(p => p.CommentedResources)
                        .HasForeignKey(d => d.ResourceId)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_CommentedResource_Resource");
                });

                modelBuilder.Entity<Country>(entity =>
                {
                    entity.ToTable("Country", "gral");

                    entity.Property(e => e.Id)
                        .HasColumnName("CountryID")
                        .HasMaxLength(100)
                        .ValueGeneratedNever();

                    entity.Property(e => e.CountryAcronym)
                        .IsRequired()
                        .HasMaxLength(100);
                });

                modelBuilder.Entity<Currency>(entity =>
                {
                    entity.ToTable("Currency", "gral");

                    entity.Property(e => e.Id)
                        .HasColumnName("CurrencyID")
                        .HasMaxLength(100)
                        .ValueGeneratedNever();

                    entity.Property(e => e.CurrencyValue)
                        .IsRequired()
                        .HasMaxLength(5);

                    entity.Property(e => e.Description).HasMaxLength(50);
                });

                modelBuilder.Entity<DefaultRolePermission>(entity =>
                {
                    entity.ToTable("DefaultRolePermission", "userm");

                    entity.HasIndex(e => new { e.Rvid, e.Orgid, e.PermissionId })
                        .HasName("IX_DefaultRolePermission")
                        .IsUnique();

                    entity.Property(e => e.Id)
                        .HasColumnName("DefaultRolePermissionID")
                        .HasMaxLength(100)
                        .ValueGeneratedNever();

                    entity.Property(e => e.Description).HasMaxLength(250);

                    entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                    entity.Property(e => e.ModificationUser)
                        .IsRequired()
                        .HasMaxLength(100);

                    entity.Property(e => e.Orgid)
                        .IsRequired()
                        .HasColumnName("ORGID")
                        .HasMaxLength(100);

                    entity.Property(e => e.PermissionId)
                        .IsRequired()
                        .HasColumnName("PermissionID")
                        .HasMaxLength(100);

                    entity.Property(e => e.Rvid)
                        .IsRequired()
                        .HasColumnName("RVID")
                        .HasMaxLength(100);

                    entity.HasOne(d => d.User)
                        .WithMany(p => p.DefaultRolePermission)
                        .HasForeignKey(d => d.ModificationUser)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_DefaultRolePermission_User");

                    entity.HasOne(d => d.Permission)
                        .WithMany(p => p.DefaultRolePermission)
                        .HasForeignKey(d => d.PermissionId)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_DefaultRolePermission_Permission");

                    entity.HasOne(d => d.OrganizationRole)
                        .WithMany(p => p.DefaultRolePermission)
                        .HasPrincipalKey(p => new { p.Rvid, p.OrganizationId })
                        .HasForeignKey(d => new { d.Rvid, d.Orgid })
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_DefaultRolePermission_OrganizationRole");
                });

                modelBuilder.Entity<Feature>(entity =>
                {
                    entity.HasKey(e => e.Id)
                       .HasName("PK_Feature_1");

                    entity.ToTable("Feature", "app");

                    entity.Property(e => e.Id)
                        .HasColumnName("FeatureID")
                        .HasMaxLength(100)
                        .ValueGeneratedNever();

                    entity.Property(e => e.Description).HasMaxLength(100);

                    entity.Property(e => e.FeatureName).HasMaxLength(50);

                    entity.Property(e => e.ResourceTypeId)
                        .IsRequired()
                        .HasColumnName("ResourceTypeID")
                        .HasMaxLength(100);

                    entity.HasOne(d => d.ResourceType)
                        .WithMany(p => p.Features)
                        .HasForeignKey(d => d.ResourceTypeId)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Feature_ResourceType");
                });

                modelBuilder.Entity<FinancingRepayment>(entity =>
                {
                    entity.ToTable("FinancingRepayment", "gral");

                    entity.Property(e => e.Id)
                        .HasColumnName("ID")
                        .HasMaxLength(100)
                        .ValueGeneratedNever();

                    entity.Property(e => e.Description).HasMaxLength(150);

                    entity.Property(e => e.Value).HasMaxLength(150);
                });

                modelBuilder.Entity<Industry>(entity =>
                {
                    entity.ToTable("Industry", "gral");

                    entity.Property(e => e.Id)
                        .HasColumnName("IndustryID")
                        .HasMaxLength(100)
                        .ValueGeneratedNever();

                    entity.Property(e => e.Description).HasMaxLength(150);

                    entity.Property(e => e.IndustryValue)
                        .IsRequired()
                        .HasMaxLength(150);
                });

                modelBuilder.Entity<LogUserByApp>(entity =>
                {
                    entity.HasKey(e => e.Id);

                    entity.ToTable("LogUserByApp", "app");

                    entity.Property(e => e.Id)
                        .HasColumnName("LogUAID")
                        .HasMaxLength(100)
                        .ValueGeneratedNever();

                    entity.Property(e => e.ActionId)
                        .HasColumnName("ActionID")
                        .HasMaxLength(100);

                    entity.Property(e => e.Appid)
                        .IsRequired()
                        .HasColumnName("APPID")
                        .HasMaxLength(100);

                    entity.Property(e => e.Description).HasMaxLength(150);

                    entity.Property(e => e.EndDate).HasColumnType("datetime");

                    entity.Property(e => e.StartDate).HasColumnType("datetime");

                    entity.Property(e => e.Userid)
                        .IsRequired()
                        .HasColumnName("USERID")
                        .HasMaxLength(100);
                });

                modelBuilder.Entity<Organization>(entity =>
                {
                    entity.HasKey(e => e.Id);

                    entity.ToTable("Organization", "userm");

                    entity.Property(e => e.Id)
                        .HasColumnName("ORGID")
                        .HasMaxLength(100)
                        .ValueGeneratedNever();

                    entity.Property(e => e.LucanetId)
                        .HasColumnName("LucanetId");
                    //.HasMaxLength(100)
                    //.ValueGeneratedNever();

                    entity.Property(e => e.OrganizationDescription).HasMaxLength(150);

                    entity.Property(e => e.OrganizationName)
                        .IsRequired()
                        .HasMaxLength(50);

                    entity.Property(e => e.ResourceId)
                        .HasColumnName("ResourceID")
                        .HasMaxLength(100);

                    entity.HasOne(d => d.Resource)
                        .WithMany(p => p.Organizations)
                        .HasForeignKey(d => d.ResourceId)
                        .HasConstraintName("FK_Organization_Resource");
                });

                modelBuilder.Entity<OrganizationRole>(entity =>
                {
                    entity.ToTable("OrganizationRole", "userm");

                    entity.HasIndex(e => new { e.Rvid, e.OrganizationId })
                        .HasName("IX_OrganizationRole")
                        .IsUnique();

                    entity.Property(e => e.Id)
                        .HasColumnName("OrganizationRoleID")
                        .HasMaxLength(100)
                        .ValueGeneratedNever();

                    entity.Property(e => e.Description).HasMaxLength(150);

                    entity.Property(e => e.OrganizationId)
                        .IsRequired()
                        .HasColumnName("ORGID")
                        .HasMaxLength(100);

                    entity.Property(e => e.Rvid)
                        .IsRequired()
                        .HasColumnName("RVID")
                        .HasMaxLength(100);

                    entity.HasOne(d => d.Organization)
                        .WithMany(p => p.OrganizationsRoles)
                        .HasForeignKey(d => d.OrganizationId)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_OrganizationRole_Organization");

                    entity.HasOne(d => d.RoleValue)
                        .WithMany(p => p.OrganizationRoles)
                        .HasForeignKey(d => d.Rvid)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_OrganizationRole_RoleValue");
                });

                modelBuilder.Entity<PasswordRequests>(entity =>
                {
                    entity.HasKey(e => e.RequestId);

                    entity.ToTable("PasswordRequests", "userm");

                    entity.Property(e => e.RequestId)
                        .HasColumnName("REQUESTID")
                        .HasMaxLength(255)
                        .ValueGeneratedNever();

                    entity.Property(e => e.RequestDate).HasColumnType("datetime");

                    entity.Property(e => e.Userid)
                        .IsRequired()
                        .HasColumnName("USERID")
                        .HasMaxLength(100);

                    entity.HasOne(d => d.User)
                        .WithMany(p => p.PasswordRequests)
                        .HasForeignKey(d => d.Userid)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_PasswordRequests_User");
                });

                modelBuilder.Entity<Permission>(entity =>
                {

                    entity.ToTable("Permission", "userm");

                    entity.Property(e => e.Id)
                        .HasColumnName("PermissionID")
                        .HasMaxLength(100)
                        .ValueGeneratedNever();

                    entity.Property(e => e.PermissionCode).HasMaxLength(50);

                    entity.Property(e => e.PermissionRelevance)
                        .IsRequired()
                        .HasMaxLength(50);

                    entity.Property(e => e.Style).HasMaxLength(150);
                });

                modelBuilder.Entity<ProcessType>(entity =>
                {
                    entity.ToTable("ProcessType", "gral");

                    entity.Property(e => e.Id)
                        .HasColumnName("ProcessTypeID")
                        .HasMaxLength(100)
                        .ValueGeneratedNever();

                    entity.Property(e => e.Description).HasMaxLength(150);

                    entity.Property(e => e.ProcessTypeValue)
                        .IsRequired()
                        .HasMaxLength(100);
                });

                modelBuilder.Entity<Resource>(entity =>
                {
                    entity.ToTable("Resource", "app");

                    entity.Property(e => e.Id)
                        .HasColumnName("ResourceID")
                        .HasMaxLength(100)
                        .ValueGeneratedNever();

                    entity.Property(e => e.Description).HasMaxLength(150);

                    entity.Property(e => e.ResourceType).HasMaxLength(100);

                    entity.HasOne(d => d.ResourceTypeNavigation)
                        .WithMany(p => p.Resources)
                        .HasForeignKey(d => d.ResourceType)
                        .HasConstraintName("FK_Resource_ResourceType");
                });

                modelBuilder.Entity<ResourceType>(entity =>
                {
                    entity.ToTable("ResourceType", "app");

                    entity.Property(e => e.Id)
                        .HasColumnName("ResourceTypeID")
                        .HasMaxLength(100)
                        .ValueGeneratedNever();

                    entity.Property(e => e.Description).HasMaxLength(150);

                    entity.Property(e => e.ResourceTypeName)
                        .IsRequired()
                        .HasMaxLength(50);
                });

                modelBuilder.Entity<RoleAppPermission>(entity =>
                {
                    entity.ToTable("RoleAppPermission", "userm");

                    entity.HasIndex(e => new { e.Rvid, e.Orgid, e.Appid, e.PermissionId })
                        .HasName("IX_RoleAppPermission")
                        .IsUnique();

                    entity.Property(e => e.Id)
                        .HasColumnName("RoleAppPermissionID")
                        .HasMaxLength(100)
                        .ValueGeneratedNever();

                    entity.Property(e => e.Appid)
                        .IsRequired()
                        .HasColumnName("APPID")
                        .HasMaxLength(100);

                    entity.Property(e => e.ExpirationDateTime).HasColumnType("datetime");

                    entity.Property(e => e.Orgid)
                        .IsRequired()
                        .HasColumnName("ORGID")
                        .HasMaxLength(100);

                    entity.Property(e => e.PermissionId)
                        .IsRequired()
                        .HasColumnName("PermissionID")
                        .HasMaxLength(100);

                    entity.Property(e => e.Rvid)
                        .IsRequired()
                        .HasColumnName("RVID")
                        .HasMaxLength(100);

                    entity.HasOne(d => d.App)
                        .WithMany(p => p.RoleAppPermission)
                        .HasForeignKey(d => d.Appid)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_RoleAppPermission_Apps");

                    entity.HasOne(d => d.PermissionUser)
                        .WithMany(p => p.RoleAppPermission)
                        .HasForeignKey(d => d.PermissionId)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_RoleAppPermission_Permission");

                    entity.HasOne(d => d.OrganizationRole)
                        .WithMany(p => p.RoleAppPermission)
                        .HasPrincipalKey(p => new { p.Rvid, p.OrganizationId })
                        .HasForeignKey(d => new { d.Rvid, d.Orgid })
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_RoleAppPermission_OrganizationRole");
                });

                modelBuilder.Entity<RoleFilePermission>(entity =>
                {
                    entity.HasKey(e => new { e.Rvid, e.Orgid, e.PermissionId, e.Id })
                        .HasName("PK_RoleFilePermission_1");

                    entity.ToTable("RoleFilePermission", "userm");

                    entity.Property(e => e.Rvid)
                        .HasColumnName("RVID")
                        .HasMaxLength(100);

                    entity.Property(e => e.Orgid)
                        .HasColumnName("ORGID")
                        .HasMaxLength(100);

                    entity.Property(e => e.PermissionId)
                        .HasColumnName("PermissionID")
                        .HasMaxLength(100);

                    entity.Property(e => e.Id)
                        .HasColumnName("FileID")
                        .HasMaxLength(100);

                    entity.Property(e => e.ExpirationDateTime).HasColumnType("datetime");
                });

                modelBuilder.Entity<RoleProjectPermission>(entity =>
                {
                    entity.ToTable("RoleProjectPermission", "userm");

                    entity.HasIndex(e => new { e.Rvid, e.Orgid, e.ProjectId, e.PermissionId })
                        .HasName("IX_RoleProjectPermission");

                    entity.Property(e => e.Id)
                        .HasColumnName("RoleProjectPermissionID")
                        .HasMaxLength(100)
                        .ValueGeneratedNever();

                    entity.Property(e => e.ExpirationDateTime).HasColumnType("datetime");

                    entity.Property(e => e.Orgid)
                        .IsRequired()
                        .HasColumnName("ORGID")
                        .HasMaxLength(100);

                    entity.Property(e => e.PermissionId)
                        .IsRequired()
                        .HasColumnName("PermissionID")
                        .HasMaxLength(100);

                    entity.Property(e => e.ProjectId)
                        .IsRequired()
                        .HasColumnName("ProjectID")
                        .HasMaxLength(100);

                    entity.Property(e => e.Rvid)
                        .IsRequired()
                        .HasColumnName("RVID")
                        .HasMaxLength(100);

                    entity.HasOne(d => d.Permissions)
                        .WithMany(p => p.RoleProjectPermission)
                        .HasForeignKey(d => d.PermissionId)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_RoleProjectPermission_Permission");

                    //entity.HasOne(d => d.Projects)
                    //    .WithMany(p => p.RoleProjectPermission)
                    //    .HasForeignKey(d => d.ProjectId)
                    //    .OnDelete(DeleteBehavior.ClientSetNull)
                    //    .HasConstraintName("FK_RoleProjectPermission_Project");

                    entity.HasOne(d => d.OrganizationRole)
                        .WithMany(p => p.RoleProjectPermission)
                        .HasPrincipalKey(p => new { p.Rvid, p.OrganizationId })
                        .HasForeignKey(d => new { d.Rvid, d.Orgid })
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_RoleProjectPermission_OrganizationRole");
                });

                modelBuilder.Entity<RoleType>(entity =>
                {
                    entity.HasKey(e => e.Id);

                    entity.ToTable("RoleType", "userm");

                    entity.Property(e => e.Id)
                        .HasColumnName("RTID")
                        .HasMaxLength(100)
                        .ValueGeneratedNever();

                    entity.Property(e => e.Description).HasMaxLength(150);

                    entity.Property(e => e.RoleTypeName)
                        .IsRequired()
                        .HasMaxLength(50);
                });

                modelBuilder.Entity<RoleValue>(entity =>
                {
                    entity.HasKey(e => e.Id)
                        .HasName("PK_RoleValues");

                    entity.ToTable("RoleValue", "userm");

                    entity.Property(e => e.Id)
                        .HasColumnName("RVID")
                        .HasMaxLength(100)
                        .ValueGeneratedNever();

                    entity.Property(e => e.ContactEmail).HasMaxLength(150);

                    entity.Property(e => e.Description).HasMaxLength(150);

                    entity.Property(e => e.Rtid)
                        .HasColumnName("RTID")
                        .HasMaxLength(100);

                    entity.Property(e => e.Value)
                        .IsRequired()
                        .HasMaxLength(50);

                    entity.HasOne(d => d.Role)
                        .WithMany(p => p.RoleValues)
                        .HasForeignKey(d => d.Rtid)
                        .HasConstraintName("FK_RoleValue_RoleType");
                });

                modelBuilder.Entity<SaleReason>(entity =>
                {
                    entity.ToTable("SaleReason", "gral");

                    entity.Property(e => e.Id)
                        .HasColumnName("SaleReasonID")
                        .HasMaxLength(100)
                        .ValueGeneratedNever();

                    entity.Property(e => e.Description).HasMaxLength(150);

                    entity.Property(e => e.SaleReasonValue)
                        .IsRequired()
                        .HasMaxLength(150);
                });

                modelBuilder.Entity<SegmentProductCategory>(entity =>
                {
                    entity.HasKey(e => e.Id);

                    entity.ToTable("SegmentProductCategory", "gral");

                    entity.Property(e => e.Id)
                        .HasColumnName("SegProdCatID")
                        .HasMaxLength(100)
                        .ValueGeneratedNever();

                    entity.Property(e => e.Description).HasMaxLength(50);

                    entity.Property(e => e.SegProdCatValue)
                        .IsRequired()
                        .HasMaxLength(150);
                });

                modelBuilder.Entity<Seller>(entity =>
                {
                    entity.ToTable("Seller", "gral");

                    entity.Property(e => e.Id)
                        .HasColumnName("SellerID")
                        .HasMaxLength(100)
                        .ValueGeneratedNever();

                    entity.Property(e => e.Description).HasMaxLength(50);

                    entity.Property(e => e.SellerValue)
                        .IsRequired()
                        .HasMaxLength(150);
                });

                modelBuilder.Entity<Session>(entity =>
                {
                    entity.ToTable("Session", "gral");

                    entity.Property(e => e.Id)
                        .HasColumnName("SessionID")
                        .HasMaxLength(32)
                        .IsUnicode(false)
                        .ValueGeneratedNever();

                    entity.Property(e => e.Access).HasColumnType("datetime");

                    entity.Property(e => e.Data).HasColumnType("text");

                    entity.Property(e => e.Expires).HasColumnType("datetime");

                    entity.Property(e => e.Userid)
                        .IsRequired()
                        .HasColumnName("USERID")
                        .HasMaxLength(100);
                });

                modelBuilder.Entity<SharedComment>(entity =>
                {
                    entity.ToTable("SharedComment", "app");

                    entity.HasIndex(e => new { e.CommentId, e.Rvid })
                        .HasName("IX_SharedComment")
                        .IsUnique();

                    entity.Property(e => e.Id)
                        .HasColumnName("SharedCommentID")
                        .HasMaxLength(100)
                        .ValueGeneratedNever();

                    entity.Property(e => e.CommentId)
                        .IsRequired()
                        .HasColumnName("CommentID")
                        .HasMaxLength(100);

                    entity.Property(e => e.Date).HasColumnType("datetime");

                    entity.Property(e => e.Rvid)
                        .IsRequired()
                        .HasColumnName("RVID")
                        .HasMaxLength(100);

                    //entity.HasOne(d => d.ProjectComment)
                    //    .WithMany(p => p.SharedComments)
                    //    .HasForeignKey(d => d.CommentId)
                    //    .OnDelete(DeleteBehavior.ClientSetNull)
                    //    .HasConstraintName("FK_SharedComment_Comment");

                    entity.HasOne(d => d.Rv)
                        .WithMany(p => p.SharedComments)
                        .HasForeignKey(d => d.Rvid)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_SharedComment_RoleValue");
                });

                modelBuilder.Entity<SPAMechanism>(entity =>
                {
                    entity.ToTable("SPAMechanism", "gral");

                    entity.Property(e => e.Id)
                        .HasColumnName("ID")
                        .HasMaxLength(100)
                        .ValueGeneratedNever();

                    entity.Property(e => e.Description).HasMaxLength(50);

                    entity.Property(e => e.Value).HasMaxLength(50);
                });

                modelBuilder.Entity<Status>(entity =>
                {
                    entity.ToTable("Status", "gral");

                    entity.Property(e => e.Id)
                        .HasColumnName("StatusID")
                        .HasMaxLength(100)
                        .ValueGeneratedNever();

                    entity.Property(e => e.Description).HasMaxLength(150);

                    entity.Property(e => e.StatusValue)
                        .IsRequired()
                        .HasMaxLength(50);
                });

                modelBuilder.Entity<Tag>(entity =>
                {
                    entity.HasKey(e => e.Id)
                       .HasName("PK_Tags");

                    entity.ToTable("Tag", "gral");

                    entity.Property(e => e.Id)
                        .HasColumnName("TagID")
                        .HasMaxLength(100)
                        .ValueGeneratedNever();

                    entity.Property(e => e.CreationDate).HasColumnType("datetime");

                    entity.Property(e => e.TagName)
                        .IsRequired()
                        .HasMaxLength(250);

                    entity.Property(e => e.TagParent).HasMaxLength(100);

                    entity.HasOne(d => d.TagParentNavigation)
                        .WithMany(p => p.TagParents)
                        .HasForeignKey(d => d.TagParent)
                        .HasConstraintName("FK_Tag_Tag");
                });

                modelBuilder.Entity<TypeOfDisclosure>(entity =>
                {
                    entity.ToTable("TypeOfDisclosure", "gral");

                    entity.Property(e => e.Id)
                        .HasColumnName("ID")
                        .HasMaxLength(100)
                        .ValueGeneratedNever();

                    entity.Property(e => e.Description).HasMaxLength(50);

                    entity.Property(e => e.Value).HasMaxLength(50);
                });

                modelBuilder.Entity<TypeOfPurchase>(entity =>
                {
                    entity.ToTable("TypeOfPurchase", "gral");

                    entity.Property(e => e.Id)
                        .HasColumnName("ID")
                        .HasMaxLength(100)
                        .ValueGeneratedNever();

                    entity.Property(e => e.Description).HasMaxLength(50);

                    entity.Property(e => e.Value).HasMaxLength(50);
                });

                modelBuilder.Entity<User>(entity =>
                {
                    entity.ToTable("User", "userm");

                    entity.Property(e => e.Id)
                        .HasColumnName("ID")
                        .HasMaxLength(100)
                        .ValueGeneratedNever();

                    entity.Property(e => e.DeskPhone).HasMaxLength(50);

                    entity.Property(e => e.Email).HasMaxLength(256);

                    entity.Property(e => e.InitDate).HasColumnType("date");

                    entity.Property(e => e.IsAdemoUser).HasColumnName("IsADemoUser");

                    entity.Property(e => e.Login)
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                    entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                    entity.Property(e => e.ResourceId)
                        .HasColumnName("ResourceID")
                        .HasMaxLength(100);

                    entity.Property(e => e.UserName).HasMaxLength(256);

                    entity.Property(e => e.UserPicture).IsRequired();

                    entity.HasOne(d => d.Resources)
                        .WithMany(p => p.Users)
                        .HasForeignKey(d => d.ResourceId)
                        .HasConstraintName("FK_User_Resource");
                });

                modelBuilder.Entity<UserRoleOrg>(entity =>
                {
                    entity.ToTable("UserRoleOrg", "userm");

                    entity.HasIndex(e => new { e.Userid, e.Rvid, e.Orgid })
                        .HasName("IX_UserRoleOrg")
                        .IsUnique();

                    entity.Property(e => e.Id)
                        .HasColumnName("UserRoleOrgID")
                        .HasMaxLength(100)
                        .ValueGeneratedNever();

                    entity.Property(e => e.Orgid)
                        .IsRequired()
                        .HasColumnName("ORGID")
                        .HasMaxLength(100);

                    entity.Property(e => e.Rvid)
                        .IsRequired()
                        .HasColumnName("RVID")
                        .HasMaxLength(100);

                    entity.Property(e => e.Userid)
                        .IsRequired()
                        .HasColumnName("USERID")
                        .HasMaxLength(100);

                    entity.HasOne(d => d.User)
                        .WithMany(p => p.UserRoleOrg)
                        .HasForeignKey(d => d.Userid)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_UserRoleOrg_User");

                    entity.HasOne(d => d.OrganizationRole)
                        .WithMany(p => p.UserRoleOrg)
                        .HasPrincipalKey(p => new { p.Rvid, p.OrganizationId })
                        .HasForeignKey(d => new { d.Rvid, d.Orgid })
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_UserRoleOrg_OrganizationRole");
                });
            }
        }
    }
}
