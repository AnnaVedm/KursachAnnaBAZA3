using System;
using System.Collections.Generic;
using KursachAnna.Models;
using Microsoft.EntityFrameworkCore;

namespace KursachAnna.Context;

public partial class User1000Context : DbContext
{
    public User1000Context()
    {
    }

    public User1000Context(DbContextOptions<User1000Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Categories2> Categories2s { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Orderdetail> Orderdetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Useraccount> Useraccounts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=193.176.78.35;port=5433;Database=user1000;Username=user1000;password=uudq");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categories2>(entity =>
        {
            entity.HasKey(e => e.Categoriesid2).HasName("categories_2_pkey");

            entity.ToTable("categories_2");

            entity.Property(e => e.Categoriesid2)
                .ValueGeneratedNever()
                .HasColumnName("categoriesid2");
            entity.Property(e => e.CategoriaName)
                .HasMaxLength(255)
                .HasColumnName("categoria_name");
            entity.Property(e => e.Categoryid1).HasColumnName("categoryid1");

            entity.HasOne(d => d.Categoryid1Navigation).WithMany(p => p.Categories2s)
                .HasForeignKey(d => d.Categoryid1)
                .HasConstraintName("categories_2_categoryid1_fkey");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Categoryid).HasName("categories_pkey");

            entity.ToTable("categories");

            entity.Property(e => e.Categoryid)
                .ValueGeneratedNever()
                .HasColumnName("categoryid");
            entity.Property(e => e.Categoryname)
                .HasMaxLength(50)
                .HasColumnName("categoryname");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Orderid).HasName("orders_pkey");

            entity.ToTable("orders");

            entity.Property(e => e.Orderid)
                .ValueGeneratedNever()
                .HasColumnName("orderid");
            entity.Property(e => e.Orderdate).HasColumnName("orderdate");
            entity.Property(e => e.Totalamount)
                .HasPrecision(10, 2)
                .HasColumnName("totalamount");
        });

        modelBuilder.Entity<Orderdetail>(entity =>
        {
            entity.HasKey(e => e.Orderdetailid).HasName("orderdetails_pkey");

            entity.ToTable("orderdetails");

            entity.Property(e => e.Orderdetailid)
                .ValueGeneratedNever()
                .HasColumnName("orderdetailid");
            entity.Property(e => e.Orderid).HasColumnName("orderid");
            entity.Property(e => e.Productid).HasColumnName("productid");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Unitprice)
                .HasPrecision(10, 2)
                .HasColumnName("unitprice");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.Order).WithMany(p => p.Orderdetails)
                .HasForeignKey(d => d.Orderid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orderdetails_orderid_fkey");

            entity.HasOne(d => d.Product).WithMany(p => p.Orderdetails)
                .HasForeignKey(d => d.Productid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orderdetails_productid_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Orderdetails)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("orderdetails_userid_fkey");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Productid).HasName("products_pkey");

            entity.ToTable("products");

            entity.Property(e => e.Productid)
                .ValueGeneratedNever()
                .HasColumnName("productid");
            entity.Property(e => e.Categoriesid2).HasColumnName("categoriesid2");
            entity.Property(e => e.Categoryid).HasColumnName("categoryid");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Photo).HasColumnName("photo");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");
            entity.Property(e => e.Productname)
                .HasMaxLength(100)
                .HasColumnName("productname");
            entity.Property(e => e.Quantityinstock).HasColumnName("quantityinstock");

            entity.HasOne(d => d.Categoriesid2Navigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.Categoriesid2)
                .HasConstraintName("products_categoriesid2_fkey");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.Categoryid)
                .HasConstraintName("products_categoryid_fkey");
        });

        modelBuilder.Entity<Useraccount>(entity =>
        {
            entity.HasKey(e => e.Userid).HasName("useraccounts_pkey");

            entity.ToTable("useraccounts");

            entity.HasIndex(e => e.Email, "useraccounts_email_key").IsUnique();

            entity.HasIndex(e => e.Telephone, "useraccounts_telephone_key").IsUnique();

            entity.HasIndex(e => e.Username, "useraccounts_username_key").IsUnique();

            entity.Property(e => e.Userid)
                .ValueGeneratedNever()
                .HasColumnName("userid");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Telephone)
                .HasMaxLength(100)
                .HasColumnName("telephone");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
            entity.Property(e => e.Userpassword)
                .HasMaxLength(255)
                .HasColumnName("userpassword");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
