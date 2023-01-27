using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using _.WebAPI.Models;

namespace _.WebAPI.Data;

public partial class ModelContext : DbContext
{
    public ModelContext()
    {
    }

    public ModelContext(DbContextOptions<ModelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Usersinfo> Usersinfos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySQL("Name=ConnectionStrings:DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.DbTransactionId).HasName("PRIMARY");

            entity.ToTable("transactions");

            entity.HasIndex(e => new { e.DbTransactionId, e.FromUserId, e.ToUserId }, "DbTransactionId").IsUnique();

            entity.HasIndex(e => e.FromUserId, "FromUserId");

            entity.HasIndex(e => new { e.SystemTransactionId, e.FromUserId, e.ToUserId }, "SystemTransactionId").IsUnique();

            entity.HasIndex(e => e.ToUserId, "ToUserId");

            entity.Property(e => e.DbTransactionId).HasColumnType("int(11)");
            entity.Property(e => e.FromUserId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)");
            entity.Property(e => e.SystemTransactionId)
                .HasMaxLength(30)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.ToUserId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)");
            entity.Property(e => e.TransactionAmount).HasColumnType("int(11)");
            entity.Property(e => e.TransactionDate)
                .HasDefaultValueSql("'current_timestamp()'")
                .HasColumnType("datetime");
            entity.Property(e => e.TransactionType)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("tinyint(4)");

            entity.HasOne(d => d.FromUser).WithMany(p => p.TransactionFromUsers)
                .HasForeignKey(d => d.FromUserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("transactions_ibfk_1");

            entity.HasOne(d => d.ToUser).WithMany(p => p.TransactionToUsers)
                .HasForeignKey(d => d.ToUserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("transactions_ibfk_2");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.DbUserId).HasName("PRIMARY");

            entity.ToTable("users");

            entity.Property(e => e.DbUserId).HasColumnType("int(11)");
            entity.Property(e => e.AccountStatus)
                .HasDefaultValueSql("'1'")
                .HasColumnType("tinyint(4)");
            entity.Property(e => e.Balance).HasColumnType("int(11)");
        });

        modelBuilder.Entity<Usersinfo>(entity =>
        {
            entity.HasKey(e => e.NationalUserId).HasName("PRIMARY");

            entity.ToTable("usersinfo");

            entity.HasIndex(e => e.DbuserId, "DBUserId");

            entity.Property(e => e.NationalUserId).HasColumnType("int(11)");
            entity.Property(e => e.DbuserId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("DBUserId");
            entity.Property(e => e.FirstName).HasMaxLength(30);
            entity.Property(e => e.Joined)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
            entity.Property(e => e.LastName).HasMaxLength(30);
            entity.Property(e => e.LastTopUp)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
            entity.Property(e => e.MiddleName)
                .HasMaxLength(30)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasDefaultValueSql("'NULL'");

            entity.HasOne(d => d.Dbuser).WithMany(p => p.Usersinfos)
                .HasForeignKey(d => d.DbuserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("usersinfo_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
