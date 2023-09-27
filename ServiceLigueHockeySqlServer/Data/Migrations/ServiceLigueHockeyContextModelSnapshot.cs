﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ServiceLigueHockeySqlServer.Data;

#nullable disable

namespace ServiceLigueHockeySqlServer.Data.Migrations
{
    [DbContext(typeof(ServiceLigueHockeyContext))]
    partial class ServiceLigueHockeyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ServiceLigueHockeySqlServer.Data.Models.EquipeBd", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AnneeDebut")
                        .HasColumnType("int");

                    b.Property<int?>("AnneeFin")
                        .HasColumnType("int");

                    b.Property<int?>("EstDevenueEquipe")
                        .HasColumnType("int");

                    b.Property<string>("NomEquipe")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Ville")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Equipe", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AnneeDebut = 1989,
                            NomEquipe = "Canadiensssss",
                            Ville = "Mourial"
                        },
                        new
                        {
                            Id = 2,
                            AnneeDebut = 1984,
                            NomEquipe = "Bruns",
                            Ville = "Albany"
                        },
                        new
                        {
                            Id = 3,
                            AnneeDebut = 1976,
                            NomEquipe = "Harfangs",
                            Ville = "Hartford"
                        },
                        new
                        {
                            Id = 4,
                            AnneeDebut = 1999,
                            NomEquipe = "Boulettes",
                            Ville = "Victoriaville"
                        },
                        new
                        {
                            Id = 5,
                            AnneeDebut = 2001,
                            NomEquipe = "Rocher",
                            Ville = "Percé"
                        },
                        new
                        {
                            Id = 6,
                            AnneeDebut = 1986,
                            NomEquipe = "Pierre",
                            Ville = "Rochester"
                        });
                });

            modelBuilder.Entity("ServiceLigueHockeySqlServer.Data.Models.EquipeJoueurBd", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateDebutAvecEquipe")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateFinAvecEquipe")
                        .HasColumnType("datetime2");

                    b.Property<int>("EquipeId")
                        .HasColumnType("int");

                    b.Property<int>("JoueurId")
                        .HasColumnType("int");

                    b.Property<short>("NoDossard")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.HasIndex("JoueurId");

                    b.HasIndex("EquipeId", "JoueurId", "DateDebutAvecEquipe")
                        .IsUnique();

                    b.ToTable("EquipeJoueur", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DateDebutAvecEquipe = new DateTime(2008, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EquipeId = 1,
                            JoueurId = 1,
                            NoDossard = (short)23
                        },
                        new
                        {
                            Id = 2,
                            DateDebutAvecEquipe = new DateTime(2016, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EquipeId = 1,
                            JoueurId = 2,
                            NoDossard = (short)24
                        },
                        new
                        {
                            Id = 3,
                            DateDebutAvecEquipe = new DateTime(2017, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EquipeId = 1,
                            JoueurId = 3,
                            NoDossard = (short)25
                        },
                        new
                        {
                            Id = 4,
                            DateDebutAvecEquipe = new DateTime(2013, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EquipeId = 1,
                            JoueurId = 4,
                            NoDossard = (short)26
                        },
                        new
                        {
                            Id = 5,
                            DateDebutAvecEquipe = new DateTime(2014, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EquipeId = 2,
                            JoueurId = 5,
                            NoDossard = (short)27
                        },
                        new
                        {
                            Id = 6,
                            DateDebutAvecEquipe = new DateTime(2020, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EquipeId = 2,
                            JoueurId = 6,
                            NoDossard = (short)28
                        },
                        new
                        {
                            Id = 7,
                            DateDebutAvecEquipe = new DateTime(2018, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EquipeId = 2,
                            JoueurId = 7,
                            NoDossard = (short)29
                        },
                        new
                        {
                            Id = 8,
                            DateDebutAvecEquipe = new DateTime(2010, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EquipeId = 2,
                            JoueurId = 8,
                            NoDossard = (short)30
                        },
                        new
                        {
                            Id = 9,
                            DateDebutAvecEquipe = new DateTime(2018, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EquipeId = 3,
                            JoueurId = 9,
                            NoDossard = (short)31
                        },
                        new
                        {
                            Id = 10,
                            DateDebutAvecEquipe = new DateTime(2018, 2, 13, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EquipeId = 3,
                            JoueurId = 10,
                            NoDossard = (short)32
                        },
                        new
                        {
                            Id = 11,
                            DateDebutAvecEquipe = new DateTime(2018, 10, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EquipeId = 3,
                            JoueurId = 11,
                            NoDossard = (short)33
                        },
                        new
                        {
                            Id = 12,
                            DateDebutAvecEquipe = new DateTime(2011, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EquipeId = 4,
                            JoueurId = 12,
                            NoDossard = (short)34
                        },
                        new
                        {
                            Id = 13,
                            DateDebutAvecEquipe = new DateTime(2012, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EquipeId = 4,
                            JoueurId = 13,
                            NoDossard = (short)35
                        });
                });

            modelBuilder.Entity("ServiceLigueHockeySqlServer.Data.Models.JoueurBd", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateNaissance")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PaysOrigine")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("VilleNaissance")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Joueur", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DateNaissance = new DateTime(1988, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Nom = "Tremblay",
                            PaysOrigine = "Canada",
                            Prenom = "Jack",
                            VilleNaissance = "Lévis"
                        },
                        new
                        {
                            Id = 2,
                            DateNaissance = new DateTime(1996, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Nom = "Lajeunesse",
                            PaysOrigine = "Canada",
                            Prenom = "Simon",
                            VilleNaissance = "St-Stanislas"
                        },
                        new
                        {
                            Id = 3,
                            DateNaissance = new DateTime(1995, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Nom = "Grandpré",
                            PaysOrigine = "Canada",
                            Prenom = "Mathieu",
                            VilleNaissance = "Val d'or"
                        },
                        new
                        {
                            Id = 4,
                            DateNaissance = new DateTime(1991, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Nom = "Callahan",
                            PaysOrigine = "Canada",
                            Prenom = "Ryan",
                            VilleNaissance = "London"
                        },
                        new
                        {
                            Id = 5,
                            DateNaissance = new DateTime(1992, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Nom = "McCain",
                            PaysOrigine = "États-Unis",
                            Prenom = "Drew",
                            VilleNaissance = "Albany"
                        },
                        new
                        {
                            Id = 6,
                            DateNaissance = new DateTime(2000, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Nom = "Harris",
                            PaysOrigine = "États-Unis",
                            Prenom = "John",
                            VilleNaissance = "Chico"
                        },
                        new
                        {
                            Id = 7,
                            DateNaissance = new DateTime(1996, 12, 21, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Nom = "Rodgers",
                            PaysOrigine = "Canada",
                            Prenom = "Phil",
                            VilleNaissance = "Calgary"
                        },
                        new
                        {
                            Id = 8,
                            DateNaissance = new DateTime(1992, 10, 21, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Nom = "Rodriguez",
                            PaysOrigine = "Canada",
                            Prenom = "Ted",
                            VilleNaissance = "Regina"
                        },
                        new
                        {
                            Id = 9,
                            DateNaissance = new DateTime(1998, 4, 21, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Nom = "Lemieux",
                            PaysOrigine = "Canada",
                            Prenom = "Patrice",
                            VilleNaissance = "Chibougamau"
                        },
                        new
                        {
                            Id = 10,
                            DateNaissance = new DateTime(1997, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Nom = "Béliveau",
                            PaysOrigine = "Canada",
                            Prenom = "Maurice",
                            VilleNaissance = "Beauceville"
                        },
                        new
                        {
                            Id = 11,
                            DateNaissance = new DateTime(1997, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Nom = "Cruz",
                            PaysOrigine = "États-Unis",
                            Prenom = "Andrew",
                            VilleNaissance = "Dallas"
                        },
                        new
                        {
                            Id = 12,
                            DateNaissance = new DateTime(1991, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Nom = "Trout",
                            PaysOrigine = "États-Unis",
                            Prenom = "Chris",
                            VilleNaissance = "Eau Claire"
                        },
                        new
                        {
                            Id = 13,
                            DateNaissance = new DateTime(1992, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Nom = "Datzyuk",
                            PaysOrigine = "États-Unis",
                            Prenom = "Sergei",
                            VilleNaissance = "Eau Claire"
                        });
                });

            modelBuilder.Entity("ServiceLigueHockeySqlServer.Data.Models.ParametresBd", b =>
                {
                    b.Property<string>("nom")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("dateDebut")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("dateFin")
                        .HasColumnType("datetime2");

                    b.Property<string>("valeur")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("nom", "dateDebut");

                    b.ToTable("Parametres", (string)null);

                    b.HasData(
                        new
                        {
                            nom = "nombrePartiesJouees",
                            dateDebut = new DateTime(2021, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            valeur = "82"
                        },
                        new
                        {
                            nom = "nombrePartiesJouees",
                            dateDebut = new DateTime(2020, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            dateFin = new DateTime(2021, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            valeur = "56"
                        },
                        new
                        {
                            nom = "nombrePartiesJouees",
                            dateDebut = new DateTime(2019, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            dateFin = new DateTime(2020, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            valeur = "71"
                        },
                        new
                        {
                            nom = "nombrePartiesJouees",
                            dateDebut = new DateTime(2013, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            dateFin = new DateTime(2019, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            valeur = "82"
                        },
                        new
                        {
                            nom = "nombrePartiesJouees",
                            dateDebut = new DateTime(2012, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            dateFin = new DateTime(2013, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            valeur = "48"
                        },
                        new
                        {
                            nom = "nombrePartiesJouees",
                            dateDebut = new DateTime(2005, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            dateFin = new DateTime(2012, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            valeur = "82"
                        },
                        new
                        {
                            nom = "nombrePartiesJouees",
                            dateDebut = new DateTime(2004, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            dateFin = new DateTime(2005, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            valeur = "0"
                        },
                        new
                        {
                            nom = "nombrePartiesJouees",
                            dateDebut = new DateTime(1995, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            dateFin = new DateTime(2004, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            valeur = "82"
                        },
                        new
                        {
                            nom = "AjoutSteve",
                            dateDebut = new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            valeur = "ma valeur"
                        });
                });

            modelBuilder.Entity("ServiceLigueHockeySqlServer.Data.Models.StatsEquipeBd", b =>
                {
                    b.Property<int>("EquipeId")
                        .HasColumnType("int");

                    b.Property<short>("AnneeStats")
                        .HasColumnType("smallint");

                    b.Property<short>("NbButsContre")
                        .HasColumnType("smallint");

                    b.Property<short>("NbButsPour")
                        .HasColumnType("smallint");

                    b.Property<short>("NbDefProlo")
                        .HasColumnType("smallint");

                    b.Property<short>("NbDefaites")
                        .HasColumnType("smallint");

                    b.Property<short>("NbPartiesJouees")
                        .HasColumnType("smallint");

                    b.Property<short>("NbVictoires")
                        .HasColumnType("smallint");

                    b.HasKey("EquipeId", "AnneeStats");

                    b.ToTable("StatsEquipe", (string)null);

                    b.HasData(
                        new
                        {
                            EquipeId = 1,
                            AnneeStats = (short)2023,
                            NbButsContre = (short)199,
                            NbButsPour = (short)499,
                            NbDefProlo = (short)12,
                            NbDefaites = (short)10,
                            NbPartiesJouees = (short)82,
                            NbVictoires = (short)60
                        });
                });

            modelBuilder.Entity("ServiceLigueHockeySqlServer.Data.Models.StatsJoueurBd", b =>
                {
                    b.Property<int>("JoueurId")
                        .HasColumnType("int");

                    b.Property<int>("EquipeId")
                        .HasColumnType("int");

                    b.Property<short>("AnneeStats")
                        .HasColumnType("smallint");

                    b.Property<int>("ButsAlloues")
                        .HasColumnType("int");

                    b.Property<short>("Defaites")
                        .HasColumnType("smallint");

                    b.Property<short>("DefaitesEnProlongation")
                        .HasColumnType("smallint");

                    b.Property<double>("MinutesJouees")
                        .HasColumnType("float");

                    b.Property<short>("NbButs")
                        .HasColumnType("smallint");

                    b.Property<short>("NbMinutesPenalites")
                        .HasColumnType("smallint");

                    b.Property<short>("NbPartiesJouees")
                        .HasColumnType("smallint");

                    b.Property<short>("NbPasses")
                        .HasColumnType("smallint");

                    b.Property<short>("NbPoints")
                        .HasColumnType("smallint");

                    b.Property<short>("Nulles")
                        .HasColumnType("smallint");

                    b.Property<short>("PlusseMoins")
                        .HasColumnType("smallint");

                    b.Property<int>("TirsAlloues")
                        .HasColumnType("int");

                    b.Property<short>("Victoires")
                        .HasColumnType("smallint");

                    b.HasKey("JoueurId", "EquipeId", "AnneeStats");

                    b.HasIndex("EquipeId");

                    b.ToTable("StatsJoueur", (string)null);

                    b.HasData(
                        new
                        {
                            JoueurId = 1,
                            EquipeId = 1,
                            AnneeStats = (short)2020,
                            ButsAlloues = 0,
                            Defaites = (short)0,
                            DefaitesEnProlongation = (short)0,
                            MinutesJouees = 500.0,
                            NbButs = (short)10,
                            NbMinutesPenalites = (short)15,
                            NbPartiesJouees = (short)25,
                            NbPasses = (short)20,
                            NbPoints = (short)30,
                            Nulles = (short)0,
                            PlusseMoins = (short)5,
                            TirsAlloues = 0,
                            Victoires = (short)0
                        },
                        new
                        {
                            JoueurId = 2,
                            EquipeId = 1,
                            AnneeStats = (short)2020,
                            ButsAlloues = 0,
                            Defaites = (short)0,
                            DefaitesEnProlongation = (short)0,
                            MinutesJouees = 500.0,
                            NbButs = (short)15,
                            NbMinutesPenalites = (short)51,
                            NbPartiesJouees = (short)25,
                            NbPasses = (short)10,
                            NbPoints = (short)25,
                            Nulles = (short)0,
                            PlusseMoins = (short)-2,
                            TirsAlloues = 0,
                            Victoires = (short)0
                        },
                        new
                        {
                            JoueurId = 3,
                            EquipeId = 1,
                            AnneeStats = (short)2020,
                            ButsAlloues = 0,
                            Defaites = (short)0,
                            DefaitesEnProlongation = (short)0,
                            MinutesJouees = 500.0,
                            NbButs = (short)5,
                            NbMinutesPenalites = (short)35,
                            NbPartiesJouees = (short)25,
                            NbPasses = (short)24,
                            NbPoints = (short)29,
                            Nulles = (short)0,
                            PlusseMoins = (short)25,
                            TirsAlloues = 0,
                            Victoires = (short)0
                        },
                        new
                        {
                            JoueurId = 4,
                            EquipeId = 1,
                            AnneeStats = (short)2020,
                            ButsAlloues = 53,
                            Defaites = (short)2,
                            DefaitesEnProlongation = (short)6,
                            MinutesJouees = 1500.0,
                            NbButs = (short)0,
                            NbMinutesPenalites = (short)4,
                            NbPartiesJouees = (short)25,
                            NbPasses = (short)0,
                            NbPoints = (short)0,
                            Nulles = (short)0,
                            PlusseMoins = (short)0,
                            TirsAlloues = 564,
                            Victoires = (short)9
                        },
                        new
                        {
                            JoueurId = 1,
                            EquipeId = 1,
                            AnneeStats = (short)2019,
                            ButsAlloues = 0,
                            Defaites = (short)0,
                            DefaitesEnProlongation = (short)0,
                            MinutesJouees = 500.0,
                            NbButs = (short)1910,
                            NbMinutesPenalites = (short)15,
                            NbPartiesJouees = (short)82,
                            NbPasses = (short)20,
                            NbPoints = (short)1930,
                            Nulles = (short)0,
                            PlusseMoins = (short)5,
                            TirsAlloues = 0,
                            Victoires = (short)0
                        },
                        new
                        {
                            JoueurId = 2,
                            EquipeId = 1,
                            AnneeStats = (short)2019,
                            ButsAlloues = 0,
                            Defaites = (short)0,
                            DefaitesEnProlongation = (short)0,
                            MinutesJouees = 500.0,
                            NbButs = (short)1915,
                            NbMinutesPenalites = (short)51,
                            NbPartiesJouees = (short)82,
                            NbPasses = (short)10,
                            NbPoints = (short)1925,
                            Nulles = (short)0,
                            PlusseMoins = (short)-2,
                            TirsAlloues = 0,
                            Victoires = (short)0
                        },
                        new
                        {
                            JoueurId = 3,
                            EquipeId = 1,
                            AnneeStats = (short)2019,
                            ButsAlloues = 0,
                            Defaites = (short)0,
                            DefaitesEnProlongation = (short)0,
                            MinutesJouees = 500.0,
                            NbButs = (short)1905,
                            NbMinutesPenalites = (short)35,
                            NbPartiesJouees = (short)82,
                            NbPasses = (short)24,
                            NbPoints = (short)1929,
                            Nulles = (short)0,
                            PlusseMoins = (short)25,
                            TirsAlloues = 0,
                            Victoires = (short)0
                        },
                        new
                        {
                            JoueurId = 4,
                            EquipeId = 1,
                            AnneeStats = (short)2019,
                            ButsAlloues = 53,
                            Defaites = (short)2,
                            DefaitesEnProlongation = (short)6,
                            MinutesJouees = 1500.0,
                            NbButs = (short)1900,
                            NbMinutesPenalites = (short)4,
                            NbPartiesJouees = (short)82,
                            NbPasses = (short)0,
                            NbPoints = (short)1900,
                            Nulles = (short)0,
                            PlusseMoins = (short)0,
                            TirsAlloues = 564,
                            Victoires = (short)9
                        },
                        new
                        {
                            JoueurId = 1,
                            EquipeId = 1,
                            AnneeStats = (short)2018,
                            ButsAlloues = 0,
                            Defaites = (short)0,
                            DefaitesEnProlongation = (short)0,
                            MinutesJouees = 500.0,
                            NbButs = (short)1810,
                            NbMinutesPenalites = (short)15,
                            NbPartiesJouees = (short)65,
                            NbPasses = (short)20,
                            NbPoints = (short)1830,
                            Nulles = (short)0,
                            PlusseMoins = (short)5,
                            TirsAlloues = 0,
                            Victoires = (short)0
                        },
                        new
                        {
                            JoueurId = 2,
                            EquipeId = 1,
                            AnneeStats = (short)2018,
                            ButsAlloues = 0,
                            Defaites = (short)0,
                            DefaitesEnProlongation = (short)0,
                            MinutesJouees = 500.0,
                            NbButs = (short)1815,
                            NbMinutesPenalites = (short)51,
                            NbPartiesJouees = (short)65,
                            NbPasses = (short)10,
                            NbPoints = (short)1825,
                            Nulles = (short)0,
                            PlusseMoins = (short)-2,
                            TirsAlloues = 0,
                            Victoires = (short)0
                        },
                        new
                        {
                            JoueurId = 3,
                            EquipeId = 1,
                            AnneeStats = (short)2018,
                            ButsAlloues = 0,
                            Defaites = (short)0,
                            DefaitesEnProlongation = (short)0,
                            MinutesJouees = 500.0,
                            NbButs = (short)1805,
                            NbMinutesPenalites = (short)35,
                            NbPartiesJouees = (short)65,
                            NbPasses = (short)24,
                            NbPoints = (short)1829,
                            Nulles = (short)0,
                            PlusseMoins = (short)25,
                            TirsAlloues = 0,
                            Victoires = (short)0
                        },
                        new
                        {
                            JoueurId = 4,
                            EquipeId = 1,
                            AnneeStats = (short)2018,
                            ButsAlloues = 53,
                            Defaites = (short)2,
                            DefaitesEnProlongation = (short)6,
                            MinutesJouees = 1500.0,
                            NbButs = (short)1800,
                            NbMinutesPenalites = (short)4,
                            NbPartiesJouees = (short)65,
                            NbPasses = (short)0,
                            NbPoints = (short)1800,
                            Nulles = (short)0,
                            PlusseMoins = (short)0,
                            TirsAlloues = 564,
                            Victoires = (short)9
                        });
                });

            modelBuilder.Entity("ServiceLigueHockeySqlServer.Data.Models.EquipeJoueurBd", b =>
                {
                    b.HasOne("ServiceLigueHockeySqlServer.Data.Models.EquipeBd", "Equipe")
                        .WithMany("listeEquipeJoueur")
                        .HasForeignKey("EquipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ServiceLigueHockeySqlServer.Data.Models.JoueurBd", "Joueur")
                        .WithMany("listeEquipeJoueur")
                        .HasForeignKey("JoueurId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Equipe");

                    b.Navigation("Joueur");
                });

            modelBuilder.Entity("ServiceLigueHockeySqlServer.Data.Models.StatsEquipeBd", b =>
                {
                    b.HasOne("ServiceLigueHockeySqlServer.Data.Models.EquipeBd", "Equipe")
                        .WithMany("listeStatsEquipe")
                        .HasForeignKey("EquipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Equipe");
                });

            modelBuilder.Entity("ServiceLigueHockeySqlServer.Data.Models.StatsJoueurBd", b =>
                {
                    b.HasOne("ServiceLigueHockeySqlServer.Data.Models.EquipeBd", "Equipe")
                        .WithMany()
                        .HasForeignKey("EquipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ServiceLigueHockeySqlServer.Data.Models.JoueurBd", "Joueur")
                        .WithMany("listeStatsJoueur")
                        .HasForeignKey("JoueurId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Equipe");

                    b.Navigation("Joueur");
                });

            modelBuilder.Entity("ServiceLigueHockeySqlServer.Data.Models.EquipeBd", b =>
                {
                    b.Navigation("listeEquipeJoueur");

                    b.Navigation("listeStatsEquipe");
                });

            modelBuilder.Entity("ServiceLigueHockeySqlServer.Data.Models.JoueurBd", b =>
                {
                    b.Navigation("listeEquipeJoueur");

                    b.Navigation("listeStatsJoueur");
                });
#pragma warning restore 612, 618
        }
    }
}
