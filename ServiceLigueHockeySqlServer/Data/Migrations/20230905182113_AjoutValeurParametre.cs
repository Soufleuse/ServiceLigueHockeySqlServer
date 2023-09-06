using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceLigueHockeySqlServer.Data.Migrations
{
    /// <inheritdoc />
    public partial class AjoutValeurParametre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Parametres",
                columns: new[] { "dateDebut", "nom", "dateFin", "valeur" },
                values: new object[] { new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AjoutSteve", null, "ma valeur" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Parametres",
                keyColumns: new[] { "dateDebut", "nom" },
                keyValues: new object[] { new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AjoutSteve" });
        }
    }
}
