using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vyukovypavouk.Migrations
{
    public partial class DbV5 : Migration
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
                name: "Prerekvizity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPrerekvizity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prerekvizity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Splneni",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSkupiny = table.Column<int>(type: "int", nullable: false),
                    IdKapitoly = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Splneni", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Jmeno = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prijmeni = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "Skupina",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TmSkupina = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    IDPredmetu = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skupina", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Skupina_Predmet_IDPredmetu",
                        column: x => x.IDPredmetu,
                        principalTable: "Predmet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentSplneni",
                columns: table => new
                {
                    IdStudent = table.Column<int>(type: "int", nullable: false),
                    IdSplneni = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentSplneni", x => new { x.IdStudent, x.IdSplneni });
                    table.ForeignKey(
                        name: "FK_StudentSplneni_Splneni_IdSplneni",
                        column: x => x.IdSplneni,
                        principalTable: "Splneni",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentSplneni_Student_IdStudent",
                        column: x => x.IdStudent,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "kapitolaPrerekvizita",
                columns: table => new
                {
                    KapitolaId = table.Column<int>(type: "int", nullable: false),
                    IdPrerekvizita = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kapitolaPrerekvizita", x => new { x.KapitolaId, x.IdPrerekvizita });
                    table.ForeignKey(
                        name: "FK_kapitolaPrerekvizita_Kapitoly_KapitolaId",
                        column: x => x.KapitolaId,
                        principalTable: "Kapitoly",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_kapitolaPrerekvizita_Prerekvizity_IdPrerekvizita",
                        column: x => x.IdPrerekvizita,
                        principalTable: "Prerekvizity",
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
                    Nazev = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
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

            migrationBuilder.CreateTable(
                name: "SkupinaStudent",
                columns: table => new
                {
                    IdSkupina = table.Column<int>(type: "int", nullable: false),
                    IdStudent = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkupinaStudent", x => new { x.IdSkupina, x.IdStudent });
                    table.ForeignKey(
                        name: "FK_SkupinaStudent_Skupina_IdSkupina",
                        column: x => x.IdSkupina,
                        principalTable: "Skupina",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SkupinaStudent_Student_IdStudent",
                        column: x => x.IdStudent,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_kapitolaPrerekvizita_IdPrerekvizita",
                table: "kapitolaPrerekvizita",
                column: "IdPrerekvizita");

            migrationBuilder.CreateIndex(
                name: "IX_Kapitoly_IdPredmetu",
                table: "Kapitoly",
                column: "IdPredmetu");

            migrationBuilder.CreateIndex(
                name: "IX_Skupina_IDPredmetu",
                table: "Skupina",
                column: "IDPredmetu");

            migrationBuilder.CreateIndex(
                name: "IX_SkupinaStudent_IdStudent",
                table: "SkupinaStudent",
                column: "IdStudent");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSplneni_IdSplneni",
                table: "StudentSplneni",
                column: "IdSplneni");

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
                name: "kapitolaPrerekvizita");

            migrationBuilder.DropTable(
                name: "SkupinaStudent");

            migrationBuilder.DropTable(
                name: "StudentSplneni");

            migrationBuilder.DropTable(
                name: "Videa");

            migrationBuilder.DropTable(
                name: "Zadani");

            migrationBuilder.DropTable(
                name: "Prerekvizity");

            migrationBuilder.DropTable(
                name: "Skupina");

            migrationBuilder.DropTable(
                name: "Splneni");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "Kapitoly");

            migrationBuilder.DropTable(
                name: "Predmet");
        }
    }
}
