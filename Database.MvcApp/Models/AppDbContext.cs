using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Database.MvcApp.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblMonth> TblMonths { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=Batch4.Database;User Id=sa;Password=sasa@123;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblMonth>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tbl_Mont__3214EC07573F8A81");

            entity.ToTable("Tbl_Months");

            entity.Property(e => e.FestivalEn).HasMaxLength(200);
            entity.Property(e => e.FestivalMm).HasMaxLength(200);
            entity.Property(e => e.MonthEn).HasMaxLength(100);
            entity.Property(e => e.MonthMm).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
