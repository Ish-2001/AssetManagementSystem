using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AssetManagementSystem.Data.Models;

public partial class DBContext : DbContext
{
    public DBContext()
    {
    }

    public DBContext(DbContextOptions<DBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AssetAssignment> AssetAssignments { get; set; }

    public virtual DbSet<AssetCategory> AssetCategories { get; set; }

    public virtual DbSet<AssetDetail> AssetDetails { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=AssetManagementSystem;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AssetAssignment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AssetAss__3214EC07EEB12F4B");

            entity.HasOne(d => d.User).WithMany(p => p.AssetAssignments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AssetAssignment_Users");
        });

        modelBuilder.Entity<AssetCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_AssetDetails");
        });

        modelBuilder.Entity<AssetDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_AssetCategory");

            entity.HasOne(d => d.Category).WithMany(p => p.AssetDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AssetDetails_AssetCategory");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC074465EA03");

            entity.Property(e => e.PhoneNumber).IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
