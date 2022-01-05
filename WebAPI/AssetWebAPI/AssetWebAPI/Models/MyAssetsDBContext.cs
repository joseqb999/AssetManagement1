using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace AssetWebAPI.Models
{
    public partial class MyAssetsDBContext : DbContext
    {
        public MyAssetsDBContext()
        {
        }

        public MyAssetsDBContext(DbContextOptions<MyAssetsDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Asset> Assets { get; set; }
        public virtual DbSet<Color> Colors { get; set; }
        public virtual DbSet<Manufacturer> Manufacturers { get; set; }
        public virtual DbSet<Model> Models { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=10.6.7.11; Initial Catalog=MyAssetsDB; user id=joseg; password=joseg@123");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Asset>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Price).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.PurchaseDate).HasColumnType("datetime");

                entity.HasOne(d => d.Color)
                    .WithMany(p => p.Assets)
                    .HasForeignKey(d => d.ColorId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Assets_Color");

                entity.HasOne(d => d.Manufacturer)
                    .WithMany(p => p.Assets)
                    .HasForeignKey(d => d.ManuFactId)
                    .HasConstraintName("FK_Assets_Manufacturers");

                entity.HasOne(d => d.Model)
                    .WithMany(p => p.Assets)
                    .HasForeignKey(d => d.ModelId)
                    .HasConstraintName("FK_Assets_Models");
            });

            modelBuilder.Entity<Color>(entity =>
            {
                entity.ToTable("Color");

                entity.Property(e => e.ColorId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Manufacturer>(entity =>
            {
                entity.HasKey(e => e.ManuFactId)
                    .HasName("PK__Manufact__3214EC07FF6945C7");

                entity.Property(e => e.ManuFactId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<Model>(entity =>
            {
                entity.Property(e => e.ModelId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
