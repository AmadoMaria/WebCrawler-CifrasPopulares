using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace CifrasPopulares.Migrations
{
    public partial class PrimeiroDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Artistas",
                columns: table => new
                {
                    ArtistaID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artistas", x => x.ArtistaID);
                });

            migrationBuilder.CreateTable(
                name: "Rankings",
                columns: table => new
                {
                    RankingID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    data = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rankings", x => x.RankingID);
                });

            migrationBuilder.CreateTable(
                name: "Musicas",
                columns: table => new
                {
                    MusicaID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true),
                    ArtistaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Musicas", x => x.MusicaID);
                    table.ForeignKey(
                        name: "FK_Musicas_Artistas_ArtistaID",
                        column: x => x.ArtistaID,
                        principalTable: "Artistas",
                        principalColumn: "ArtistaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RankingMusicas",
                columns: table => new
                {
                    MusicaID = table.Column<int>(nullable: false),
                    RankingID = table.Column<int>(nullable: false),
                    PosicaoMusica = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RankingMusicas", x => new { x.RankingID, x.MusicaID });
                    table.ForeignKey(
                        name: "FK_RankingMusicas_Musicas_MusicaID",
                        column: x => x.MusicaID,
                        principalTable: "Musicas",
                        principalColumn: "MusicaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RankingMusicas_Rankings_RankingID",
                        column: x => x.RankingID,
                        principalTable: "Rankings",
                        principalColumn: "RankingID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Artistas_Nome",
                table: "Artistas",
                column: "Nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Musicas_ArtistaID",
                table: "Musicas",
                column: "ArtistaID");

            migrationBuilder.CreateIndex(
                name: "IX_RankingMusicas_MusicaID",
                table: "RankingMusicas",
                column: "MusicaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RankingMusicas");

            migrationBuilder.DropTable(
                name: "Musicas");

            migrationBuilder.DropTable(
                name: "Rankings");

            migrationBuilder.DropTable(
                name: "Artistas");
        }
    }
}
