﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace UI.Models
{
    public partial class WonderHardwareContext : DbContext
    {
        public WonderHardwareContext()
        {
        }

        public WonderHardwareContext(DbContextOptions<WonderHardwareContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Case> Cases { get; set; }
        public virtual DbSet<GraphicsCard> GraphicsCards { get; set; }
        public virtual DbSet<Hdd> Hdds { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<Motherboard> Motherboards { get; set; }
        public virtual DbSet<PowerSupply> PowerSupplies { get; set; }
        public virtual DbSet<Processor> Processors { get; set; }
        public virtual DbSet<Ram> Rams { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }
        public virtual DbSet<Ssd> Ssds { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("Server=DESKTOP-R34I8VP;Database=WonderHardware;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Arabic_CI_AS");

            modelBuilder.Entity<Brand>(entity =>
            {
                entity.ToTable("Brand");

                entity.Property(e => e.BrandId).HasColumnName("BrandID");

                entity.Property(e => e.BrandName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Case>(entity =>
            {
                entity.HasKey(e => e.CaseCode);

                entity.ToTable("Case");

                entity.Property(e => e.CaseCode)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.CaseBrandId).HasColumnName("CaseBrandID");

                entity.Property(e => e.CaseFactorySize)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CaseName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.CaseBrand)
                    .WithMany(p => p.Cases)
                    .HasForeignKey(d => d.CaseBrandId)
                    .HasConstraintName("FK_Case_Brand");
            });

            modelBuilder.Entity<GraphicsCard>(entity =>
            {
                entity.HasKey(e => e.Vgacode)
                    .HasName("PK_Graphics Card_1");

                entity.ToTable("Graphics Card");

                entity.Property(e => e.Vgacode)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("VGACode");

                entity.Property(e => e.IntermediateBrandId).HasColumnName("IntermediateBrandID");

                entity.Property(e => e.VgabrandId).HasColumnName("VGABrandID");

                entity.Property(e => e.Vganame)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("VGAName");

                entity.Property(e => e.Vgaprice).HasColumnName("VGAPrice");

                entity.Property(e => e.Vgaquantity).HasColumnName("VGAQuantity");

                entity.Property(e => e.Vgarate).HasColumnName("VGARate");

                entity.Property(e => e.Vram).HasColumnName("VRAM");

                entity.HasOne(d => d.Vgabrand)
                    .WithMany(p => p.GraphicsCards)
                    .HasForeignKey(d => d.VgabrandId)
                    .HasConstraintName("FK_Graphics Card_Brand");
            });

            modelBuilder.Entity<Hdd>(entity =>
            {
                entity.HasKey(e => e.Hddcode)
                    .HasName("PK_HDD_1");

                entity.ToTable("HDD");

                entity.Property(e => e.Hddcode)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("HDDCode");

                entity.Property(e => e.HddbrandId).HasColumnName("HDDBrandID");

                entity.Property(e => e.Hddname)
                    .IsRequired()
                    .HasMaxLength(35)
                    .IsUnicode(false)
                    .HasColumnName("HDDName");

                entity.Property(e => e.Hddprice).HasColumnName("HDDPrice");

                entity.Property(e => e.Hddquantity).HasColumnName("HDDQuantity");

                entity.Property(e => e.Hddrate).HasColumnName("HDDRate");

                entity.Property(e => e.Hddrpm).HasColumnName("HDDRPM");

                entity.Property(e => e.Hddsize).HasColumnName("HDDSize");

                entity.Property(e => e.Hddtype)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("HDDType");

                entity.HasOne(d => d.Hddbrand)
                    .WithMany(p => p.Hdds)
                    .HasForeignKey(d => d.HddbrandId)
                    .HasConstraintName("FK_HDD_Brand");
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.ProductCode)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.ProductImage).HasColumnType("image");

                entity.HasOne(d => d.ProductCodeNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.ProductCode)
                    .HasConstraintName("FK_Images_Case");

                entity.HasOne(d => d.ProductCode1)
                    .WithMany()
                    .HasForeignKey(d => d.ProductCode)
                    .HasConstraintName("FK_Images_HDD");

                entity.HasOne(d => d.ProductCode2)
                    .WithMany()
                    .HasForeignKey(d => d.ProductCode)
                    .HasConstraintName("FK_Images_Motherboard");

                entity.HasOne(d => d.ProductCode3)
                    .WithMany()
                    .HasForeignKey(d => d.ProductCode)
                    .HasConstraintName("FK_Images_Processor");

                entity.HasOne(d => d.ProductCode4)
                    .WithMany()
                    .HasForeignKey(d => d.ProductCode)
                    .HasConstraintName("FK_Images_Power Supply");

                entity.HasOne(d => d.ProductCode5)
                    .WithMany()
                    .HasForeignKey(d => d.ProductCode)
                    .HasConstraintName("FK_Images_Ram");

                entity.HasOne(d => d.ProductCode6)
                    .WithMany()
                    .HasForeignKey(d => d.ProductCode)
                    .HasConstraintName("FK_Images_SSD");

                entity.HasOne(d => d.ProductCode7)
                    .WithMany()
                    .HasForeignKey(d => d.ProductCode)
                    .HasConstraintName("FK_Images_Graphics Card");
            });

            modelBuilder.Entity<Motherboard>(entity =>
            {
                entity.HasKey(e => e.MotherCode)
                    .HasName("PK_Motherboard_1");

                entity.ToTable("Motherboard");

                entity.Property(e => e.MotherCode)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.MotherBrandId).HasColumnName("MotherBrandID");

                entity.Property(e => e.MotherName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.MotherSocket)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.MotherBrand)
                    .WithMany(p => p.Motherboards)
                    .HasForeignKey(d => d.MotherBrandId)
                    .HasConstraintName("FK_Motherboard_Brand");
            });

            modelBuilder.Entity<PowerSupply>(entity =>
            {
                entity.HasKey(e => e.Psucode);

                entity.ToTable("Power Supply");

                entity.Property(e => e.Psucode)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("PSUCode");

                entity.Property(e => e.PsubrandId).HasColumnName("PSUBrandID");

                entity.Property(e => e.Psucertificate)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PSUCertificate");

                entity.Property(e => e.Psuname)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("PSUName");

                entity.Property(e => e.Psuprice).HasColumnName("PSUPrice");

                entity.Property(e => e.Psuquantity).HasColumnName("PSUQuantity");

                entity.Property(e => e.Psurate).HasColumnName("PSURate");

                entity.Property(e => e.Psuwatt).HasColumnName("PSUWatt");

                entity.HasOne(d => d.Psubrand)
                    .WithMany(p => p.PowerSupplies)
                    .HasForeignKey(d => d.PsubrandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Power Supply_Brand");
            });

            modelBuilder.Entity<Processor>(entity =>
            {
                entity.HasKey(e => e.ProCode);

                entity.ToTable("Processor");

                entity.Property(e => e.ProCode)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("proCode");

                entity.Property(e => e.ProBrandId).HasColumnName("ProBrandID");

                entity.Property(e => e.ProLithography)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.ProName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.ProSocket)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.ProBrand)
                    .WithMany(p => p.Processors)
                    .HasForeignKey(d => d.ProBrandId)
                    .HasConstraintName("FK_Processor_Brand");
            });

            modelBuilder.Entity<Ram>(entity =>
            {
                entity.HasKey(e => e.RamCode);

                entity.ToTable("Ram");

                entity.Property(e => e.RamCode)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.RamBrandId).HasColumnName("RamBrandID");

                entity.Property(e => e.RamName)
                    .IsRequired()
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.Property(e => e.RamType)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.RamBrand)
                    .WithMany(p => p.Rams)
                    .HasForeignKey(d => d.RamBrandId)
                    .HasConstraintName("FK_Ram_Brand");
            });

            modelBuilder.Entity<Sale>(entity =>
            {
                entity.HasKey(e => e.SalesId);

                entity.Property(e => e.Address).IsUnicode(false);

                entity.Property(e => e.CaseCode)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.DateAndTime).HasColumnType("smalldatetime");

                entity.Property(e => e.Hddcode)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("HDDCode");

                entity.Property(e => e.MotherCode)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.ProCode)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("proCode");

                entity.Property(e => e.Psucode)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("PSUCode");

                entity.Property(e => e.RamCode)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Ssdcode)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("SSDCode");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Vgacode)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("VGACode");

                entity.HasOne(d => d.CaseCodeNavigation)
                    .WithMany(p => p.Sales)
                    .HasForeignKey(d => d.CaseCode)
                    .HasConstraintName("FK_Sales_Case");

                entity.HasOne(d => d.HddcodeNavigation)
                    .WithMany(p => p.Sales)
                    .HasForeignKey(d => d.Hddcode)
                    .HasConstraintName("FK_Sales_HDD");

                entity.HasOne(d => d.MotherCodeNavigation)
                    .WithMany(p => p.Sales)
                    .HasForeignKey(d => d.MotherCode)
                    .HasConstraintName("FK_Sales_Motherboard");

                entity.HasOne(d => d.ProCodeNavigation)
                    .WithMany(p => p.Sales)
                    .HasForeignKey(d => d.ProCode)
                    .HasConstraintName("FK_Sales_Processor");

                entity.HasOne(d => d.PsucodeNavigation)
                    .WithMany(p => p.Sales)
                    .HasForeignKey(d => d.Psucode)
                    .HasConstraintName("FK_Sales_Power Supply");

                entity.HasOne(d => d.RamCodeNavigation)
                    .WithMany(p => p.Sales)
                    .HasForeignKey(d => d.RamCode)
                    .HasConstraintName("FK_Sales_Ram");

                entity.HasOne(d => d.SsdcodeNavigation)
                    .WithMany(p => p.Sales)
                    .HasForeignKey(d => d.Ssdcode)
                    .HasConstraintName("FK_Sales_SSD");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Sales)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Sales_User");

                entity.HasOne(d => d.VgacodeNavigation)
                    .WithMany(p => p.Sales)
                    .HasForeignKey(d => d.Vgacode)
                    .HasConstraintName("FK_Sales_Graphics Card");
            });

            modelBuilder.Entity<Ssd>(entity =>
            {
                entity.HasKey(e => e.Ssdcode)
                    .HasName("PK_SSD_1");

                entity.ToTable("SSD");

                entity.Property(e => e.Ssdcode)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("SSDCode");

                entity.Property(e => e.SsdbrandId).HasColumnName("SSDBrandID");

                entity.Property(e => e.Ssdinterface)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("SSDInterface");

                entity.Property(e => e.Ssdname)
                    .IsRequired()
                    .HasMaxLength(35)
                    .IsUnicode(false)
                    .HasColumnName("SSDName");

                entity.Property(e => e.Ssdprice).HasColumnName("SSDPrice");

                entity.Property(e => e.Ssdquantity).HasColumnName("SSDQuantity");

                entity.Property(e => e.Ssdrate).HasColumnName("SSDRate");

                entity.Property(e => e.Ssdsize).HasColumnName("SSDSize");

                entity.HasOne(d => d.Ssdbrand)
                    .WithMany(p => p.Ssds)
                    .HasForeignKey(d => d.SsdbrandId)
                    .HasConstraintName("FK_SSD_Brand");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}