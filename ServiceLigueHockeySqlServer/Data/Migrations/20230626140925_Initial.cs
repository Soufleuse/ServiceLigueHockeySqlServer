using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ServiceLigueHockeySqlServer.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Equipe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomEquipe = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Ville = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AnneeDebut = table.Column<int>(type: "int", nullable: false),
                    AnneeFin = table.Column<int>(type: "int", nullable: true),
                    EstDevenueEquipe = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipe", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Joueur",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Prenom = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DateNaissance = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VilleNaissance = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PaysOrigine = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Joueur", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EquipeJoueur",
                columns: table => new
                {
                    JoueurId = table.Column<int>(type: "int", nullable: false),
                    EquipeId = table.Column<int>(type: "int", nullable: false),
                    DateDebutAvecEquipe = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateFinAvecEquipe = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NoDossard = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipeJoueur", x => new { x.EquipeId, x.JoueurId, x.DateDebutAvecEquipe });
                    table.ForeignKey(
                        name: "FK_EquipeJoueur_Equipe_EquipeId",
                        column: x => x.EquipeId,
                        principalTable: "Equipe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquipeJoueur_Joueur_JoueurId",
                        column: x => x.JoueurId,
                        principalTable: "Joueur",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StatsJoueur",
                columns: table => new
                {
                    AnneeStats = table.Column<short>(type: "smallint", nullable: false),
                    JoueurId = table.Column<int>(type: "int", nullable: false),
                    EquipeId = table.Column<int>(type: "int", nullable: false),
                    NbPartiesJouees = table.Column<short>(type: "smallint", nullable: false),
                    NbButs = table.Column<short>(type: "smallint", nullable: false),
                    NbPasses = table.Column<short>(type: "smallint", nullable: false),
                    NbPoints = table.Column<short>(type: "smallint", nullable: false),
                    NbMinutesPenalites = table.Column<short>(type: "smallint", nullable: false),
                    PlusseMoins = table.Column<short>(type: "smallint", nullable: false),
                    Victoires = table.Column<short>(type: "smallint", nullable: false),
                    Defaites = table.Column<short>(type: "smallint", nullable: false),
                    Nulles = table.Column<short>(type: "smallint", nullable: false),
                    DefaitesEnProlongation = table.Column<short>(type: "smallint", nullable: false),
                    ButsAlloues = table.Column<int>(type: "int", nullable: false),
                    TirsAlloues = table.Column<int>(type: "int", nullable: false),
                    MinutesJouees = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatsJoueur", x => new { x.JoueurId, x.EquipeId, x.AnneeStats });
                    table.ForeignKey(
                        name: "FK_StatsJoueur_Equipe_EquipeId",
                        column: x => x.EquipeId,
                        principalTable: "Equipe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StatsJoueur_Joueur_JoueurId",
                        column: x => x.JoueurId,
                        principalTable: "Joueur",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Equipe",
                columns: new[] { "Id", "AnneeDebut", "AnneeFin", "EstDevenueEquipe", "NomEquipe", "Ville" },
                values: new object[,]
                {
                    { 1, 1989, null, null, "Canadiensssss", "Mourial" },
                    { 2, 1984, null, null, "Bruns", "Albany" },
                    { 3, 1976, null, null, "Harfangs", "Hartford" },
                    { 4, 1999, null, null, "Boulettes", "Victoriaville" },
                    { 5, 2001, null, null, "Rocher", "Percé" },
                    { 6, 1986, null, null, "Pierre", "Rochester" }
                });

            migrationBuilder.InsertData(
                table: "Joueur",
                columns: new[] { "Id", "DateNaissance", "Nom", "PaysOrigine", "Prenom", "VilleNaissance" },
                values: new object[,]
                {
                    { 1, new DateTime(1988, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tremblay", "Canada", "Jack", "Lévis" },
                    { 2, new DateTime(1996, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lajeunesse", "Canada", "Simon", "St-Stanislas" },
                    { 3, new DateTime(1995, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Grandpré", "Canada", "Mathieu", "Val d'or" },
                    { 4, new DateTime(1991, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Callahan", "Canada", "Ryan", "London" },
                    { 5, new DateTime(1992, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "McCain", "États-Unis", "Drew", "Albany" },
                    { 6, new DateTime(2000, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Harris", "États-Unis", "John", "Chico" },
                    { 7, new DateTime(1996, 12, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Rodgers", "Canada", "Phil", "Calgary" },
                    { 8, new DateTime(1992, 10, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Rodriguez", "Canada", "Ted", "Regina" },
                    { 9, new DateTime(1998, 4, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lemieux", "Canada", "Patrice", "Chibougamau" },
                    { 10, new DateTime(1997, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Béliveau", "Canada", "Maurice", "Beauceville" },
                    { 11, new DateTime(1997, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cruz", "États-Unis", "Andrew", "Dallas" },
                    { 12, new DateTime(1991, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Trout", "États-Unis", "Chris", "Eau Claire" },
                    { 13, new DateTime(1992, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Datzyuk", "États-Unis", "Sergei", "Eau Claire" }
                });

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

            migrationBuilder.InsertData(
                table: "StatsJoueur",
                columns: new[] { "AnneeStats", "EquipeId", "JoueurId", "ButsAlloues", "Defaites", "DefaitesEnProlongation", "MinutesJouees", "NbButs", "NbMinutesPenalites", "NbPartiesJouees", "NbPasses", "NbPoints", "Nulles", "PlusseMoins", "TirsAlloues", "Victoires" },
                values: new object[,]
                {
                    { (short)2018, 1, 1, 0, (short)0, (short)0, 500.0, (short)1810, (short)15, (short)65, (short)20, (short)1830, (short)0, (short)5, 0, (short)0 },
                    { (short)2019, 1, 1, 0, (short)0, (short)0, 500.0, (short)1910, (short)15, (short)82, (short)20, (short)1930, (short)0, (short)5, 0, (short)0 },
                    { (short)2020, 1, 1, 0, (short)0, (short)0, 500.0, (short)10, (short)15, (short)25, (short)20, (short)30, (short)0, (short)5, 0, (short)0 },
                    { (short)2018, 1, 2, 0, (short)0, (short)0, 500.0, (short)1815, (short)51, (short)65, (short)10, (short)1825, (short)0, (short)-2, 0, (short)0 },
                    { (short)2019, 1, 2, 0, (short)0, (short)0, 500.0, (short)1915, (short)51, (short)82, (short)10, (short)1925, (short)0, (short)-2, 0, (short)0 },
                    { (short)2020, 1, 2, 0, (short)0, (short)0, 500.0, (short)15, (short)51, (short)25, (short)10, (short)25, (short)0, (short)-2, 0, (short)0 },
                    { (short)2018, 1, 3, 0, (short)0, (short)0, 500.0, (short)1805, (short)35, (short)65, (short)24, (short)1829, (short)0, (short)25, 0, (short)0 },
                    { (short)2019, 1, 3, 0, (short)0, (short)0, 500.0, (short)1905, (short)35, (short)82, (short)24, (short)1929, (short)0, (short)25, 0, (short)0 },
                    { (short)2020, 1, 3, 0, (short)0, (short)0, 500.0, (short)5, (short)35, (short)25, (short)24, (short)29, (short)0, (short)25, 0, (short)0 },
                    { (short)2018, 1, 4, 53, (short)2, (short)6, 1500.0, (short)1800, (short)4, (short)65, (short)0, (short)1800, (short)0, (short)0, 564, (short)9 },
                    { (short)2019, 1, 4, 53, (short)2, (short)6, 1500.0, (short)1900, (short)4, (short)82, (short)0, (short)1900, (short)0, (short)0, 564, (short)9 },
                    { (short)2020, 1, 4, 53, (short)2, (short)6, 1500.0, (short)0, (short)4, (short)25, (short)0, (short)0, (short)0, (short)0, 564, (short)9 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EquipeJoueur_JoueurId",
                table: "EquipeJoueur",
                column: "JoueurId");

            migrationBuilder.CreateIndex(
                name: "IX_StatsJoueur_EquipeId",
                table: "StatsJoueur",
                column: "EquipeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EquipeJoueur");

            migrationBuilder.DropTable(
                name: "StatsJoueur");

            migrationBuilder.DropTable(
                name: "Equipe");

            migrationBuilder.DropTable(
                name: "Joueur");
        }
    }
}
