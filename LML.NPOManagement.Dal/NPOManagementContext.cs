using Microsoft.EntityFrameworkCore;
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
        public virtual DbSet<AccountManagerInfo> AccountManagerInfos { get; set; } = null!;
        public virtual DbSet<AccountManagerRole> AccountManagerRoles { get; set; } = null!;
        public virtual DbSet<Beneficiary> Beneficiaries { get; set; } = null!;
        public virtual DbSet<BeneficiaryRole> BeneficiaryRoles { get; set; } = null!;
        public virtual DbSet<Donation> Donations { get; set; } = null!;
        public virtual DbSet<Investor> Investors { get; set; } = null!;
        public virtual DbSet<Status> Statuses { get; set; } = null!;

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

                entity.Property(e => e.AccountCategory)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AccountManagerInfo>(entity =>
            {
                entity.ToTable("AccountManagerInfo");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.Gender)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.MiddleName).HasMaxLength(50);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.HasOne(d => d.AccountManagerCategory)
                    .WithMany(p => p.AccountManagerInfos)
                    .HasForeignKey(d => d.AccountManagerCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccountManagerInfo_Account");

             

                entity.HasOne(d => d.AccountManagerCategory1)
                    .WithMany(p => p.AccountManagerInfos)
                    .HasForeignKey(d => d.AccountManagerCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccountManagerInfo_AccountManagerRole1");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.AccountManagerInfos)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccountManagerInfo_Status1");
            });

            modelBuilder.Entity<AccountManagerRole>(entity =>
            {
                entity.ToTable("AccountManagerRole");

                entity.Property(e => e.AccountManagerRoleType)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Beneficiary>(entity =>
            {
                entity.ToTable("Beneficiary");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.Gender)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.MiddleName).HasMaxLength(50);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.HasOne(d => d.BeneficiaryCategory)
                    .WithMany(p => p.Beneficiaries)
                    .HasForeignKey(d => d.BeneficiaryCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Beneficiary_Account");

                entity.HasOne(d => d.BeneficiaryRole)
                    .WithMany(p => p.Beneficiaries)
                    .HasForeignKey(d => d.BeneficiaryRoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Beneficiary_BeneficiaryRole");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Beneficiaries)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Beneficiary_Status1");
            });

            modelBuilder.Entity<BeneficiaryRole>(entity =>
            {
                entity.ToTable("BeneficiaryRole");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.BeneficiaryRoleType)
                    .HasMaxLength(50)
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

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("Status");

                entity.Property(e => e.StatusType)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
