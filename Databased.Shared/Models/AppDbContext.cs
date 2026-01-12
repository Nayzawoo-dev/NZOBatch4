using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Databased.Blog.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblBlog> TblBlogs { get; set; }

    public virtual DbSet<TblProduct> TblProducts { get; set; }

   

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblBlog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Blog");

            entity.ToTable("Tbl_Blog");

            entity.Property(e => e.Caption).HasMaxLength(500);
            entity.Property(e => e.Date).HasColumnType("datetime");
        });

        modelBuilder.Entity<TblProduct>(entity =>
        {
            entity.ToTable("Tbl_Product");

            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.ProductName).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
