﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProyectoPrograWeb.Models;

namespace ProyectoPrograWeb.Migrations
{
    [DbContext(typeof(ProyectoPrograWebContext))]
    partial class ProyectoPrograWebContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ProyectoPrograWeb.Models.AdoptionRequest", b =>
                {
                    b.Property<int>("IdAdoptionRequest")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("idAdoptionRequest")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CellphoneUser")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("cellphoneUser");

                    b.Property<int>("IdPet")
                        .HasColumnType("int")
                        .HasColumnName("idPet");

                    b.Property<int?>("IdStatusPet")
                        .HasColumnType("int")
                        .HasColumnName("idStatusPet");

                    b.Property<string>("IdUser")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("idUser");

                    b.Property<string>("ReasonAdoption")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("reasonAdoption");

                    b.Property<string>("WhereWhoAdoption")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("whereWhoAdoption");

                    b.HasKey("IdAdoptionRequest");

                    b.ToTable("AdoptionRequest");
                });

            modelBuilder.Entity("ProyectoPrograWeb.Models.Breed", b =>
                {
                    b.Property<int>("IdBreed")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("idBreed")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IdSpecieRace")
                        .HasColumnType("int")
                        .HasColumnName("idSpecieRace");

                    b.Property<string>("NameBreed")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("nameBreed");

                    b.HasKey("IdBreed")
                        .HasName("PK_Race");

                    b.HasIndex("IdSpecieRace");

                    b.ToTable("Breed");
                });

            modelBuilder.Entity("ProyectoPrograWeb.Models.EnergyLevel", b =>
                {
                    b.Property<int>("LevelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("LevelName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("LevelId");

                    b.ToTable("EnergyLevel");
                });

            modelBuilder.Entity("ProyectoPrograWeb.Models.Pet", b =>
                {
                    b.Property<int>("IdPet")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("idPet")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AgePet")
                        .HasColumnType("int")
                        .HasColumnName("agePet");

                    b.Property<string>("DescriptionPet")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("descriptionPet");

                    b.Property<int>("EnergyLevelId")
                        .HasColumnType("int");

                    b.Property<int?>("IdBreedPet")
                        .HasColumnType("int")
                        .HasColumnName("idBreedPet");

                    b.Property<int>("IdSexPet")
                        .HasColumnType("int")
                        .HasColumnName("idSexPet");

                    b.Property<int>("IdSpeciePet")
                        .HasColumnType("int")
                        .HasColumnName("idSpeciePet");

                    b.Property<int>("IdStatusPet")
                        .HasColumnType("int")
                        .HasColumnName("idStatusPet");

                    b.Property<bool>("IsAgeMonth")
                        .HasColumnType("bit")
                        .HasColumnName("isAgeMonth");

                    b.Property<string>("NamePet")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("namePet");

                    b.Property<string>("PhotoPathPet")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("photoPathPet");

                    b.Property<decimal>("WeightPet")
                        .HasColumnType("numeric(18,3)")
                        .HasColumnName("weightPet");

                    b.HasKey("IdPet")
                        .HasName("PK_Pet2");

                    b.HasIndex("EnergyLevelId");

                    b.HasIndex("IdBreedPet");

                    b.HasIndex("IdSexPet");

                    b.HasIndex("IdSpeciePet");

                    b.HasIndex("IdStatusPet");

                    b.ToTable("Pet");
                });

            modelBuilder.Entity("ProyectoPrograWeb.Models.Sex", b =>
                {
                    b.Property<int>("IdSex")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("idSex")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("NameSex")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("nameSex");

                    b.HasKey("IdSex");

                    b.ToTable("Sex");
                });

            modelBuilder.Entity("ProyectoPrograWeb.Models.Specie", b =>
                {
                    b.Property<int>("IdSpecie")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("idSpecie")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("NameSpecie")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("nameSpecie");

                    b.HasKey("IdSpecie");

                    b.ToTable("Specie");
                });

            modelBuilder.Entity("ProyectoPrograWeb.Models.StatusPet", b =>
                {
                    b.Property<int>("IdStatus")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("idStatus")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("NameStatus")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("nameStatus");

                    b.HasKey("IdStatus");

                    b.ToTable("StatusPet");
                });

            modelBuilder.Entity("ProyectoPrograWeb.Models.VAdoptionUser", b =>
                {
                    b.Property<int>("AgePet")
                        .HasColumnType("int")
                        .HasColumnName("agePet");

                    b.Property<string>("CellphoneUser")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("cellphoneUser");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdAdoptionRequest")
                        .HasColumnType("int")
                        .HasColumnName("idAdoptionRequest");

                    b.Property<int?>("IdBreedPet")
                        .HasColumnType("int")
                        .HasColumnName("idBreedPet");

                    b.Property<int>("IdPet")
                        .HasColumnType("int")
                        .HasColumnName("idPet");

                    b.Property<int>("IdSexPet")
                        .HasColumnType("int")
                        .HasColumnName("idSexPet");

                    b.Property<int>("IdSpeciePet")
                        .HasColumnType("int")
                        .HasColumnName("idSpeciePet");

                    b.Property<int>("IdStatusPet")
                        .HasColumnType("int")
                        .HasColumnName("idStatusPet");

                    b.Property<string>("IdUser")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("idUser");

                    b.Property<bool>("IsAgeMonth")
                        .HasColumnType("bit")
                        .HasColumnName("isAgeMonth");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameBreed")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("nameBreed");

                    b.Property<string>("NamePet")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("namePet");

                    b.Property<string>("NameSex")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("nameSex");

                    b.Property<string>("NameSpecie")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("nameSpecie");

                    b.Property<string>("NameStatus")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("nameStatus");

                    b.Property<string>("PhotoPathPet")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("photoPathPet");

                    b.Property<string>("ReasonAdoption")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("reasonAdoption");

                    b.Property<decimal>("WeightPet")
                        .HasColumnType("numeric(18,3)")
                        .HasColumnName("weightPet");

                    b.Property<string>("WhereWhoAdoption")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("whereWhoAdoption");

                    b.ToView("vAdoptionUser");
                });

            modelBuilder.Entity("ProyectoPrograWeb.Models.VPet", b =>
                {
                    b.Property<int>("AgePet")
                        .HasColumnType("int")
                        .HasColumnName("agePet");

                    b.Property<string>("DescriptionPet")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("descriptionPet");

                    b.Property<int>("EnergyLevelId")
                        .HasColumnType("int");

                    b.Property<int?>("IdBreedPet")
                        .HasColumnType("int")
                        .HasColumnName("idBreedPet");

                    b.Property<int>("IdPet")
                        .HasColumnType("int")
                        .HasColumnName("idPet");

                    b.Property<int>("IdSexPet")
                        .HasColumnType("int")
                        .HasColumnName("idSexPet");

                    b.Property<int>("IdSpeciePet")
                        .HasColumnType("int")
                        .HasColumnName("idSpeciePet");

                    b.Property<int>("IdStatusPet")
                        .HasColumnType("int")
                        .HasColumnName("idStatusPet");

                    b.Property<bool>("IsAgeMonth")
                        .HasColumnType("bit")
                        .HasColumnName("isAgeMonth");

                    b.Property<string>("LevelName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("NameBreed")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("nameBreed");

                    b.Property<string>("NamePet")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("namePet");

                    b.Property<string>("NameSex")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("nameSex");

                    b.Property<string>("NameSpecie")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("nameSpecie");

                    b.Property<string>("NameStatus")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("nameStatus");

                    b.Property<string>("PhotoPathPet")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("photoPathPet");

                    b.Property<decimal>("WeightPet")
                        .HasColumnType("numeric(18,3)")
                        .HasColumnName("weightPet");

                    b.ToView("vPets");
                });

            modelBuilder.Entity("ProyectoPrograWeb.Models.Breed", b =>
                {
                    b.HasOne("ProyectoPrograWeb.Models.Specie", "IdSpecieRaceNavigation")
                        .WithMany("Breeds")
                        .HasForeignKey("IdSpecieRace")
                        .HasConstraintName("FK_Race_Specie")
                        .IsRequired();

                    b.Navigation("IdSpecieRaceNavigation");
                });

            modelBuilder.Entity("ProyectoPrograWeb.Models.Pet", b =>
                {
                    b.HasOne("ProyectoPrograWeb.Models.EnergyLevel", "EnergyLevel")
                        .WithMany("Pets")
                        .HasForeignKey("EnergyLevelId")
                        .HasConstraintName("FK_Pet_EnergyLevel")
                        .IsRequired();

                    b.HasOne("ProyectoPrograWeb.Models.Breed", "IdBreedPetNavigation")
                        .WithMany("Pets")
                        .HasForeignKey("IdBreedPet")
                        .HasConstraintName("FK_Pet_Race");

                    b.HasOne("ProyectoPrograWeb.Models.Sex", "IdSexPetNavigation")
                        .WithMany("Pets")
                        .HasForeignKey("IdSexPet")
                        .HasConstraintName("FK_Pet_Sex")
                        .IsRequired();

                    b.HasOne("ProyectoPrograWeb.Models.Specie", "IdSpeciePetNavigation")
                        .WithMany("Pets")
                        .HasForeignKey("IdSpeciePet")
                        .HasConstraintName("FK_Pet_Specie")
                        .IsRequired();

                    b.HasOne("ProyectoPrograWeb.Models.StatusPet", "IdStatusPetNavigation")
                        .WithMany("Pets")
                        .HasForeignKey("IdStatusPet")
                        .HasConstraintName("FK_Pet_StatusPet")
                        .IsRequired();

                    b.Navigation("EnergyLevel");

                    b.Navigation("IdBreedPetNavigation");

                    b.Navigation("IdSexPetNavigation");

                    b.Navigation("IdSpeciePetNavigation");

                    b.Navigation("IdStatusPetNavigation");
                });

            modelBuilder.Entity("ProyectoPrograWeb.Models.Breed", b =>
                {
                    b.Navigation("Pets");
                });

            modelBuilder.Entity("ProyectoPrograWeb.Models.EnergyLevel", b =>
                {
                    b.Navigation("Pets");
                });

            modelBuilder.Entity("ProyectoPrograWeb.Models.Sex", b =>
                {
                    b.Navigation("Pets");
                });

            modelBuilder.Entity("ProyectoPrograWeb.Models.Specie", b =>
                {
                    b.Navigation("Breeds");

                    b.Navigation("Pets");
                });

            modelBuilder.Entity("ProyectoPrograWeb.Models.StatusPet", b =>
                {
                    b.Navigation("Pets");
                });
#pragma warning restore 612, 618
        }
    }
}
