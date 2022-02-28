using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace LML.NPOManagement.Dal.Models
{
    public partial class NPOManagementContext : DbContext
    {
        public NPOManagementContext()
        {
        }

        public NPOManagementContext(DbContextOptions<NPOManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<AccountProgress> AccountProgresses { get; set; } = null!;
        public virtual DbSet<Attachment> Attachments { get; set; } = null!;
        public virtual DbSet<DailySchedule> DailySchedules { get; set; } = null!;
        public virtual DbSet<Donation> Donations { get; set; } = null!;
        public virtual DbSet<InventoryType> InventoryTypes { get; set; } = null!;
        public virtual DbSet<InvestorInformation> InvestorInformations { get; set; } = null!;
        public virtual DbSet<InvestorTierType> InvestorTierTypes { get; set; } = null!;
        public virtual DbSet<MeetingSchedule> MeetingSchedules { get; set; } = null!;
        public virtual DbSet<Notification> Notifications { get; set; } = null!;
        public virtual DbSet<NotificationType> NotificationTypes { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Template> Templates { get; set; } = null!;
        public virtual DbSet<TemplateType> TemplateTypes { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserInformation> UserInformations { get; set; } = null!;
        public virtual DbSet<UserInventory> UserInventories { get; set; } = null!;
        public virtual DbSet<UserType> UserTypes { get; set; } = null!;
        public virtual DbSet<WeeklySchedule> WeeklySchedules { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //            if (!optionsBuilder.IsConfigured)
            //            {
            //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
            //                optionsBuilder.UseSqlServer("Server=lmldb.cj8tmk4otjem.eu-west-1.rds.amazonaws.com,1433;Database=NPOManagement;User Id=lmladmin;Password=January2021");
            //            }
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                              .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                              .AddJsonFile("appsettings.json")
                              .Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.Property(e => e.Description).HasColumnType("ntext");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasMany(d => d.Users)
                    .WithMany(p => p.Accounts)
                    .UsingEntity<Dictionary<string, object>>(
                        "AccountUserInformationConnection",
                        l => l.HasOne<User>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_AccountUserInformationConnection_User"),
                        r => r.HasOne<Account>().WithMany().HasForeignKey("AccountId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_AccountUserInformationConnection_Account"),
                        j =>
                        {
                            j.HasKey("AccountId", "UserId").HasName("PK_AccountBeneficiaryConnaction");

                            j.ToTable("AccountUserInformationConnection");
                        });
            });

            modelBuilder.Entity<AccountProgress>(entity =>
            {
                entity.ToTable("AccountProgress");

                entity.Property(e => e.Id).HasComment("From Progress in account");

                entity.Property(e => e.Description).HasColumnType("ntext");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.AccountProgresses)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccountProgress_Account");
            });

            modelBuilder.Entity<Attachment>(entity =>
            {
                entity.ToTable("Attachment");
            });

            modelBuilder.Entity<DailySchedule>(entity =>
            {
                entity.ToTable("DailySchedule");

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.StartTime).HasColumnType("datetime");

                entity.HasOne(d => d.WeeklySchedule)
                    .WithMany(p => p.DailySchedules)
                    .HasForeignKey(d => d.WeeklyScheduleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DailySchedule_WeeklySchedule");
            });

            modelBuilder.Entity<Donation>(entity =>
            {
                entity.ToTable("Donation");

                entity.Property(e => e.Amount).HasColumnType("numeric(10, 3)");

                entity.Property(e => e.DateOfCharity).HasColumnType("datetime");

                entity.HasOne(d => d.Investor)
                    .WithMany(p => p.Donations)
                    .HasForeignKey(d => d.InvestorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Donation_InvestorInformation");
            });

            modelBuilder.Entity<InventoryType>(entity =>
            {
                entity.ToTable("InventoryType");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<InvestorInformation>(entity =>
            {
                entity.ToTable("InvestorInformation");

                entity.HasOne(d => d.InvestorTier)
                    .WithMany(p => p.InvestorInformations)
                    .HasForeignKey(d => d.InvestorTierId)
                    .HasConstraintName("FK_InvestorInformation_InvestorTierType");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.InvestorInformations)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_InvestorInformation_User");
            });

            modelBuilder.Entity<InvestorTierType>(entity =>
            {
                entity.ToTable("InvestorTierType");

                entity.Property(e => e.InvestorTier).HasMaxLength(50);
            });

            modelBuilder.Entity<MeetingSchedule>(entity =>
            {
                entity.ToTable("MeetingSchedule");

                entity.Property(e => e.DateInYear).HasColumnType("datetime");

                entity.HasOne(d => d.WeeklySchedule)
                    .WithMany(p => p.MeetingSchedules)
                    .HasForeignKey(d => d.WeeklyScheduleId)
                    .HasConstraintName("FK_MeetingSchedule_WeeklySchedule1");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.ToTable("Notification");

                entity.Property(e => e.Body).HasColumnType("ntext");

                entity.Property(e => e.Metadate).HasColumnType("ntext");

                entity.Property(e => e.Reminder).HasMaxLength(50);

                entity.Property(e => e.Subject)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.HasOne(d => d.Attachment)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.AttachmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Notification_Attachment");

                entity.HasOne(d => d.MeetingSchedule)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.MeetingScheduleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Notification_MeetingSchedule");

                entity.HasMany(d => d.NotificationTypes)
                    .WithMany(p => p.Notifications)
                    .UsingEntity<Dictionary<string, object>>(
                        "NotificationBroadcast",
                        l => l.HasOne<NotificationType>().WithMany().HasForeignKey("NotificationTypeId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_NotificationBroadcast_NotificationType"),
                        r => r.HasOne<Notification>().WithMany().HasForeignKey("NotificationId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_NotificationBroadcast_Notification"),
                        j =>
                        {
                            j.HasKey("NotificationId", "NotificationTypeId");

                            j.ToTable("NotificationBroadcast");
                        });

                entity.HasMany(d => d.Users)
                    .WithMany(p => p.Notifications)
                    .UsingEntity<Dictionary<string, object>>(
                        "Notification2User",
                        l => l.HasOne<User>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_Notification2User_User"),
                        r => r.HasOne<Notification>().WithMany().HasForeignKey("NotificationId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_Notification2User_Notification"),
                        j =>
                        {
                            j.HasKey("NotificationId", "UserId").HasName("PK_AccounManagerRoleConnection");

                            j.ToTable("Notification2User");
                        });
            });

            modelBuilder.Entity<NotificationType>(entity =>
            {
                entity.ToTable("NotificationType");

                entity.Property(e => e.Description).HasMaxLength(50);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.UserRole).HasMaxLength(50);

                entity.HasMany(d => d.Users)
                    .WithMany(p => p.Roles)
                    .UsingEntity<Dictionary<string, object>>(
                        "UserRoleConnection",
                        l => l.HasOne<User>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_UserRoleConnection_User"),
                        r => r.HasOne<Role>().WithMany().HasForeignKey("RoleId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_UserRoleConnection_Role"),
                        j =>
                        {
                            j.HasKey("RoleId", "UserId");

                            j.ToTable("UserRoleConnection");
                        });
            });

            modelBuilder.Entity<Template>(entity =>
            {
                entity.ToTable("Template");

                entity.Property(e => e.Uri).HasColumnName("URI");

                entity.HasOne(d => d.TemplateType)
                    .WithMany(p => p.Templates)
                    .HasForeignKey(d => d.TemplateTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Template_TemplateType");
            });

            modelBuilder.Entity<TemplateType>(entity =>
            {
                entity.ToTable("TemplateType");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description).HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Email)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Password).HasMaxLength(250);

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.HasOne(d => d.UserInformation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserInformationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_UserInformation");

                entity.HasOne(d => d.UserType)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_UserType");
            });

            modelBuilder.Entity<UserInformation>(entity =>
            {
                entity.ToTable("UserInformation");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.Information).HasColumnType("ntext");

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.MiddleName).HasMaxLength(50);

                entity.Property(e => e.PhoneNumber).HasMaxLength(50);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<UserInventory>(entity =>
            {
                entity.ToTable("UserInventory");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Description).HasColumnType("ntext");

                entity.HasOne(d => d.InventoryType)
                    .WithMany(p => p.UserInventories)
                    .HasForeignKey(d => d.InventoryTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccountManagerInventory_InventoryType");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserInventories)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserInventory_User");
            });

            modelBuilder.Entity<UserType>(entity =>
            {
                entity.ToTable("UserType");

                entity.Property(e => e.Description).HasMaxLength(50);
            });

            modelBuilder.Entity<WeeklySchedule>(entity =>
            {
                entity.ToTable("WeeklySchedule");

                entity.Property(e => e.DayOfWeek).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
