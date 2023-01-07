using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vyukovypavouk.Migrations
{
    public partial class DbV9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Prerekvizity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrerekvizityID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prerekvizity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Skupina",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TmSkupina = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    PredmetID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skupina", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Splneni",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SkupinaID = table.Column<int>(type: "int", nullable: false),
                    KapitolaID = table.Column<int>(type: "int", nullable: false)
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
                name: "Predmet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazev = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Vytvoril = table.Column<string>(type: "nvarchar(62)", maxLength: 62, nullable: false),
                    Privatni = table.Column<bool>(type: "bit", nullable: false),
                    SkupinaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Predmet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Predmet_Skupina_SkupinaID",
                        column: x => x.SkupinaID,
                        principalTable: "Skupina",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SkupinaStudent",
                columns: table => new
                {
                    SkupinaID = table.Column<int>(type: "int", nullable: false),
                    StudentID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkupinaStudent", x => new { x.SkupinaID, x.StudentID });
                    table.ForeignKey(
                        name: "FK_SkupinaStudent_Skupina_SkupinaID",
                        column: x => x.SkupinaID,
                        principalTable: "Skupina",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SkupinaStudent_Student_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentSplneni",
                columns: table => new
                {
                    StudentID = table.Column<int>(type: "int", nullable: false),
                    SplneniID = table.Column<int>(type: "int", nullable: false),
                    Uspech = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentSplneni", x => new { x.StudentID, x.SplneniID });
                    table.ForeignKey(
                        name: "FK_StudentSplneni_Splneni_SplneniID",
                        column: x => x.SplneniID,
                        principalTable: "Splneni",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentSplneni_Student_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    PredmetID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kapitoly", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kapitoly_Predmet_PredmetID",
                        column: x => x.PredmetID,
                        principalTable: "Predmet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "kapitolaPrerekvizita",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KapitolaID = table.Column<int>(type: "int", nullable: false),
                    PrerekvizitaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kapitolaPrerekvizita", x => x.Id);
                    table.ForeignKey(
                        name: "FK_kapitolaPrerekvizita_Kapitoly_KapitolaID",
                        column: x => x.KapitolaID,
                        principalTable: "Kapitoly",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_kapitolaPrerekvizita_Prerekvizity_PrerekvizitaID",
                        column: x => x.PrerekvizitaID,
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
                    KapitolaID = table.Column<int>(type: "int", nullable: false),
                    Nazev = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Odkaz = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Videa_Kapitoly_KapitolaID",
                        column: x => x.KapitolaID,
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
                    KapitolaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zadani", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Zadani_Kapitoly_KapitolaID",
                        column: x => x.KapitolaID,
                        principalTable: "Kapitoly",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_kapitolaPrerekvizita_KapitolaID",
                table: "kapitolaPrerekvizita",
                column: "KapitolaID");

            migrationBuilder.CreateIndex(
                name: "IX_kapitolaPrerekvizita_PrerekvizitaID",
                table: "kapitolaPrerekvizita",
                column: "PrerekvizitaID");

            migrationBuilder.CreateIndex(
                name: "IX_Kapitoly_PredmetID",
                table: "Kapitoly",
                column: "PredmetID");

            migrationBuilder.CreateIndex(
                name: "IX_Predmet_SkupinaID",
                table: "Predmet",
                column: "SkupinaID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SkupinaStudent_StudentID",
                table: "SkupinaStudent",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSplneni_SplneniID",
                table: "StudentSplneni",
                column: "SplneniID");

            migrationBuilder.CreateIndex(
                name: "IX_Videa_KapitolaID",
                table: "Videa",
                column: "KapitolaID");

            migrationBuilder.CreateIndex(
                name: "IX_Zadani_KapitolaID",
                table: "Zadani",
                column: "KapitolaID");
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
                name: "Splneni");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "Kapitoly");

            migrationBuilder.DropTable(
                name: "Predmet");

            migrationBuilder.DropTable(
                name: "Skupina");
        }
    }
}
