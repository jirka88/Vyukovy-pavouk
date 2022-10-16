using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vyukovypavouk.Migrations
{
    public partial class DbTestV1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Predmet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazev = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Predmet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Skupina",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TmSkupina = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skupina", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Jmeno = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prijmeni = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kapitoly",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Název = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Perex = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Kontent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdPredmetu = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kapitoly", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kapitoly_Predmet_IdPredmetu",
                        column: x => x.IdPredmetu,
                        principalTable: "Predmet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SkupinaPredmet",
                columns: table => new
                {
                    SkupinaId = table.Column<int>(type: "int", nullable: false),
                    PredmetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkupinaPredmet", x => new { x.SkupinaId, x.PredmetId });
                    table.ForeignKey(
                        name: "FK_SkupinaPredmet_Predmet_PredmetId",
                        column: x => x.PredmetId,
                        principalTable: "Predmet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SkupinaPredmet_Skupina_SkupinaId",
                        column: x => x.SkupinaId,
                        principalTable: "Skupina",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SkupinaStudent",
                columns: table => new
                {
                    SkupinaId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkupinaStudent", x => new { x.SkupinaId, x.StudentId });
                    table.ForeignKey(
                        name: "FK_SkupinaStudent_Skupina_SkupinaId",
                        column: x => x.SkupinaId,
                        principalTable: "Skupina",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SkupinaStudent_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Splneni",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_studenta = table.Column<int>(type: "int", nullable: false),
                    Id_skupiny = table.Column<int>(type: "int", nullable: false),
                    Id_kapitoly = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Splneni", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Splneni_Student_Id_studenta",
                        column: x => x.Id_studenta,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Videa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdKapitoly = table.Column<int>(type: "int", nullable: false),
                    Odkaz = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Videa_Kapitoly_IdKapitoly",
                        column: x => x.IdKapitoly,
                        principalTable: "Kapitoly",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Zadani",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Odkaz = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    IdKapitoly = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zadani", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Zadani_Kapitoly_IdKapitoly",
                        column: x => x.IdKapitoly,
                        principalTable: "Kapitoly",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Kapitoly_IdPredmetu",
                table: "Kapitoly",
                column: "IdPredmetu");

            migrationBuilder.CreateIndex(
                name: "IX_SkupinaPredmet_PredmetId",
                table: "SkupinaPredmet",
                column: "PredmetId");

            migrationBuilder.CreateIndex(
                name: "IX_SkupinaStudent_StudentId",
                table: "SkupinaStudent",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Splneni_Id_studenta",
                table: "Splneni",
                column: "Id_studenta");

            migrationBuilder.CreateIndex(
                name: "IX_Videa_IdKapitoly",
                table: "Videa",
                column: "IdKapitoly");

            migrationBuilder.CreateIndex(
                name: "IX_Zadani_IdKapitoly",
                table: "Zadani",
                column: "IdKapitoly");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SkupinaPredmet");

            migrationBuilder.DropTable(
                name: "SkupinaStudent");

            migrationBuilder.DropTable(
                name: "Splneni");

            migrationBuilder.DropTable(
                name: "Videa");

            migrationBuilder.DropTable(
                name: "Zadani");

            migrationBuilder.DropTable(
                name: "Skupina");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "Kapitoly");

            migrationBuilder.DropTable(
                name: "Predmet");
        }
    }
}
