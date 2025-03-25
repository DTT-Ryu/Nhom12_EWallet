using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Nhom12_EWallet.Models;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblBank> TblBanks { get; set; }

    public virtual DbSet<TblBankAccount> TblBankAccounts { get; set; }

    public virtual DbSet<TblRole> TblRoles { get; set; }

    public virtual DbSet<TblTransaction> TblTransactions { get; set; }

    public virtual DbSet<TblUser> TblUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-O4RIVEOG\\SQLEXPRESS;Database=EWallet;Integrated Security=True;Encrypt=False;TrustServerCertificate=True;MultiSubnetFailover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblBank>(entity =>
        {
            entity.HasKey(e => e.SBankIdPk).HasName("PK__tblBanks__EE41F187D032959E");
        });

        modelBuilder.Entity<TblBankAccount>(entity =>
        {
            entity.HasKey(e => e.IAccountIdPk).HasName("PK__tblBankA__0BC743F6C7619718");

            entity.Property(e => e.SStatus).HasDefaultValue("active");

            entity.HasOne(d => d.IUserIdFkNavigation).WithMany(p => p.TblBankAccounts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tblBankAccounts_User");

            entity.HasOne(d => d.SBankIdFkNavigation).WithMany(p => p.TblBankAccounts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tblBankAccounts_Bank");
        });

        modelBuilder.Entity<TblRole>(entity =>
        {
            entity.HasKey(e => e.IRoleIdPk).HasName("PK__tblRoles__380CD3EC732DFFEE");
        });

        modelBuilder.Entity<TblTransaction>(entity =>
        {
            entity.HasKey(e => e.ITransactionIdPk).HasName("PK__tblTrans__E99858FC85EB4859");

            entity.Property(e => e.DCreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.SStatus).HasDefaultValue("pending");

            entity.HasOne(d => d.IBankAccountIdFkNavigation).WithMany(p => p.TblTransactions).HasConstraintName("FK_tblTransactions_Bank");

            entity.HasOne(d => d.IRecipientUserIdFkNavigation).WithMany(p => p.TblTransactionIRecipientUserIdFkNavigations).HasConstraintName("FK_tblTransactions_Recipient");

            entity.HasOne(d => d.ISenderUserIdFkNavigation).WithMany(p => p.TblTransactionISenderUserIdFkNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tblTransactions_Sender");
        });

        modelBuilder.Entity<TblUser>(entity =>
        {
            entity.HasKey(e => e.IUserIdPk).HasName("PK__tblUsers__8691D11D7D706FB5");

            entity.Property(e => e.DCreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DUpdatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FBalance).HasDefaultValue(0m);
            entity.Property(e => e.SStatus).HasDefaultValue("active");

            entity.HasOne(d => d.IRoleIdFkNavigation).WithMany(p => p.TblUsers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tblUsers_Role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
