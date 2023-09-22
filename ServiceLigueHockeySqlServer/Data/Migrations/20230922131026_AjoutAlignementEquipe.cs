using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceLigueHockeySqlServer.Data.Migrations
{
    /// <inheritdoc />
    public partial class AjoutAlignementEquipe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alignement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateDebut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateFin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EquipeId = table.Column<int>(type: "int", nullable: false),
                    JoueurId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alignement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alignement_Equipe_EquipeId",
                        column: x => x.EquipeId,
                        principalTable: "Equipe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Alignement_Joueur_JoueurId",
                        column: x => x.JoueurId,
                        principalTable: "Joueur",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Alignement",
                columns: new[] { "Id", "DateDebut", "DateFin", "EquipeId", "JoueurId" },
                values: new object[] { 1, new DateTime(2016, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Alignement_EquipeId",
                table: "Alignement",
                column: "EquipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Alignement_JoueurId",
                table: "Alignement",
                column: "JoueurId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alignement");
        }
    }
}
