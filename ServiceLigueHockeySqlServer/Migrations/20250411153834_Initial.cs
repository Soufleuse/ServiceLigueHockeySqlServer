using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ServiceLigueHockeySqlServer.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnneeStats",
                columns: table => new
                {
                    AnneeStats = table.Column<short>(type: "smallint", nullable: false),
                    DescnCourte = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DescnLongue = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnneeStats", x => x.AnneeStats);
                });

            migrationBuilder.CreateTable(
                name: "Equipe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
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
                    Id = table.Column<int>(type: "int", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "TypePenalites",
                columns: table => new
                {
                    IdTypePenalite = table.Column<short>(type: "smallint", nullable: false),
                    NbreMinutesPenalitesPourCetteInfraction = table.Column<int>(type: "int", nullable: false),
                    DescriptionPenalite = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypePenalites", x => x.IdTypePenalite);
                });

            migrationBuilder.CreateTable(
                name: "Calendrier",
                columns: table => new
                {
                    IdPartie = table.Column<int>(type: "int", nullable: false),
                    DatePartieJouee = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AnneeStats = table.Column<short>(type: "smallint", nullable: false),
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
                        name: "FK_Calendrier_AnneeStats_AnneeStats",
                        column: x => x.AnneeStats,
                        principalTable: "AnneeStats",
                        principalColumn: "AnneeStats");
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
                name: "StatsEquipe",
                columns: table => new
                {
                    AnneeStats = table.Column<short>(type: "smallint", nullable: false),
                    EquipeId = table.Column<int>(type: "int", nullable: false),
                    NbPartiesJouees = table.Column<short>(type: "smallint", nullable: false),
                    NbVictoires = table.Column<short>(type: "smallint", nullable: false),
                    NbDefaites = table.Column<short>(type: "smallint", nullable: false),
                    NbDefProlo = table.Column<short>(type: "smallint", nullable: false),
                    NbButsPour = table.Column<short>(type: "smallint", nullable: false),
                    NbButsContre = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatsEquipe", x => new { x.EquipeId, x.AnneeStats });
                    table.ForeignKey(
                        name: "FK_StatsEquipe_Equipe_EquipeId",
                        column: x => x.EquipeId,
                        principalTable: "Equipe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EquipeJoueur",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JoueurId = table.Column<int>(type: "int", nullable: false),
                    EquipeId = table.Column<int>(type: "int", nullable: false),
                    DateDebutAvecEquipe = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateFinAvecEquipe = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NoDossard = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipeJoueur", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EquipeJoueur_Equipe_EquipeId",
                        column: x => x.EquipeId,
                        principalTable: "Equipe",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EquipeJoueur_Joueur_JoueurId",
                        column: x => x.JoueurId,
                        principalTable: "Joueur",
                        principalColumn: "Id");
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

            migrationBuilder.CreateTable(
                name: "FeuillePointage",
                columns: table => new
                {
                    MomentDuButMarque = table.Column<TimeSpan>(type: "time", nullable: false),
                    IdPartie = table.Column<int>(type: "int", nullable: false),
                    IdJoueurButMarque = table.Column<int>(type: "int", nullable: false),
                    IdJoueurPremiereAssistance = table.Column<int>(type: "int", nullable: true),
                    IdJoueurSecondeAssistance = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeuillePointage", x => new { x.IdPartie, x.MomentDuButMarque });
                    table.ForeignKey(
                        name: "FK_FeuillePointage_Calendrier_IdPartie",
                        column: x => x.IdPartie,
                        principalTable: "Calendrier",
                        principalColumn: "IdPartie");
                });

            migrationBuilder.CreateTable(
                name: "Penalites",
                columns: table => new
                {
                    MomentDelaPenalite = table.Column<TimeSpan>(type: "time", nullable: false),
                    IdPartie = table.Column<int>(type: "int", nullable: false),
                    IdJoueurPenalise = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Penalites", x => new { x.MomentDelaPenalite, x.IdPartie });
                    table.ForeignKey(
                        name: "FK_Penalites_Calendrier_IdPartie",
                        column: x => x.IdPartie,
                        principalTable: "Calendrier",
                        principalColumn: "IdPartie");
                    table.ForeignKey(
                        name: "FK_Penalites_Joueur_IdJoueurPenalise",
                        column: x => x.IdJoueurPenalise,
                        principalTable: "Joueur",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Penalite_TypePenalite",
                columns: table => new
                {
                    IdPenalite = table.Column<int>(type: "int", nullable: false),
                    IdTypePenalite = table.Column<short>(type: "smallint", nullable: false),
                    MomentDelaPenalite = table.Column<TimeSpan>(type: "time", nullable: false),
                    IdJoueurPenalise = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Penalite_TypePenalite", x => x.IdPenalite);
                    table.ForeignKey(
                        name: "FK_Penalite_TypePenalite_Penalites_MomentDelaPenalite_IdJoueurPenalise",
                        columns: x => new { x.MomentDelaPenalite, x.IdJoueurPenalise },
                        principalTable: "Penalites",
                        principalColumns: new[] { "MomentDelaPenalite", "IdPartie" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Penalite_TypePenalite_TypePenalites_IdTypePenalite",
                        column: x => x.IdTypePenalite,
                        principalTable: "TypePenalites",
                        principalColumn: "IdTypePenalite",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AnneeStats",
                columns: new[] { "AnneeStats", "DescnCourte", "DescnLongue" },
                values: new object[,]
                {
                    { (short)2017, "2017/2018", "Représente la saison 2017/2018" },
                    { (short)2018, "2018/2019", "Représente la saison 2018/2019" },
                    { (short)2019, "2019/2020", "Représente la saison 2019/2020" },
                    { (short)2020, "2020/2021", "Représente la saison 2020/2021" },
                    { (short)2021, "2021/2022", "Représente la saison 2021/2022" },
                    { (short)2022, "2022/2023", "Représente la saison 2022/2023" },
                    { (short)2023, "2023/2024", "Représente la saison 2023/2024" },
                    { (short)2024, "2024/2025", "Représente la saison 2024/2025" }
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
                table: "Parametres",
                columns: new[] { "dateDebut", "nom", "dateFin", "valeur" },
                values: new object[,]
                {
                    { new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AjoutSteve", null, "ma valeur" },
                    { new DateTime(1995, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "nombrePartiesJouees", new DateTime(2004, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "82" },
                    { new DateTime(2004, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "nombrePartiesJouees", new DateTime(2005, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "0" },
                    { new DateTime(2005, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "nombrePartiesJouees", new DateTime(2012, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "82" },
                    { new DateTime(2012, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "nombrePartiesJouees", new DateTime(2013, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "48" },
                    { new DateTime(2013, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "nombrePartiesJouees", new DateTime(2019, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "82" },
                    { new DateTime(2019, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "nombrePartiesJouees", new DateTime(2020, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "71" },
                    { new DateTime(2020, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "nombrePartiesJouees", new DateTime(2021, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "56" },
                    { new DateTime(2021, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "nombrePartiesJouees", null, "82" }
                });

            migrationBuilder.InsertData(
                table: "TypePenalites",
                columns: new[] { "IdTypePenalite", "DescriptionPenalite", "NbreMinutesPenalitesPourCetteInfraction" },
                values: new object[,]
                {
                    { (short)1, "Mineure", 2 },
                    { (short)2, "Majeure", 5 },
                    { (short)3, "Inconduite de partie", 10 }
                });

            migrationBuilder.InsertData(
                table: "Calendrier",
                columns: new[] { "IdPartie", "AFiniEnProlongation", "AFiniEnTirDeBarrage", "AnneeStats", "DatePartieJouee", "EstUnePartieDeSerie", "EstUnePartiePresaison", "EstUnePartieSaisonReguliere", "IdEquipeHote", "IdEquipeVisiteuse", "NbreButsComptesParHote", "NbreButsComptesParVisiteur", "SommairePartie" },
                values: new object[] { 1, null, null, (short)2024, new DateTime(2024, 10, 5, 20, 0, 0, 0, DateTimeKind.Unspecified), "N", "N", "O", 1, 2, null, null, "" });

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

            migrationBuilder.InsertData(
                table: "StatsEquipe",
                columns: new[] { "AnneeStats", "EquipeId", "NbButsContre", "NbButsPour", "NbDefProlo", "NbDefaites", "NbPartiesJouees", "NbVictoires" },
                values: new object[] { (short)2023, 1, (short)199, (short)499, (short)12, (short)10, (short)82, (short)60 });

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
                name: "IX_Calendrier_AnneeStats",
                table: "Calendrier",
                column: "AnneeStats");

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
                name: "IX_EquipeJoueur_EquipeId_JoueurId_DateDebutAvecEquipe",
                table: "EquipeJoueur",
                columns: new[] { "EquipeId", "JoueurId", "DateDebutAvecEquipe" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EquipeJoueur_JoueurId",
                table: "EquipeJoueur",
                column: "JoueurId");

            migrationBuilder.CreateIndex(
                name: "IX_Penalite_TypePenalite_IdTypePenalite",
                table: "Penalite_TypePenalite",
                column: "IdTypePenalite");

            migrationBuilder.CreateIndex(
                name: "IX_Penalite_TypePenalite_MomentDelaPenalite_IdJoueurPenalise",
                table: "Penalite_TypePenalite",
                columns: new[] { "MomentDelaPenalite", "IdJoueurPenalise" });

            migrationBuilder.CreateIndex(
                name: "IX_Penalites_IdJoueurPenalise",
                table: "Penalites",
                column: "IdJoueurPenalise");

            migrationBuilder.CreateIndex(
                name: "IX_Penalites_IdPartie",
                table: "Penalites",
                column: "IdPartie");

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
                name: "FeuillePointage");

            migrationBuilder.DropTable(
                name: "Parametres");

            migrationBuilder.DropTable(
                name: "Penalite_TypePenalite");

            migrationBuilder.DropTable(
                name: "StatsEquipe");

            migrationBuilder.DropTable(
                name: "StatsJoueur");

            migrationBuilder.DropTable(
                name: "Penalites");

            migrationBuilder.DropTable(
                name: "TypePenalites");

            migrationBuilder.DropTable(
                name: "Calendrier");

            migrationBuilder.DropTable(
                name: "Joueur");

            migrationBuilder.DropTable(
                name: "AnneeStats");

            migrationBuilder.DropTable(
                name: "Equipe");
        }
    }
}
