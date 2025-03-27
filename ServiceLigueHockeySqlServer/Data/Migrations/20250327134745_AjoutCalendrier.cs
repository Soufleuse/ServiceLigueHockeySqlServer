using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceLigueHockeySqlServer.Data.Migrations
{
    /// <inheritdoc />
    public partial class AjoutCalendrier : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EquipeJoueur_Equipe_EquipeId",
                table: "EquipeJoueur");

            migrationBuilder.DropForeignKey(
                name: "FK_EquipeJoueur_Joueur_JoueurId",
                table: "EquipeJoueur");

            migrationBuilder.CreateTable(
                name: "Calendrier",
                columns: table => new
                {
                    IdPartie = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatePartieJouee = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NbreButsComptesParHote = table.Column<short>(type: "smallint", nullable: true),
                    NbreButsComptesParVisiteur = table.Column<short>(type: "smallint", nullable: true),
                    AFiniEnProlongation = table.Column<string>(type: "nvarchar(1)", nullable: true),
                    AFiniEnTirDeBarrage = table.Column<string>(type: "nvarchar(1)", nullable: true),
                    EstUnePartieDeSerie = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    EstUnePartiePresaison = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    EstUnePartieSaisonReguliere = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    SommairePartie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdEquipeHote = table.Column<int>(type: "int", nullable: false),
                    IdEquipeVisiteuse = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calendrier", x => x.IdPartie);
                    table.ForeignKey(
                        name: "FK_Calendrier_Equipe_IdEquipeHote",
                        column: x => x.IdEquipeHote,
                        principalTable: "Equipe",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Calendrier_Equipe_IdEquipeVisiteuse",
                        column: x => x.IdEquipeVisiteuse,
                        principalTable: "Equipe",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FeuillePointage",
                columns: table => new
                {
                    MomentDuButMarque = table.Column<TimeSpan>(type: "time", nullable: false),
                    IdPartie = table.Column<int>(type: "int", nullable: false),
                    IdJoueurButMarque = table.Column<int>(type: "int", nullable: false),
                    IdJoueurPremiereAssistance = table.Column<int>(type: "int", nullable: true),
                    IdJoueurSecondeAssistance = table.Column<int>(type: "int", nullable: true),
                    MonCalendrierIdPartie = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeuillePointage", x => new { x.IdPartie, x.MomentDuButMarque });
                    table.ForeignKey(
                        name: "FK_FeuillePointage_Calendrier_MonCalendrierIdPartie",
                        column: x => x.MonCalendrierIdPartie,
                        principalTable: "Calendrier",
                        principalColumn: "IdPartie");
                });

            migrationBuilder.CreateTable(
                name: "Penalites",
                columns: table => new
                {
                    MomentDelaPenalite = table.Column<TimeSpan>(type: "time", nullable: false),
                    IdPartie = table.Column<int>(type: "int", nullable: false),
                    IdJoueurPenalise = table.Column<int>(type: "int", nullable: false),
                    joueurPenaliseId = table.Column<int>(type: "int", nullable: false),
                    MonCalendrierIdPartie = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Penalites", x => new { x.IdPartie, x.MomentDelaPenalite });
                    table.ForeignKey(
                        name: "FK_Penalites_Calendrier_MonCalendrierIdPartie",
                        column: x => x.MonCalendrierIdPartie,
                        principalTable: "Calendrier",
                        principalColumn: "IdPartie");
                    table.ForeignKey(
                        name: "FK_Penalites_Joueur_joueurPenaliseId",
                        column: x => x.joueurPenaliseId,
                        principalTable: "Joueur",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TypePenalites",
                columns: table => new
                {
                    IdTypePenalite = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NbreMinutesPenalitesPourCetteInfraction = table.Column<int>(type: "int", nullable: false),
                    PenalitesBdIdPartie = table.Column<int>(type: "int", nullable: true),
                    PenalitesBdMomentDelaPenalite = table.Column<TimeSpan>(type: "time", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypePenalites", x => x.IdTypePenalite);
                    table.ForeignKey(
                        name: "FK_TypePenalites_Penalites_PenalitesBdIdPartie_PenalitesBdMomentDelaPenalite",
                        columns: x => new { x.PenalitesBdIdPartie, x.PenalitesBdMomentDelaPenalite },
                        principalTable: "Penalites",
                        principalColumns: new[] { "IdPartie", "MomentDelaPenalite" });
                });

            migrationBuilder.InsertData(
                table: "Calendrier",
                columns: new[] { "IdPartie", "AFiniEnProlongation", "AFiniEnTirDeBarrage", "DatePartieJouee", "EstUnePartieDeSerie", "EstUnePartiePresaison", "EstUnePartieSaisonReguliere", "IdEquipeHote", "IdEquipeVisiteuse", "NbreButsComptesParHote", "NbreButsComptesParVisiteur", "SommairePartie" },
                values: new object[] { 1, null, null, new DateTime(2024, 10, 5, 20, 0, 0, 0, DateTimeKind.Unspecified), "N", "N", "O", 1, 2, null, null, "" });

            migrationBuilder.CreateIndex(
                name: "IX_Calendrier_IdEquipeHote_IdEquipeVisiteuse_DatePartieJouee",
                table: "Calendrier",
                columns: new[] { "IdEquipeHote", "IdEquipeVisiteuse", "DatePartieJouee" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Calendrier_IdEquipeVisiteuse",
                table: "Calendrier",
                column: "IdEquipeVisiteuse");

            migrationBuilder.CreateIndex(
                name: "IX_FeuillePointage_MonCalendrierIdPartie",
                table: "FeuillePointage",
                column: "MonCalendrierIdPartie");

            migrationBuilder.CreateIndex(
                name: "IX_Penalites_joueurPenaliseId",
                table: "Penalites",
                column: "joueurPenaliseId");

            migrationBuilder.CreateIndex(
                name: "IX_Penalites_MonCalendrierIdPartie",
                table: "Penalites",
                column: "MonCalendrierIdPartie");

            migrationBuilder.CreateIndex(
                name: "IX_TypePenalites_PenalitesBdIdPartie_PenalitesBdMomentDelaPenalite",
                table: "TypePenalites",
                columns: new[] { "PenalitesBdIdPartie", "PenalitesBdMomentDelaPenalite" });

            migrationBuilder.AddForeignKey(
                name: "FK_EquipeJoueur_Equipe_EquipeId",
                table: "EquipeJoueur",
                column: "EquipeId",
                principalTable: "Equipe",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EquipeJoueur_Joueur_JoueurId",
                table: "EquipeJoueur",
                column: "JoueurId",
                principalTable: "Joueur",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EquipeJoueur_Equipe_EquipeId",
                table: "EquipeJoueur");

            migrationBuilder.DropForeignKey(
                name: "FK_EquipeJoueur_Joueur_JoueurId",
                table: "EquipeJoueur");

            migrationBuilder.DropTable(
                name: "FeuillePointage");

            migrationBuilder.DropTable(
                name: "TypePenalites");

            migrationBuilder.DropTable(
                name: "Penalites");

            migrationBuilder.DropTable(
                name: "Calendrier");

            migrationBuilder.AddForeignKey(
                name: "FK_EquipeJoueur_Equipe_EquipeId",
                table: "EquipeJoueur",
                column: "EquipeId",
                principalTable: "Equipe",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EquipeJoueur_Joueur_JoueurId",
                table: "EquipeJoueur",
                column: "JoueurId",
                principalTable: "Joueur",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
