using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LML.NPOManagement.Dal.Models;

public partial class NpomanagementContext : DbContext
{
    public NpomanagementContext()
    {
    }

    public NpomanagementContext(DbContextOptions<NpomanagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<AccountProgress> AccountProgresses { get; set; }

    public virtual DbSet<AccountStatus> AccountStatuses { get; set; }

    public virtual DbSet<Attachment> Attachments { get; set; }

    public virtual DbSet<Donation> Donations { get; set; }

    public virtual DbSet<InventoryType> InventoryTypes { get; set; }

    public virtual DbSet<InvestorInformation> InvestorInformations { get; set; }

    public virtual DbSet<InvestorTierType> InvestorTierTypes { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<NotificationTransportType> NotificationTransportTypes { get; set; }

    public virtual DbSet<NotificationType> NotificationTypes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserIdea> UserIdeas { get; set; }

    public virtual DbSet<UserInformation> UserInformations { get; set; }

    public virtual DbSet<UserInventory> UserInventories { get; set; }

    public virtual DbSet<UserStatus> UserStatuses { get; set; }

    public virtual DbSet<UserType> UserTypes { get; set; }

    public virtual DbSet<UsersGroup> UsersGroups { get; set; }

    public virtual DbSet<UsersGroupStatus> UsersGroupStatuses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=tcp:127.0.0.1,9210;Initial Catalog=NPOManagement;User ID=sa;Password=Hx6^2s%W?{U5Pu$a;encrypt=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.ToTable("Account");

            entity.Property(e => e.Description).HasColumnType("ntext");
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.Status).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("FK_AccountStatus");

            entity.HasMany(d => d.Users).WithMany(p => p.Accounts)
                .UsingEntity<Dictionary<string, object>>(
                    "AccountUserInformationConnection",
                    r => r.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_AccountUserInformationConnection_User"),
                    l => l.HasOne<Account>().WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_AccountUserInformationConnection_Account"),
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

            entity.HasOne(d => d.Account).WithMany(p => p.AccountProgresses)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AccountProgress_Account");
        });

        modelBuilder.Entity<AccountStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AccountS__3214EC07DB00B019");

            entity.ToTable("AccountStatus");

            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Attachment>(entity =>
        {
            entity.ToTable("Attachment");
        });

        modelBuilder.Entity<Donation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Table1");

            entity.ToTable("Donation");

            entity.Property(e => e.Amount).HasColumnType("numeric(10, 3)");
            entity.Property(e => e.DateOfCharity).HasColumnType("datetime");

            entity.HasOne(d => d.Investor).WithMany(p => p.Donations)
                .HasForeignKey(d => d.InvestorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Donation_InvestorInformation");
        });

        modelBuilder.Entity<InventoryType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_AccountManager");

            entity.ToTable("InventoryType");

            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<InvestorInformation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Table2");

            entity.ToTable("InvestorInformation");

            entity.HasOne(d => d.InvestorTier).WithMany(p => p.InvestorInformations)
                .HasForeignKey(d => d.InvestorTierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InvestorInformation_InvestorTierType");

            entity.HasOne(d => d.User).WithMany(p => p.InvestorInformations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InvestorInformation_User");
        });

        modelBuilder.Entity<InvestorTierType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_InvestorRangeType");

            entity.ToTable("InvestorTierType");

            entity.Property(e => e.InvestorTier).HasMaxLength(50);
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

            entity.HasOne(d => d.Attachment).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.AttachmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Notification_Attachment");

            entity.HasOne(d => d.NotificationType).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.NotificationTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Notification_NotificationType");

            entity.HasMany(d => d.NotificationTransportTypes).WithMany(p => p.Notifications)
                .UsingEntity<Dictionary<string, object>>(
                    "NotificationBroadcast",
                    r => r.HasOne<NotificationTransportType>().WithMany()
                        .HasForeignKey("NotificationTransportTypeId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_NotificationBroadcast_NotificationType"),
                    l => l.HasOne<Notification>().WithMany()
                        .HasForeignKey("NotificationId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_NotificationBroadcast_Notification"),
                    j =>
                    {
                        j.HasKey("NotificationId", "NotificationTransportTypeId");
                        j.ToTable("NotificationBroadcast");
                    });

            entity.HasMany(d => d.Users).WithMany(p => p.Notifications)
                .UsingEntity<Dictionary<string, object>>(
                    "Notification2User",
                    r => r.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Notification2User_User"),
                    l => l.HasOne<Notification>().WithMany()
                        .HasForeignKey("NotificationId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Notification2User_Notification"),
                    j =>
                    {
                        j.HasKey("NotificationId", "UserId").HasName("PK_AccounManagerRoleConnection");
                        j.ToTable("Notification2User");
                    });
        });

        modelBuilder.Entity<NotificationTransportType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_NotificationType");

            entity.ToTable("NotificationTransportType");

            entity.Property(e => e.Description).HasMaxLength(50);
        });

        modelBuilder.Entity<NotificationType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_NotificationType_1");

            entity.ToTable("NotificationType");

            entity.Property(e => e.Description).HasMaxLength(50);
            entity.Property(e => e.Uri)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("URI");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.Email)
                .HasMaxLength(128)
                .IsUnicode(false);
            entity.Property(e => e.Password).HasMaxLength(250);

            entity.HasOne(d => d.Status).WithMany(p => p.Users)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("FK_UserStatus");

            entity.HasMany(d => d.Groups).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserGroupMembership",
                    r => r.HasOne<UsersGroup>().WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK__UserGroup__Group__19DFD96B"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK__UserGroup__UserI__114A936A"),
                    j =>
                    {
                        j.HasKey("UserId", "GroupId");
                        j.ToTable("UserGroupMembership");
                    });
        });

        modelBuilder.Entity<UserIdea>(entity =>
        {
            entity.ToTable("UserIdea");

            entity.Property(e => e.IdeaCategory).HasMaxLength(256);

            entity.HasOne(d => d.User).WithMany(p => p.UserIdeas)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserIdea_User");
        });

        modelBuilder.Entity<UserInformation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_UserInformation_1");

            entity.ToTable("UserInformation");

            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.DateOfBirth).HasColumnType("datetime");
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Metadata).HasColumnType("ntext");
            entity.Property(e => e.MiddleName).HasMaxLength(50);
            entity.Property(e => e.PhoneNumber).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.User).WithMany(p => p.UserInformations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserInformation_User");
        });

        modelBuilder.Entity<UserInventory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_AMInventory");

            entity.ToTable("UserInventory");

            entity.Property(e => e.Amount).HasColumnType("numeric(10, 2)");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Description).HasColumnType("ntext");
            entity.Property(e => e.MeasurmentUnit).HasMaxLength(50);

            entity.HasOne(d => d.InventoryType).WithMany(p => p.UserInventories)
                .HasForeignKey(d => d.InventoryTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AccountManagerInventory_InventoryType");

            entity.HasOne(d => d.User).WithMany(p => p.UserInventories)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserInventory_User");
        });

        modelBuilder.Entity<UserStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserStat__3214EC07E128C285");

            entity.ToTable("UserStatus");

            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UserType>(entity =>
        {
            entity.ToTable("UserType");

            entity.Property(e => e.Description).HasMaxLength(50);

            entity.HasMany(d => d.Users).WithMany(p => p.UserTypes)
                .UsingEntity<Dictionary<string, object>>(
                    "User2UserType",
                    r => r.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_User2UserType_User1"),
                    l => l.HasOne<UserType>().WithMany()
                        .HasForeignKey("UserTypeId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_User2UserType_UserType1"),
                    j =>
                    {
                        j.HasKey("UserTypeId", "UserId").HasName("PK_User2UserTypeConnection");
                        j.ToTable("User2UserType");
                    });
        });

        modelBuilder.Entity<UsersGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UsersGro__3214EC0700A942C1");

            entity.ToTable("UsersGroup");

            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DateModified)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.GroupName).HasMaxLength(255);
            entity.Property(e => e.StatusId).HasDefaultValueSql("((1))");

            entity.HasOne(d => d.Creator).WithMany(p => p.UsersGroups)
                .HasForeignKey(d => d.CreatorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UsersGroup_User");

            entity.HasOne(d => d.Status).WithMany(p => p.UsersGroups)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UsersGroup_UsersGroupStatus");
        });

        modelBuilder.Entity<UsersGroupStatus>(entity =>
        {
            entity.ToTable("UsersGroupStatus");

            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
