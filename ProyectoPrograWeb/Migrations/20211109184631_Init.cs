using Microsoft.EntityFrameworkCore.Migrations;

namespace ProyectoPrograWeb.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdoptionRequest",
                columns: table => new
                {
                    idAdoptionRequest = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cellphoneUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    reasonAdoption = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    whereWhoAdoption = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    idUser = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    idPet = table.Column<int>(type: "int", nullable: false),
                    idStatusPet = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdoptionRequest", x => x.idAdoptionRequest);
                });

            migrationBuilder.CreateTable(
                name: "EnergyLevel",
                columns: table => new
                {
                    LevelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LevelName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnergyLevel", x => x.LevelId);
                });

            migrationBuilder.CreateTable(
                name: "Sex",
                columns: table => new
                {
                    idSex = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nameSex = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sex", x => x.idSex);
                });

            migrationBuilder.CreateTable(
                name: "Specie",
                columns: table => new
                {
                    idSpecie = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nameSpecie = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specie", x => x.idSpecie);
                });

            migrationBuilder.CreateTable(
                name: "StatusPet",
                columns: table => new
                {
                    idStatus = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nameStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusPet", x => x.idStatus);
                });

            migrationBuilder.CreateTable(
                name: "Breed",
                columns: table => new
                {
                    idBreed = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idSpecieRace = table.Column<int>(type: "int", nullable: false),
                    nameBreed = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Race", x => x.idBreed);
                    table.ForeignKey(
                        name: "FK_Race_Specie",
                        column: x => x.idSpecieRace,
                        principalTable: "Specie",
                        principalColumn: "idSpecie",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pet",
                columns: table => new
                {
                    idPet = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idSpeciePet = table.Column<int>(type: "int", nullable: false),
                    idBreedPet = table.Column<int>(type: "int", nullable: true),
                    agePet = table.Column<int>(type: "int", nullable: false),
                    isAgeMonth = table.Column<bool>(type: "bit", nullable: false),
                    weightPet = table.Column<decimal>(type: "numeric(18,3)", nullable: false),
                    idSexPet = table.Column<int>(type: "int", nullable: false),
                    namePet = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EnergyLevelId = table.Column<int>(type: "int", nullable: false),
                    descriptionPet = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    photoPathPet = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    idStatusPet = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pet2", x => x.idPet);
                    table.ForeignKey(
                        name: "FK_Pet_EnergyLevel",
                        column: x => x.EnergyLevelId,
                        principalTable: "EnergyLevel",
                        principalColumn: "LevelId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pet_Race",
                        column: x => x.idBreedPet,
                        principalTable: "Breed",
                        principalColumn: "idBreed",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pet_Sex",
                        column: x => x.idSexPet,
                        principalTable: "Sex",
                        principalColumn: "idSex",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pet_Specie",
                        column: x => x.idSpeciePet,
                        principalTable: "Specie",
                        principalColumn: "idSpecie",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pet_StatusPet",
                        column: x => x.idStatusPet,
                        principalTable: "StatusPet",
                        principalColumn: "idStatus",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.Sql(@"
            create view vAdoptionUser as
            SELECT    dbo.AdoptionRequest.idAdoptionRequest, dbo.AdoptionRequest.cellphoneUser, dbo.AdoptionRequest.reasonAdoption, dbo.AdoptionRequest.whereWhoAdoption, dbo.AdoptionRequest.idUser, dbo.AdoptionRequest.idPet, 
                         dbo.AspNetUsers.Email, dbo.AspNetUsers.FirstName, dbo.AspNetUsers.LastName, dbo.Pet.idSpeciePet, dbo.Pet.idBreedPet, dbo.Pet.agePet, dbo.Pet.isAgeMonth, dbo.Pet.weightPet, dbo.Pet.idSexPet, dbo.Pet.namePet, 
                         dbo.Pet.photoPathPet, dbo.Pet.idStatusPet, dbo.Breed.nameBreed, dbo.StatusPet.nameStatus, dbo.Sex.nameSex, dbo.Specie.nameSpecie
            FROM      dbo.Pet INNER JOIN
                         dbo.AdoptionRequest ON dbo.Pet.idPet = dbo.AdoptionRequest.idPet LEFT OUTER JOIN
                         dbo.Breed ON dbo.Pet.idBreedPet = dbo.Breed.idBreed LEFT OUTER JOIN
                         dbo.Sex ON dbo.Pet.idSexPet = dbo.Sex.idSex LEFT OUTER JOIN
                         dbo.Specie ON dbo.Pet.idSpeciePet = dbo.Specie.idSpecie LEFT OUTER JOIN
                         dbo.StatusPet ON dbo.Pet.idStatusPet = dbo.StatusPet.idStatus LEFT OUTER JOIN
                         dbo.AspNetUsers ON dbo.AdoptionRequest.idUser = dbo.AspNetUsers.Id
            ");

            migrationBuilder.Sql(@"
            create view vPets as
            SELECT    dbo.Breed.nameBreed, dbo.Sex.nameSex, dbo.Specie.nameSpecie, dbo.StatusPet.nameStatus, dbo.Pet.idPet, dbo.Pet.idSpeciePet, dbo.Pet.idBreedPet, dbo.Pet.agePet, dbo.Pet.isAgeMonth, dbo.Pet.weightPet, dbo.Pet.idSexPet, 
                         dbo.Pet.namePet, dbo.Pet.descriptionPet, dbo.Pet.photoPathPet, dbo.Pet.idStatusPet, dbo.EnergyLevel.LevelName, dbo.Pet.EnergyLevelId
            FROM      dbo.EnergyLevel INNER JOIN
                         dbo.Pet ON dbo.EnergyLevel.LevelId = dbo.Pet.EnergyLevelId LEFT OUTER JOIN
                         dbo.Breed ON dbo.Pet.idBreedPet = dbo.Breed.idBreed LEFT OUTER JOIN
                         dbo.Sex ON dbo.Pet.idSexPet = dbo.Sex.idSex LEFT OUTER JOIN
                         dbo.Specie ON dbo.Breed.idSpecieRace = dbo.Specie.idSpecie AND dbo.Pet.idSpeciePet = dbo.Specie.idSpecie LEFT OUTER JOIN
                         dbo.StatusPet ON dbo.Pet.idStatusPet = dbo.StatusPet.idStatus
            ");

            migrationBuilder.CreateIndex(
                name: "IX_Breed_idSpecieRace",
                table: "Breed",
                column: "idSpecieRace");

            migrationBuilder.CreateIndex(
                name: "IX_Pet_EnergyLevelId",
                table: "Pet",
                column: "EnergyLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Pet_idBreedPet",
                table: "Pet",
                column: "idBreedPet");

            migrationBuilder.CreateIndex(
                name: "IX_Pet_idSexPet",
                table: "Pet",
                column: "idSexPet");

            migrationBuilder.CreateIndex(
                name: "IX_Pet_idSpeciePet",
                table: "Pet",
                column: "idSpeciePet");

            migrationBuilder.CreateIndex(
                name: "IX_Pet_idStatusPet",
                table: "Pet",
                column: "idStatusPet");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdoptionRequest");

            migrationBuilder.DropTable(
                name: "Pet");

            migrationBuilder.DropTable(
                name: "EnergyLevel");

            migrationBuilder.DropTable(
                name: "Breed");

            migrationBuilder.DropTable(
                name: "Sex");

            migrationBuilder.DropTable(
                name: "StatusPet");

            migrationBuilder.DropTable(
                name: "Specie");
        }
    }
}
