using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Ads.Models
{
    public partial class Ads_DBContext : DbContext
    {
        public Ads_DBContext()
        {
        }

        public Ads_DBContext(DbContextOptions<Ads_DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblAd> TblAds { get; set; } = null!;
        public virtual DbSet<TblAdOwner> TblAdOwners { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblAd>(entity =>
            {
                entity.HasKey(e => e.AdId)
                    .HasName("PK_ads");

                entity.ToTable("tbl_ads");

                entity.Property(e => e.AdId).HasColumnName("ad_id");

                entity.Property(e => e.AdContent)
                    .HasMaxLength(1000)
                    .HasColumnName("ad_content");

                entity.Property(e => e.AdCost).HasColumnName("ad_cost");

                entity.Property(e => e.AdGoodsPrice).HasColumnName("ad_goods_price");

                entity.Property(e => e.AdOwnerId).HasColumnName("ad_owner_id");

                entity.Property(e => e.AdTitle)
                    .HasMaxLength(255)
                    .HasColumnName("ad_title");

                entity.HasOne(d => d.AdOwner)
                    .WithMany(p => p.TblAds)
                    .HasForeignKey(d => d.AdOwnerId)
                    .HasConstraintName("FK_ad_owner");
            });

            modelBuilder.Entity<TblAdOwner>(entity =>
            {
                entity.HasKey(e => e.OwnId)
                    .HasName("PK_ad_owner");

                entity.ToTable("tbl_ad_owner");

                entity.Property(e => e.OwnId).HasColumnName("own_id");

                entity.Property(e => e.OwnBillingAdress)
                    .HasMaxLength(255)
                    .HasColumnName("own_billing_adress");

                entity.Property(e => e.OwnBillingCounty)
                    .HasMaxLength(255)
                    .HasColumnName("own_billing_county");

                entity.Property(e => e.OwnBillingZip)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("own_billing_zip");

                entity.Property(e => e.OwnCompanyOrgNr)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("own_company_org_nr");

                entity.Property(e => e.OwnDeliveryAdress)
                    .HasMaxLength(255)
                    .HasColumnName("own_delivery_adress");

                entity.Property(e => e.OwnDeliveryCounty)
                    .HasMaxLength(255)
                    .HasColumnName("own_delivery_county");

                entity.Property(e => e.OwnDeliveryZip)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("own_delivery_zip");

                entity.Property(e => e.OwnIsSub).HasColumnName("own_is_sub");

                entity.Property(e => e.OwnName)
                    .HasMaxLength(255)
                    .HasColumnName("own_name");

                entity.Property(e => e.OwnPhone)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("own_phone");

                entity.Property(e => e.OwnSubId).HasColumnName("own_sub_id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
