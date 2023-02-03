using System;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BillsServiceLibrary.Models
{
    public partial class BillsContext : DbContext
    {
        public BillsContext()
        {
        }

        public BillsContext(DbContextOptions<BillsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bills> Bills { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<UsersXBills> UsersXBills { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                
                optionsBuilder.UseNpgsql("Host=localhost;Database=Bills;Username=postgres;Password=postgres");
                //optionsBuilder.UseNpgsql(ConfigurationManager.ConnectionStrings["PostgreSql"].ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bills>(entity =>
            {
                entity.HasKey(e => e.BillId)
                    .HasName("bills_pkey");

                entity.ToTable("bills");

                entity.Property(e => e.BillId).HasColumnName("bill_id");

                entity.Property(e => e.BuyerEmail)
                    .HasColumnName("buyer_email")
                    .HasMaxLength(200);

                entity.Property(e => e.BuyerFirstName)
                    .HasColumnName("buyer_first_name")
                    .HasMaxLength(50);

                entity.Property(e => e.BuyerLastName)
                    .HasColumnName("buyer_last_name")
                    .HasMaxLength(50);

                entity.Property(e => e.Currency)
                    .HasColumnName("currency")
                    .HasMaxLength(20);

                entity.Property(e => e.Product)
                    .HasColumnName("product")
                    .HasMaxLength(50);

                entity.Property(e => e.ProductPrice).HasColumnName("product_price");

                entity.Property(e => e.SellerAccountNumber)
                    .HasColumnName("seller_account_number")
                    .HasMaxLength(50);

                entity.Property(e => e.SellerEmail)
                    .HasColumnName("seller_email")
                    .HasMaxLength(200);

                entity.Property(e => e.SellerFirstName)
                    .HasColumnName("seller_first_name")
                    .HasMaxLength(50);

                entity.Property(e => e.SellerLastName)
                    .HasColumnName("seller_last_name")
                    .HasMaxLength(50);

                entity.Property(e => e.SellerNip)
                    .HasColumnName("seller_nip")
                    .HasMaxLength(50);

                entity.Property(e => e.SellerPhone)
                    .HasColumnName("seller_phone")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("users_pkey");

                entity.ToTable("users");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(50);

                entity.Property(e => e.FirstName)
                    .HasColumnName("first_name")
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .HasColumnName("last_name")
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(50);

                entity.Property(e => e.PhoneNumber)
                    .HasColumnName("phone_number")
                    .HasMaxLength(20);

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<UsersXBills>(entity =>
            {
                entity.HasKey(e => e.RelId)
                    .HasName("users_x_bills_pkey");

                entity.ToTable("users_x_bills");

                entity.Property(e => e.RelId).HasColumnName("rel_id");

                entity.Property(e => e.BillId).HasColumnName("bill_id");

                entity.Property(e => e.IsOwner).HasColumnName("is_owner");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Bill)
                    .WithMany(p => p.UsersXBills)
                    .HasForeignKey(d => d.BillId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("users_x_bills_bill_id_fkey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UsersXBills)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("users_x_bills_user_id_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
