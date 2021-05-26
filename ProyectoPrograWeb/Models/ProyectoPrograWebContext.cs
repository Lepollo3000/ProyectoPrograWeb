using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ProyectoPrograWeb.Models
{
    public partial class ProyectoPrograWebContext : DbContext
    {
        public ProyectoPrograWebContext()
        {
        }

        public ProyectoPrograWebContext(DbContextOptions<ProyectoPrograWebContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AdoptionRequest> AdoptionRequests { get; set; }
        public virtual DbSet<Breed> Breeds { get; set; }
        public virtual DbSet<EnergyLevel> EnergyLevels { get; set; }
        public virtual DbSet<Pet> Pets { get; set; }
        public virtual DbSet<Sex> Sexes { get; set; }
        public virtual DbSet<Specie> Species { get; set; }
        public virtual DbSet<StatusPet> StatusPets { get; set; }
        public virtual DbSet<VAdoptionUser> VAdoptionUsers { get; set; }
        public virtual DbSet<VPet> VPets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=PetsOnUrHeart;Trusted_Connection=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<AdoptionRequest>(entity =>
            {
                entity.HasKey(e => e.IdAdoptionRequest);

                entity.ToTable("AdoptionRequest");

                entity.Property(e => e.IdAdoptionRequest).HasColumnName("idAdoptionRequest");

                entity.Property(e => e.CellphoneUser)
                    .HasMaxLength(50)
                    .HasColumnName("cellphoneUser");

                entity.Property(e => e.IdPet).HasColumnName("idPet");

                entity.Property(e => e.IdStatusPet).HasColumnName("idStatusPet");

                entity.Property(e => e.IdUser)
                    .IsRequired()
                    .HasMaxLength(450)
                    .HasColumnName("idUser");

                entity.Property(e => e.ReasonAdoption)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasColumnName("reasonAdoption");

                entity.Property(e => e.WhereWhoAdoption)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasColumnName("whereWhoAdoption");
            });

            modelBuilder.Entity<Breed>(entity =>
            {
                entity.HasKey(e => e.IdBreed)
                    .HasName("PK_Race");

                entity.ToTable("Breed");

                entity.Property(e => e.IdBreed).HasColumnName("idBreed");

                entity.Property(e => e.IdSpecieRace).HasColumnName("idSpecieRace");

                entity.Property(e => e.NameBreed)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("nameBreed");

                entity.HasOne(d => d.IdSpecieRaceNavigation)
                    .WithMany(p => p.Breeds)
                    .HasForeignKey(d => d.IdSpecieRace)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Race_Specie");
            });

            modelBuilder.Entity<EnergyLevel>(entity =>
            {
                entity.HasKey(e => e.LevelId);

                entity.ToTable("EnergyLevel");

                entity.Property(e => e.LevelName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Pet>(entity =>
            {
                entity.HasKey(e => e.IdPet)
                    .HasName("PK_Pet2");

                entity.ToTable("Pet");

                entity.Property(e => e.IdPet).HasColumnName("idPet");

                entity.Property(e => e.AgePet).HasColumnName("agePet");

                entity.Property(e => e.DescriptionPet)
                    .HasMaxLength(500)
                    .HasColumnName("descriptionPet");

                entity.Property(e => e.IdBreedPet).HasColumnName("idBreedPet");

                entity.Property(e => e.IdSexPet).HasColumnName("idSexPet");

                entity.Property(e => e.IdSpeciePet).HasColumnName("idSpeciePet");

                entity.Property(e => e.IdStatusPet).HasColumnName("idStatusPet");

                entity.Property(e => e.IsAgeMonth).HasColumnName("isAgeMonth");

                entity.Property(e => e.NamePet)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("namePet");

                entity.Property(e => e.PhotoPathPet)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasColumnName("photoPathPet");

                entity.Property(e => e.WeightPet)
                    .HasColumnType("numeric(18, 3)")
                    .HasColumnName("weightPet");

                entity.HasOne(d => d.EnergyLevel)
                    .WithMany(p => p.Pets)
                    .HasForeignKey(d => d.EnergyLevelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pet_EnergyLevel");

                entity.HasOne(d => d.IdBreedPetNavigation)
                    .WithMany(p => p.Pets)
                    .HasForeignKey(d => d.IdBreedPet)
                    .HasConstraintName("FK_Pet_Race");

                entity.HasOne(d => d.IdSexPetNavigation)
                    .WithMany(p => p.Pets)
                    .HasForeignKey(d => d.IdSexPet)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pet_Sex");

                entity.HasOne(d => d.IdSpeciePetNavigation)
                    .WithMany(p => p.Pets)
                    .HasForeignKey(d => d.IdSpeciePet)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pet_Specie");

                entity.HasOne(d => d.IdStatusPetNavigation)
                    .WithMany(p => p.Pets)
                    .HasForeignKey(d => d.IdStatusPet)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pet_StatusPet");
            });

            modelBuilder.Entity<Sex>(entity =>
            {
                entity.HasKey(e => e.IdSex);

                entity.ToTable("Sex");

                entity.Property(e => e.IdSex).HasColumnName("idSex");

                entity.Property(e => e.NameSex)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("nameSex");
            });

            modelBuilder.Entity<Specie>(entity =>
            {
                entity.HasKey(e => e.IdSpecie);

                entity.ToTable("Specie");

                entity.Property(e => e.IdSpecie).HasColumnName("idSpecie");

                entity.Property(e => e.NameSpecie)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("nameSpecie");
            });

            modelBuilder.Entity<StatusPet>(entity =>
            {
                entity.HasKey(e => e.IdStatus);

                entity.ToTable("StatusPet");

                entity.Property(e => e.IdStatus).HasColumnName("idStatus");

                entity.Property(e => e.NameStatus)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("nameStatus");
            });

            modelBuilder.Entity<VAdoptionUser>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vAdoptionUser");

                entity.Property(e => e.AgePet).HasColumnName("agePet");

                entity.Property(e => e.CellphoneUser)
                    .HasMaxLength(50)
                    .HasColumnName("cellphoneUser");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.IdAdoptionRequest).HasColumnName("idAdoptionRequest");

                entity.Property(e => e.IdBreedPet).HasColumnName("idBreedPet");

                entity.Property(e => e.IdPet).HasColumnName("idPet");

                entity.Property(e => e.IdSexPet).HasColumnName("idSexPet");

                entity.Property(e => e.IdSpeciePet).HasColumnName("idSpeciePet");

                entity.Property(e => e.IdStatusPet).HasColumnName("idStatusPet");

                entity.Property(e => e.IdUser)
                    .IsRequired()
                    .HasMaxLength(450)
                    .HasColumnName("idUser");

                entity.Property(e => e.IsAgeMonth).HasColumnName("isAgeMonth");

                entity.Property(e => e.NameBreed)
                    .HasMaxLength(50)
                    .HasColumnName("nameBreed");

                entity.Property(e => e.NamePet)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("namePet");

                entity.Property(e => e.NameSex)
                    .HasMaxLength(50)
                    .HasColumnName("nameSex");

                entity.Property(e => e.NameSpecie)
                    .HasMaxLength(50)
                    .HasColumnName("nameSpecie");

                entity.Property(e => e.NameStatus)
                    .HasMaxLength(50)
                    .HasColumnName("nameStatus");

                entity.Property(e => e.PhotoPathPet)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasColumnName("photoPathPet");

                entity.Property(e => e.ReasonAdoption)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasColumnName("reasonAdoption");

                entity.Property(e => e.WeightPet)
                    .HasColumnType("numeric(18, 3)")
                    .HasColumnName("weightPet");

                entity.Property(e => e.WhereWhoAdoption)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasColumnName("whereWhoAdoption");
            });

            modelBuilder.Entity<VPet>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vPets");

                entity.Property(e => e.AgePet).HasColumnName("agePet");

                entity.Property(e => e.DescriptionPet)
                    .HasMaxLength(500)
                    .HasColumnName("descriptionPet");

                entity.Property(e => e.IdBreedPet).HasColumnName("idBreedPet");

                entity.Property(e => e.IdPet).HasColumnName("idPet");

                entity.Property(e => e.IdSexPet).HasColumnName("idSexPet");

                entity.Property(e => e.IdSpeciePet).HasColumnName("idSpeciePet");

                entity.Property(e => e.IdStatusPet).HasColumnName("idStatusPet");

                entity.Property(e => e.IsAgeMonth).HasColumnName("isAgeMonth");

                entity.Property(e => e.LevelName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NameBreed)
                    .HasMaxLength(50)
                    .HasColumnName("nameBreed");

                entity.Property(e => e.NamePet)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("namePet");

                entity.Property(e => e.NameSex)
                    .HasMaxLength(50)
                    .HasColumnName("nameSex");

                entity.Property(e => e.NameSpecie)
                    .HasMaxLength(50)
                    .HasColumnName("nameSpecie");

                entity.Property(e => e.NameStatus)
                    .HasMaxLength(50)
                    .HasColumnName("nameStatus");

                entity.Property(e => e.PhotoPathPet)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasColumnName("photoPathPet");

                entity.Property(e => e.WeightPet)
                    .HasColumnType("numeric(18, 3)")
                    .HasColumnName("weightPet");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
