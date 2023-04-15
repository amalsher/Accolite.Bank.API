using Accolite.Bank.Data.MsSql.Entities;
using Microsoft.EntityFrameworkCore;

namespace Accolite.Bank.Data.MsSql.DbContext;

public partial class AccoliteBankContext : Microsoft.EntityFrameworkCore.DbContext
{
    public AccoliteBankContext()
    {
    }

    public AccoliteBankContext(DbContextOptions<AccoliteBankContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AccountEntity> Accounts { get; set; }

    public virtual DbSet<UserEntity> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccountEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_AccountId");

            entity.Property(e => e.Amount).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(16);
            entity.Property(e => e.Updated).HasColumnType("datetime");

            entity.HasOne(d => d.User).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Account_User");
        });

        modelBuilder.Entity<UserEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_UserId");

            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(64);
            entity.Property(e => e.FirstName).HasMaxLength(64);
            entity.Property(e => e.LastName).HasMaxLength(64);
            entity.Property(e => e.Phone).HasMaxLength(16);
            entity.Property(e => e.Updated).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
