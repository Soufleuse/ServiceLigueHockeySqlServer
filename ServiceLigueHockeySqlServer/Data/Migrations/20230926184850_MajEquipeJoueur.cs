using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ServiceLigueHockeySqlServer.Data.Migrations
{
    /// <inheritdoc />
    public partial class MajEquipeJoueur : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EquipeJoueur",
                table: "EquipeJoueur");

            migrationBuilder.DeleteData(
                table: "EquipeJoueur",
                keyColumns: new[] { "DateDebutAvecEquipe", "EquipeId", "JoueurId" },
                keyValues: new object[] { new DateTime(2008, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1 });

            migrationBuilder.DeleteData(
                table: "EquipeJoueur",
                keyColumns: new[] { "DateDebutAvecEquipe", "EquipeId", "JoueurId" },
                keyValues: new object[] { new DateTime(2016, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 2 });

            migrationBuilder.DeleteData(
                table: "EquipeJoueur",
                keyColumns: new[] { "DateDebutAvecEquipe", "EquipeId", "JoueurId" },
                keyValues: new object[] { new DateTime(2017, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 3 });

            migrationBuilder.DeleteData(
                table: "EquipeJoueur",
                keyColumns: new[] { "DateDebutAvecEquipe", "EquipeId", "JoueurId" },
                keyValues: new object[] { new DateTime(2013, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 4 });

            migrationBuilder.DeleteData(
                table: "EquipeJoueur",
                keyColumns: new[] { "DateDebutAvecEquipe", "EquipeId", "JoueurId" },
                keyValues: new object[] { new DateTime(2014, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 5 });

            migrationBuilder.DeleteData(
                table: "EquipeJoueur",
                keyColumns: new[] { "DateDebutAvecEquipe", "EquipeId", "JoueurId" },
                keyValues: new object[] { new DateTime(2020, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 6 });

            migrationBuilder.DeleteData(
                table: "EquipeJoueur",
                keyColumns: new[] { "DateDebutAvecEquipe", "EquipeId", "JoueurId" },
                keyValues: new object[] { new DateTime(2018, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 7 });

            migrationBuilder.DeleteData(
                table: "EquipeJoueur",
                keyColumns: new[] { "DateDebutAvecEquipe", "EquipeId", "JoueurId" },
                keyValues: new object[] { new DateTime(2010, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 8 });

            migrationBuilder.DeleteData(
                table: "EquipeJoueur",
                keyColumns: new[] { "DateDebutAvecEquipe", "EquipeId", "JoueurId" },
                keyValues: new object[] { new DateTime(2018, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 9 });

            migrationBuilder.DeleteData(
                table: "EquipeJoueur",
                keyColumns: new[] { "DateDebutAvecEquipe", "EquipeId", "JoueurId" },
                keyValues: new object[] { new DateTime(2018, 2, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 10 });

            migrationBuilder.DeleteData(
                table: "EquipeJoueur",
                keyColumns: new[] { "DateDebutAvecEquipe", "EquipeId", "JoueurId" },
                keyValues: new object[] { new DateTime(2018, 10, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 11 });

            migrationBuilder.DeleteData(
                table: "EquipeJoueur",
                keyColumns: new[] { "DateDebutAvecEquipe", "EquipeId", "JoueurId" },
                keyValues: new object[] { new DateTime(2011, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 12 });

            migrationBuilder.DeleteData(
                table: "EquipeJoueur",
                keyColumns: new[] { "DateDebutAvecEquipe", "EquipeId", "JoueurId" },
                keyValues: new object[] { new DateTime(2012, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 13 });

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "EquipeJoueur",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EquipeJoueur",
                table: "EquipeJoueur",
                column: "Id");

            migrationBuilder.InsertData(
                table: "EquipeJoueur",
                columns: new[] { "Id", "DateDebutAvecEquipe", "DateFinAvecEquipe", "EquipeId", "JoueurId", "NoDossard" },
                values: new object[,]
                {
                    { 1, new DateTime(2008, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, 1, (short)23 },
                    { 2, new DateTime(2016, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, 2, (short)24 },
                    { 3, new DateTime(2017, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, 3, (short)25 },
                    { 4, new DateTime(2013, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, 4, (short)26 },
                    { 5, new DateTime(2014, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2, 5, (short)27 },
                    { 6, new DateTime(2020, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2, 6, (short)28 },
                    { 7, new DateTime(2018, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2, 7, (short)29 },
                    { 8, new DateTime(2010, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2, 8, (short)30 },
                    { 9, new DateTime(2018, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 3, 9, (short)31 },
                    { 10, new DateTime(2018, 2, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 3, 10, (short)32 },
                    { 11, new DateTime(2018, 10, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 3, 11, (short)33 },
                    { 12, new DateTime(2011, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 4, 12, (short)34 },
                    { 13, new DateTime(2012, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 4, 13, (short)35 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EquipeJoueur_EquipeId_JoueurId_DateDebutAvecEquipe",
                table: "EquipeJoueur",
                columns: new[] { "EquipeId", "JoueurId", "DateDebutAvecEquipe" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EquipeJoueur",
                table: "EquipeJoueur");

            migrationBuilder.DropIndex(
                name: "IX_EquipeJoueur_EquipeId_JoueurId_DateDebutAvecEquipe",
                table: "EquipeJoueur");

            migrationBuilder.DeleteData(
                table: "EquipeJoueur",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "EquipeJoueur",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "EquipeJoueur",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "EquipeJoueur",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "EquipeJoueur",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "EquipeJoueur",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "EquipeJoueur",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "EquipeJoueur",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "EquipeJoueur",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "EquipeJoueur",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "EquipeJoueur",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "EquipeJoueur",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "EquipeJoueur",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 13);

            migrationBuilder.DropColumn(
                name: "Id",
                table: "EquipeJoueur");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EquipeJoueur",
                table: "EquipeJoueur",
                columns: new[] { "EquipeId", "JoueurId", "DateDebutAvecEquipe" });

            migrationBuilder.InsertData(
                table: "EquipeJoueur",
                columns: new[] { "DateDebutAvecEquipe", "EquipeId", "JoueurId", "DateFinAvecEquipe", "NoDossard" },
                values: new object[,]
                {
                    { new DateTime(2008, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, null, (short)23 },
                    { new DateTime(2016, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 2, null, (short)24 },
                    { new DateTime(2017, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 3, null, (short)25 },
                    { new DateTime(2013, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 4, null, (short)26 },
                    { new DateTime(2014, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 5, null, (short)27 },
                    { new DateTime(2020, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 6, null, (short)28 },
                    { new DateTime(2018, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 7, null, (short)29 },
                    { new DateTime(2010, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 8, null, (short)30 },
                    { new DateTime(2018, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 9, null, (short)31 },
                    { new DateTime(2018, 2, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 10, null, (short)32 },
                    { new DateTime(2018, 10, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 11, null, (short)33 },
                    { new DateTime(2011, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 12, null, (short)34 },
                    { new DateTime(2012, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 13, null, (short)35 }
                });
        }
    }
}
