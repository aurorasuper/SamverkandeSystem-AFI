using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Subscribers.Models
{
    public partial class Subscription_DBContext : DbContext
    {
        public Subscription_DBContext()
        {
        }

        public Subscription_DBContext(DbContextOptions<Subscription_DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblSubscriber> TblSubscribers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost,1433;Initial Catalog=Subscription_DB;Persist Security Info=False;User ID=SA;Password=12345OHdf%e;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=60;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblSubscriber>(entity =>
            {
                entity.HasKey(e => e.SubId)
                    .HasName("PK_subscribers");

                entity.ToTable("tbl_subscribers");

                entity.Property(e => e.SubId).HasColumnName("sub_id");

                entity.Property(e => e.SubDeliveryAdress)
                    .HasMaxLength(255)
                    .HasColumnName("sub_delivery_adress");

                entity.Property(e => e.SubDeliveryCounty)
                    .HasMaxLength(255)
                    .HasColumnName("sub_delivery_county");

                entity.Property(e => e.SubDeliveryZip)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("sub_delivery_zip");

                entity.Property(e => e.SubName)
                    .HasMaxLength(255)
                    .HasColumnName("sub_name");

                entity.Property(e => e.SubPhone)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("sub_phone");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
