using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ServiceLigueHockeySqlServer.Data.Migrations
{
    /// <inheritdoc />
    public partial class AjoutParametres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Parametres",
                columns: table => new
                {
                    nom = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    dateDebut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    valeur = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    dateFin = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parametres", x => new { x.nom, x.dateDebut });
                });

            migrationBuilder.InsertData(
                table: "Parametres",
                columns: new[] { "dateDebut", "nom", "dateFin", "valeur" },
                values: new object[,]
                {
                    { new DateTime(1995, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "nombrePartiesJouees", new DateTime(2004, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "82" },
                    { new DateTime(2004, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "nombrePartiesJouees", new DateTime(2005, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "0" },
                    { new DateTime(2005, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "nombrePartiesJouees", new DateTime(2012, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "82" },
                    { new DateTime(2012, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "nombrePartiesJouees", new DateTime(2013, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "48" },
                    { new DateTime(2013, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "nombrePartiesJouees", new DateTime(2019, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "82" },
                    { new DateTime(2019, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "nombrePartiesJouees", new DateTime(2020, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "71" },
                    { new DateTime(2020, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "nombrePartiesJouees", new DateTime(2021, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "56" },
                    { new DateTime(2021, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "nombrePartiesJouees", null, "82" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Parametres");
        }
    }
}
