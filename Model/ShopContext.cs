using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ASP_NET_CORE_API_For_Shop.Model;

public partial class ShopContext : DbContext
{
    public ShopContext()
    {
    }

    public ShopContext(DbContextOptions<ShopContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Price> Prices { get; set; }
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<Sale> Sales { get; set; }
    public virtual DbSet<Review> Reviews { get; set; }
    public virtual DbSet<Warehouse> Warehouses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Name=DB");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Price>(entity =>
        {
            entity.HasKey(e => e.PriceId).HasName("PK__Prices__4957584FF9452080");

            entity.Property(e => e.PriceId).HasColumnName("PriceID");
            entity.Property(e => e.Price1)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Price");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");

            entity.HasOne(d => d.Product)
                .WithMany(p => p.Prices)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__Prices__ProductID__4222D4EF");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Products__B40CC6ED00C34E0D");

            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.Eancode)
                .HasMaxLength(30)
                .HasColumnName("EANCode");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasKey(e => e.SaleId);
            entity.Property(e => e.QuantitySold).IsRequired();
            entity.HasOne(d => d.Product)
                .WithMany(p => p.Sales)
                .HasForeignKey(d => d.ProductId);
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.ReviewId);
            entity.Property(e => e.Rating).IsRequired();
            entity.HasOne(d => d.Product)
                .WithMany(p => p.Reviews)
                .HasForeignKey(d => d.ProductId);
        });

        modelBuilder.Entity<Warehouse>(entity =>
        {
            entity.HasKey(e => e.WarehouseId);
            entity.Property(e => e.Quantity).IsRequired();
            entity.Property(e => e.WarehouseNumber).IsRequired();
            entity.HasOne(d => d.Product)
                .WithMany(p => p.WarehouseEntries)
                .HasForeignKey(d => d.ProductId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
