using KnightFrank.DAL.Entities.Models.MemfusWongData;
using KnightFrank.DataAccessLayer.EF.Core;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace KnightFrank.DAL
{
    public partial class MemfusWongDataEntities : DataContext
    {
        public MemfusWongDataEntities()
        {
            //this.Database.SetCommandTimeout(60);
        }

        public MemfusWongDataEntities(DbContextOptions<MemfusWongDataEntities> options)
            : base(options)
        {
        }

        public virtual DbSet<Block> Blocks { get; set; }
        public virtual DbSet<Building> Buildings { get; set; }
        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<Estate> Estates { get; set; }
        public virtual DbSet<Floor> Floors { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Mdbimpfl> Mdbimpfls { get; set; }
        public virtual DbSet<Phase> Phases { get; set; }
        public virtual DbSet<Ppa> Ppas { get; set; }
        public virtual DbSet<PropertyInformation> PropertyInformations { get; set; }
        public virtual DbSet<PropertyInformationFull> PropertyInformationFulls { get; set; }
        public virtual DbSet<Street> Streets { get; set; }
        public virtual DbSet<Unit> Units { get; set; }
        public virtual DbSet<Zone> Zones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Building>(entity =>
            {
                entity.Property(e => e.BuildingId).ValueGeneratedNever();
            });

            modelBuilder.Entity<District>(entity =>
            {
                entity.HasOne(d => d.Zone)
                    .WithMany(p => p.Districts)
                    .HasForeignKey(d => d.ZoneId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_District_Zone");
            });

            modelBuilder.Entity<Estate>(entity =>
            {
                entity.Property(e => e.EstateId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Floor>(entity =>
            {
                entity.Property(e => e.FloorId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.Property(e => e.LocationId).ValueGeneratedNever();

                entity.HasOne(d => d.Block)
                    .WithMany(p => p.Locations)
                    .HasForeignKey(d => d.BlockId)
                    .HasConstraintName("FK_Location_Block");

                entity.HasOne(d => d.Building)
                    .WithMany(p => p.Locations)
                    .HasForeignKey(d => d.BuildingId)
                    .HasConstraintName("FK_Location_Building");

                entity.HasOne(d => d.District)
                    .WithMany(p => p.Locations)
                    .HasForeignKey(d => d.DistrictId)
                    .HasConstraintName("FK_Location_District");

                entity.HasOne(d => d.Estate)
                    .WithMany(p => p.Locations)
                    .HasForeignKey(d => d.EstateId)
                    .HasConstraintName("FK_Location_Estate");

                entity.HasOne(d => d.Street1)
                    .WithMany(p => p.LocationStreet1s)
                    .HasForeignKey(d => d.Street1Id)
                    .HasConstraintName("FK_Location_Street");

                entity.HasOne(d => d.Street2)
                    .WithMany(p => p.LocationStreet2s)
                    .HasForeignKey(d => d.Street2Id)
                    .HasConstraintName("FK_Location_Street1");

                entity.HasOne(d => d.Phase)
                    .WithMany(p => p.Locations)
                    .HasForeignKey(d => d.PhaseId)
                    .HasConstraintName("FK_Location_Phase");
            });

            modelBuilder.Entity<Mdbimpfl>(entity =>
            {
                entity.Property(e => e.CreatedBy).IsFixedLength(true);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedBy).IsFixedLength(true);

                entity.Property(e => e.Nature).IsFixedLength(true);
            });

            modelBuilder.Entity<Ppa>(entity =>
            {
                entity.Property(e => e.Guid).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedBy).IsFixedLength(true);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedBy).IsFixedLength(true);

                entity.Property(e => e.PropRefno).IsFixedLength(true);

                entity.HasOne(d => d.DistrictCodeNavigation)
                    .WithMany(p => p.Ppas)
                    .HasForeignKey(d => d.DistrictCode)
                    .HasConstraintName("FK_PPA_District");

                entity.HasOne(d => d.MemNoNavigation)
                    .WithMany(p => p.Ppas)
                    .HasForeignKey(d => d.MemNo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PPA_MDBIMPFL");
            });

            modelBuilder.Entity<PropertyInformation>(entity =>
            {
                entity.Property(e => e.Guid).ValueGeneratedNever();

                entity.HasOne(d => d.Floor)
                    .WithMany(p => p.PropertyInformations)
                    .HasForeignKey(d => d.FloorId)
                    .HasConstraintName("FK_PropertyInformation_Floor");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.PropertyInformations)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PropertyInformation_Location");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.PropertyInformations)
                    .HasForeignKey(d => d.UnitId)
                    .HasConstraintName("FK_PropertyInformation_Unit");
            });

            modelBuilder.Entity<PropertyInformationFull>(entity =>
            {
                entity.ToView("PropertyInformationFull");
            });

            modelBuilder.Entity<Street>(entity =>
            {
                entity.Property(e => e.StreetId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Unit>(entity =>
            {
                entity.Property(e => e.UnitId).ValueGeneratedNever();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
