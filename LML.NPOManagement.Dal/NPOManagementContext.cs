using System;
using System.Collections.Generic;
using LML.NPOManagement.Dal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;


namespace LML.NPOManagement.Dal
{
    public partial class NPOManagementContext : DbContext, INPOManagementContext
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
        public virtual DbSet<Donation> Donations { get; set; } = null!;
        public virtual DbSet<InventoryType> InventoryTypes { get; set; } = null!;
        public virtual DbSet<InvestorInformation> InvestorInformations { get; set; } = null!;
        public virtual DbSet<InvestorTierType> InvestorTierTypes { get; set; } = null!;
        public virtual DbSet<Key> Keys { get; set; } = null!;
        public virtual DbSet<Messaging> Messagings { get; set; } = null!;
        public virtual DbSet<Notification> Notifications { get; set; } = null!;
        public virtual DbSet<NotificationTransportType> NotificationTransportTypes { get; set; } = null!;
        public virtual DbSet<NotificationType> NotificationTypes { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserIdea> UserIdeas { get; set; } = null!;
        public virtual DbSet<UserInformation> UserInformations { get; set; } = null!;
        public virtual DbSet<UserInventory> UserInventories { get; set; } = null!;
        public virtual DbSet<UserType> UserTypes { get; set; } = null!;
        public virtual DbSet<UsersGroup> UsersGroups { get; set; } = null!;
		

		public Task<int> SaveChangesAsync()
		{
			throw new NotImplementedException();
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=localhost,1433;Initial Catalog=NPOManagement;User ID=sa;Password=098305624v;TrustServerCertificate=true;Encrypt=False;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.Property(e => e.Description).HasColumnType("ntext");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Status).HasMaxLength(50);

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
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InvestorInformation_InvestorTierType");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.InvestorInformations)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InvestorInformation_User");
            });

            modelBuilder.Entity<InvestorTierType>(entity =>
            {
                entity.ToTable("InvestorTierType");

                entity.Property(e => e.InvestorTier).HasMaxLength(50);
            });

            modelBuilder.Entity<Key>(entity =>
            {
                entity.ToTable("Key");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.PrivateKey).HasColumnType("text");

                entity.Property(e => e.PublicKey).HasColumnType("text");

                entity.Property(e => e.Recovery).HasMaxLength(128);
            });

            modelBuilder.Entity<Messaging>(entity =>
            {
                entity.ToTable("Messaging");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Message).HasColumnType("text");

                entity.Property(e => e.Recovery)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Sender)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.ToTable("Notification");

                entity.Property(e => e.Body).HasColumnType("ntext");

                entity.Property(e => e.Metadata).HasColumnType("ntext");

                entity.Property(e => e.Reminder).HasMaxLength(50);

                entity.Property(e => e.Subject)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.HasOne(d => d.Attachment)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.AttachmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Notification_Attachment");

                entity.HasOne(d => d.NotificationType)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.NotificationTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Notification_NotificationType");

                entity.HasMany(d => d.NotificationTransportTypes)
                    .WithMany(p => p.Notifications)
                    .UsingEntity<Dictionary<string, object>>(
                        "NotificationBroadcast",
                        l => l.HasOne<NotificationTransportType>().WithMany().HasForeignKey("NotificationTransportTypeId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_NotificationBroadcast_NotificationType"),
                        r => r.HasOne<Notification>().WithMany().HasForeignKey("NotificationId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_NotificationBroadcast_Notification"),
                        j =>
                        {
                            j.HasKey("NotificationId", "NotificationTransportTypeId");

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

            modelBuilder.Entity<NotificationTransportType>(entity =>
            {
                entity.ToTable("NotificationTransportType");

                entity.Property(e => e.Description).HasMaxLength(50);
            });

            modelBuilder.Entity<NotificationType>(entity =>
            {
                entity.ToTable("NotificationType");

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.Uri)
                    .HasMaxLength(1024)
                    .IsUnicode(false)
                    .HasColumnName("URI");
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

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Email)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Password).HasMaxLength(250);

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.HasMany(d => d.Groups)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "UserGroupMembership",
                        l => l.HasOne<UsersGroup>().WithMany().HasForeignKey("GroupId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__UserGroup__Group__19DFD96B"),
                        r => r.HasOne<User>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__UserGroup__UserI__47A6A41B"),
                        j =>
                        {
                            j.HasKey("UserId", "GroupId").HasName("PK__UserGrou__A6C1637AC2561575");

                            j.ToTable("UserGroupMembership");
                        });
            });

            modelBuilder.Entity<UserIdea>(entity =>
            {
                entity.ToTable("UserIdea");

                entity.Property(e => e.IdeaCategory).HasMaxLength(256);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserIdeas)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserIdea_User");
            });

            modelBuilder.Entity<UserInformation>(entity =>
            {
                entity.ToTable("UserInformation");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.Metadata).HasColumnType("ntext");

                entity.Property(e => e.MiddleName).HasMaxLength(50);

                entity.Property(e => e.PhoneNumber).HasMaxLength(50);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserInformations)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserInformation_User");
            });

            modelBuilder.Entity<UserInventory>(entity =>
            {
                entity.ToTable("UserInventory");

                entity.Property(e => e.Amount).HasColumnType("numeric(10, 2)");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Description).HasColumnType("ntext");

                entity.Property(e => e.MeasurmentUnit).HasMaxLength(50);

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

                entity.HasMany(d => d.Users)
                    .WithMany(p => p.UserTypes)
                    .UsingEntity<Dictionary<string, object>>(
                        "User2UserType",
                        l => l.HasOne<User>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_User2UserType_User1"),
                        r => r.HasOne<UserType>().WithMany().HasForeignKey("UserTypeId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_User2UserType_UserType1"),
                        j =>
                        {
                            j.HasKey("UserTypeId", "UserId").HasName("PK_User2UserTypeConnection");

                            j.ToTable("User2UserType");
                        });
            });

            modelBuilder.Entity<UsersGroup>(entity =>
            {
                entity.ToTable("UsersGroup");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.GroupName).HasMaxLength(255);

                entity.HasOne(d => d.CreatedByUser)
                    .WithMany(p => p.UsersGroups)
                    .HasForeignKey(d => d.CreatedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UsersGroup_User");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

		void INPOManagementContext.SaveChanges()
		{
			throw new NotImplementedException();
		}
	}
}
