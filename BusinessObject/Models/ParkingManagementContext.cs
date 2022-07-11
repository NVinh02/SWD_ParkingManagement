using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace BusinessObject.Models
{
    public partial class ParkingManagementContext : DbContext
    {
        public ParkingManagementContext()
        {
        }

        public ParkingManagementContext(DbContextOptions<ParkingManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Manager> Managers { get; set; }
        public virtual DbSet<ParkingInfo> ParkingInfos { get; set; }
        public virtual DbSet<Pricing> Pricings { get; set; }
        public virtual DbSet<Slot> Slots { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Vehicle> Vehicles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.;Database=ParkingManagement;Uid=sa;Pwd=1234567890;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Manager>(entity =>
            {
                entity.HasKey(e => e.Username)
                    .HasName("PK__Manager__536C85E54D8569F9");

                entity.ToTable("Manager");

                entity.Property(e => e.Username).HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ParkingInfo>(entity =>
            {
                entity.ToTable("ParkingInfo");

                entity.Property(e => e.ParkingInfoId).HasColumnName("ParkingInfoID");

                entity.Property(e => e.Ccid)
                    .HasMaxLength(50)
                    .HasColumnName("CCID");

                entity.Property(e => e.CheckInTime).HasColumnType("datetime");

                entity.Property(e => e.CheckOutTime).HasColumnType("datetime");

                entity.Property(e => e.PricingId).HasColumnName("PricingID");

                entity.Property(e => e.SlotId).HasColumnName("SlotID");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.VehicleId)
                    .HasMaxLength(50)
                    .HasColumnName("VehicleID");

                entity.HasOne(d => d.Cc)
                    .WithMany(p => p.ParkingInfos)
                    .HasForeignKey(d => d.Ccid)
                    .HasConstraintName("FK__ParkingInf__CCID__4222D4EF");

                entity.HasOne(d => d.Pricing)
                    .WithMany(p => p.ParkingInfos)
                    .HasForeignKey(d => d.PricingId)
                    .HasConstraintName("FK__ParkingIn__Prici__44FF419A");

                entity.HasOne(d => d.Slot)
                    .WithMany(p => p.ParkingInfos)
                    .HasForeignKey(d => d.SlotId)
                    .HasConstraintName("FK__ParkingIn__SlotI__440B1D61");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.ParkingInfos)
                    .HasForeignKey(d => d.VehicleId)
                    .HasConstraintName("FK__ParkingIn__Vehic__4316F928");
            });

            modelBuilder.Entity<Pricing>(entity =>
            {
                entity.ToTable("Pricing");

                entity.Property(e => e.PricingId).HasColumnName("PricingID");

                entity.Property(e => e.DurationType)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Value).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.VehicleType)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Slot>(entity =>
            {
                entity.ToTable("Slot");

                entity.Property(e => e.SlotId).HasColumnName("SlotID");

                entity.Property(e => e.Position)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Ccid)
                    .HasName("PK__User__A9561A42DBAE998F");

                entity.ToTable("User");

                entity.Property(e => e.Ccid)
                    .HasMaxLength(50)
                    .HasColumnName("CCID");

                entity.Property(e => e.Fullname)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.ToTable("Vehicle");

                entity.Property(e => e.VehicleId)
                    .HasMaxLength(50)
                    .HasColumnName("VehicleID");

                entity.Property(e => e.Ccid)
                    .HasMaxLength(50)
                    .HasColumnName("CCID");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Cc)
                    .WithMany(p => p.Vehicles)
                    .HasForeignKey(d => d.Ccid)
                    .HasConstraintName("FK__Vehicle__CCID__3B75D760");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
