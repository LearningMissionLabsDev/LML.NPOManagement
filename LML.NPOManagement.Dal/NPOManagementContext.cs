using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

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

        public virtual DbSet<AccountManager> AccountManagers { get; set; } = null!;
        public virtual DbSet<AccountManagerInfo> AccountManagerInfos { get; set; } = null!;
        public virtual DbSet<Beneficiary> Beneficiaries { get; set; } = null!;
        public virtual DbSet<ContactInfo> ContactInfos { get; set; } = null!;
        public virtual DbSet<Donation> Donations { get; set; } = null!;
        public virtual DbSet<Investor> Investors { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Status> Statuses { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=lmldb.cj8tmk4otjem.eu-west-1.rds.amazonaws.com,1433;Database=NPOManagement;User Id=lmladmin;Password=January2021");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountManager>(entity =>
            {
                entity.ToTable("AccountManager");

                entity.Property(e => e.AccountManagerCategory).HasMaxLength(100);

                entity.Property(e => e.NarrowProfessional).HasMaxLength(100);
            });

            modelBuilder.Entity<AccountManagerInfo>(entity =>
            {
                entity.ToTable("AccountManagerInfo");

                entity.Property(e => e.Email)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.HasOne(d => d.AccountManagerCategory)
                    .WithMany(p => p.AccountManagerInfos)
                    .HasForeignKey(d => d.AccountManagerCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccountManagerInfo_AccountManager");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AccountManagerInfos)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccountManagerInfo_Roles");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.AccountManagerInfos)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccountManagerInfo_Status");
            });

            modelBuilder.Entity<Beneficiary>(entity =>
            {
                entity.ToTable("Beneficiary");

                entity.Property(e => e.DateOfBirth)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Beneficiaries)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Beneficiary_Roles");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Beneficiaries)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Beneficiary_Status");

                entity.HasMany(d => d.AccountManagerInfos)
                    .WithMany(p => p.Beneficiries)
                    .UsingEntity<Dictionary<string, object>>(
                        "AccountManagerBeneficiry",
                        l => l.HasOne<AccountManagerInfo>().WithMany().HasForeignKey("AccountManagerInfoId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_AccountManagerBeneficiry_AccountManagerInfo"),
                        r => r.HasOne<Beneficiary>().WithMany().HasForeignKey("BeneficiryId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_AccountManagerBeneficiry_Beneficiary"),
                        j =>
                        {
                            j.HasKey("BeneficiryId", "AccountManagerInfoId").HasName("PK_ClassroomStudent");

                            j.ToTable("AccountManagerBeneficiry");
                        });
            });

            modelBuilder.Entity<ContactInfo>(entity =>
            {
                entity.ToTable("ContactInfo");

                entity.Property(e => e.Email)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumberOne)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumberTwo)
                    .HasMaxLength(20)
                    .IsUnicode(false);
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
                    .HasConstraintName("FK_Table1_Table2");
            });

            modelBuilder.Entity<Investor>(entity =>
            {
                entity.ToTable("Investor");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Role1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Role");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("Status");

                entity.Property(e => e.StatusTayp)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
