using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using ServiceLigueHockeySqlServer.Data.Models;

namespace ServiceLigueHockeySqlServer.Data
{
    public class ServiceLigueHockeyContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public ServiceLigueHockeyContext(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public DbSet<ConferenceBd> conference { get; set; } = default!;

        public DbSet<DivisionBd> division { get; set; } = default!;

        public DbSet<EquipeBd> equipe { get; set; } = default!;

        public DbSet<JoueurBd> joueur { get; set; } = default!;

        public DbSet<EquipeJoueurBd> equipeJoueur { get; set; } = default!;

        public DbSet<StatsJoueurBd> statsJoueurBd { get; set; } = default!;

        public DbSet<StatsEquipeBd> statsEquipe { get; set; } = default!;

        public DbSet<ParametresBd> parametres { get; set; } = default!;

        public DbSet<TypePenalitesBd> typePenalites { get; set; } = default!;

        public DbSet<CalendrierBd> calendriers { get; set; } = default!;

        public DbSet<PenalitesBd> penalites { get; set; } = default!;

        public DbSet<PointeursBd> pointeurs { get; set; } = default!;

        public DbSet<Penalite_TypePenaliteBd> penalite_TypePenalites { get; set; } = default!;

        public DbSet<AnneeStatsBd> anneeStats { get; set; } = default!;

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = this.Configuration.GetConnectionString("sqlServerConnection");
                if(string.IsNullOrEmpty(connectionString))
                    throw new System.Exception("La chaine de connexion est vide.");

                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Création des tables
            modelBuilder.Entity<AnneeStatsBd>().ToTable("AnneeStats");
            modelBuilder.Entity<AnneeStatsBd>().HasKey(s => s.AnneeStats);
            modelBuilder.Entity<AnneeStatsBd>().Property(e => e.AnneeStats).ValueGeneratedNever();
            modelBuilder.Entity<AnneeStatsBd>().HasMany(d => d.ListeParties)
                                               .WithOne(d => d.zeAnnee);

            modelBuilder.Entity<ConferenceBd>().ToTable("Conference");
            modelBuilder.Entity<ConferenceBd>().HasKey(c => c.Id);
            modelBuilder.Entity<ConferenceBd>().Property(c => c.Id).ValueGeneratedNever();
            modelBuilder.Entity<ConferenceBd>().Property(x => x.NomConference).HasMaxLength(25);
            modelBuilder.Entity<ConferenceBd>().HasIndex(x => new { x.NomConference, x.AnneeDebut }).IsUnique();

            modelBuilder.Entity<DivisionBd>().ToTable("Division");
            modelBuilder.Entity<DivisionBd>().HasKey(x => x.Id);
            modelBuilder.Entity<DivisionBd>().Property(x => x.Id).ValueGeneratedNever();
            modelBuilder.Entity<DivisionBd>().Property(x => x.NomDivision).HasMaxLength(25);
            modelBuilder.Entity<DivisionBd>().HasIndex(x => new { x.NomDivision, x.AnneeDebut }).IsUnique();
            modelBuilder.Entity<DivisionBd>().HasOne(x => x.Conference).WithMany(y => y.listeDivision).HasForeignKey(d => d.ConferenceId);
            /*modelBuilder.Entity<DivisionBd>().HasOne(x => x.Conference)
                                             .WithMany(y => y.listeDivision)
                                             .HasForeignKey(z => z.Conference);*/

            modelBuilder.Entity<EquipeBd>().ToTable("Equipe");
            modelBuilder.Entity<EquipeBd>().HasKey(c => c.Id);
            modelBuilder.Entity<EquipeBd>().Property(x => x.Id).IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<EquipeBd>().Property(x => x.NomEquipe).HasMaxLength(50);
            modelBuilder.Entity<EquipeBd>().Property(x => x.Ville).HasMaxLength(50);
            modelBuilder.Entity<EquipeBd>().HasIndex(x => new { x.NomEquipe, x.Ville }).IsUnique();
            modelBuilder.Entity<EquipeBd>().HasOne(x => x.division)
                                           .WithMany(y => y.listeEquipeBd);
            modelBuilder.Entity<EquipeBd>().HasMany("listeEquipeJoueur")
                                           .WithOne("Equipe")
                                           .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<JoueurBd>().ToTable("Joueur");
            modelBuilder.Entity<JoueurBd>().HasKey(c => c.Id);
            modelBuilder.Entity<JoueurBd>().Property(c => c.Id).IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<JoueurBd>().Property(c => c.Prenom).HasMaxLength(50);
            modelBuilder.Entity<JoueurBd>().Property(c => c.Nom).HasMaxLength(50);
            modelBuilder.Entity<JoueurBd>().Property(c => c.VilleNaissance).HasMaxLength(50);
            modelBuilder.Entity<JoueurBd>().Property(c => c.PaysOrigine).HasMaxLength(50);
            modelBuilder.Entity<JoueurBd>().HasMany("listeEquipeJoueur")
                                           .WithOne("Joueur")
                                           .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<JoueurBd>().HasMany("listePenalites")
                                           .WithOne("joueurPenalise")
                                           .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<EquipeJoueurBd>().ToTable("EquipeJoueur");
            modelBuilder.Entity<EquipeJoueurBd>()
                .HasKey(o => new { o.Id });
            modelBuilder.Entity<EquipeJoueurBd>()
                .HasIndex(d => new { d.EquipeId, d.JoueurId, d.DateDebutAvecEquipe })
                .IsUnique();
            modelBuilder.Entity<EquipeJoueurBd>()
                .HasOne(p => p.Joueur)
                .WithMany(p => p.listeEquipeJoueur)
                .HasForeignKey(p => p.JoueurId );
            modelBuilder.Entity<EquipeJoueurBd>()
                .HasOne("Equipe")
                .WithMany("listeEquipeJoueur")
                .HasForeignKey("EquipeId");

            modelBuilder.Entity<StatsJoueurBd>().ToTable("StatsJoueur");
            modelBuilder.Entity<StatsJoueurBd>()
                .HasKey(o => new { o.JoueurId, o.EquipeId, o.AnneeStats });
            modelBuilder.Entity<StatsJoueurBd>().Property(c => c.AnneeStats).IsRequired();
            modelBuilder.Entity<StatsJoueurBd>()
                .HasOne("Joueur")
                .WithMany("listeStatsJoueur")
                .HasForeignKey("JoueurId");

            modelBuilder.Entity<StatsEquipeBd>().ToTable("StatsEquipe");
            modelBuilder.Entity<StatsEquipeBd>()
                .HasKey(o => new { o.EquipeId, o.AnneeStats });
            modelBuilder.Entity<StatsEquipeBd>().Property(c => c.AnneeStats).IsRequired();
            modelBuilder.Entity<StatsEquipeBd>()
                .HasOne("Equipe")
                .WithMany("listeStatsEquipe")
                .HasForeignKey("EquipeId");

            modelBuilder.Entity<ParametresBd>().ToTable("Parametres");
            modelBuilder.Entity<ParametresBd>().HasKey(c => new { c.nom, c.dateDebut });
            modelBuilder.Entity<ParametresBd>().Property(c => c.nom).IsRequired();
            modelBuilder.Entity<ParametresBd>().Property(c => c.valeur).IsRequired().HasMaxLength(200);
            modelBuilder.Entity<ParametresBd>().Property(c => c.dateDebut).IsRequired();
            modelBuilder.Entity<ParametresBd>().Property(c => c.dateFin);

            modelBuilder.Entity<TypePenalitesBd>().ToTable("TypePenalites");
            modelBuilder.Entity<TypePenalitesBd>().HasKey(p => p.IdTypePenalite);

            modelBuilder.Entity<CalendrierBd>().ToTable("Calendrier");
            modelBuilder.Entity<CalendrierBd>().HasKey("IdPartie");
            modelBuilder.Entity<CalendrierBd>().Property(e => e.IdPartie).ValueGeneratedNever();
            modelBuilder.Entity<CalendrierBd>().HasOne("zeAnnee")
                                               .WithMany("ListeParties")
                                               .HasForeignKey("AnneeStats")
                                               .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<CalendrierBd>().HasIndex(u => new { u.IdEquipeHote, u.IdEquipeVisiteuse, u.DatePartieJouee })
                                               .IsUnique();
            modelBuilder.Entity<CalendrierBd>().HasOne("EquipeHote")
                                               .WithMany("listeEquipeHote")
                                               .HasForeignKey("IdEquipeHote")
                                               .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<CalendrierBd>().HasOne("EquipeVisiteuse")
                                               .WithMany("listeEquipeVisiteur")
                                               .HasForeignKey("IdEquipeVisiteuse")
                                               .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<CalendrierBd>().HasMany("listePointeurs")
                                               .WithOne("MonCalendrier")
                                               .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<CalendrierBd>().HasMany("listePenalites")
                                               .WithOne("MonCalendrier")
                                               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PointeursBd>().ToTable("FeuillePointage");
            modelBuilder.Entity<PointeursBd>().HasKey(u => new { u.IdPartie, u.MomentDuButMarque });
            modelBuilder.Entity<PointeursBd>().HasOne("MonCalendrier")
                                              .WithMany("listePointeurs")
                                              .HasForeignKey("IdPartie");

            modelBuilder.Entity<TypePenalitesBd>().ToTable("TypePenalites");
            modelBuilder.Entity<TypePenalitesBd>().HasKey(g => g.IdTypePenalite);
            modelBuilder.Entity<TypePenalitesBd>().Property(e => e.IdTypePenalite).ValueGeneratedNever();

            modelBuilder.Entity<Penalite_TypePenaliteBd>().ToTable("Penalite_TypePenalite");
            modelBuilder.Entity<Penalite_TypePenaliteBd>().HasKey("IdPenalite");
            modelBuilder.Entity<Penalite_TypePenaliteBd>().Property(e => e.IdPenalite).ValueGeneratedNever();
            modelBuilder.Entity<Penalite_TypePenaliteBd>().HasOne("typePenalitesParent")
                                                          .WithMany("listePenTypePen")
                                                          .HasForeignKey("IdTypePenalite");
            modelBuilder.Entity<Penalite_TypePenaliteBd>().HasOne("penaliteParent")
                                                          .WithMany("listePenalites")
                                                          .HasForeignKey("MomentDelaPenalite", "IdJoueurPenalise");

            modelBuilder.Entity<PenalitesBd>().ToTable("Penalites");
            modelBuilder.Entity<PenalitesBd>().HasKey("MomentDelaPenalite", "IdPartie");
            modelBuilder.Entity<PenalitesBd>().HasOne("MonCalendrier")
                                              .WithMany("listePenalites")
                                              .HasForeignKey("IdPartie");
            modelBuilder.Entity<PenalitesBd>().HasOne("joueurPenalise")
                                              .WithMany("listePenalites")
                                              .HasForeignKey("IdJoueurPenalise");

            // Ajout de données
            modelBuilder.Entity<ConferenceBd>().HasData(
                new ConferenceBd { Id = 1, NomConference = "Est", AnneeDebut = 1994 },
                new ConferenceBd { Id = 2, NomConference = "Ouest", AnneeDebut = 1994 }
            );

            modelBuilder.Entity<DivisionBd>().HasData(
                new DivisionBd { Id = 1, ConferenceId = 1, NomDivision = "Atlantique", AnneeDebut = 1994 },
                new DivisionBd { Id = 2, ConferenceId = 1, NomDivision = "Métropolitaine", AnneeDebut = 1994 },
                new DivisionBd { Id = 3, ConferenceId = 2, NomDivision = "Centrale", AnneeDebut = 1994 },
                new DivisionBd { Id = 4, ConferenceId = 2, NomDivision = "Pacifique", AnneeDebut = 1994 }
            );

            modelBuilder.Entity<EquipeBd>()
                .HasData(
                new EquipeBd { Id = 1, NomEquipe = "Canadiensssss", Ville = "Mourial", AnneeDebut = 1989, DivisionId = 1 },
                new EquipeBd { Id = 2, NomEquipe = "Bruns", Ville = "Albany", AnneeDebut = 1984, DivisionId = 1 },
                new EquipeBd { Id = 3, NomEquipe = "Harfangs", Ville = "Hartford", AnneeDebut = 1976, DivisionId = 1 },
                new EquipeBd { Id = 4, NomEquipe = "Boulettes", Ville = "Victoriaville", AnneeDebut = 1999, DivisionId = 1 },
                new EquipeBd { Id = 5, NomEquipe = "Rocher", Ville = "Percé", AnneeDebut = 2001, DivisionId = 1 },
                new EquipeBd { Id = 6, NomEquipe = "Pierre", Ville = "Rochester", AnneeDebut = 1986, DivisionId = 1 }
                );

            modelBuilder.Entity<JoueurBd>()
                .HasData(
                new JoueurBd { Id = 1, Prenom = "Jack", Nom = "Tremblay", DateNaissance = new DateTime(1988, 10, 20), VilleNaissance = "Lévis", PaysOrigine = "Canada" },
                new JoueurBd { Id = 2, Prenom = "Simon", Nom = "Lajeunesse", DateNaissance = new DateTime(1996, 1, 25), VilleNaissance = "St-Stanislas", PaysOrigine = "Canada" },
                new JoueurBd { Id = 3, Prenom = "Mathieu", Nom = "Grandpré", DateNaissance = new DateTime(1995, 3, 5), VilleNaissance = "Val d\'or", PaysOrigine = "Canada" },
                new JoueurBd { Id = 4, Prenom = "Ryan", Nom = "Callahan", DateNaissance = new DateTime(1991, 3, 15), VilleNaissance = "London", PaysOrigine = "Canada" },
                new JoueurBd { Id = 5, Prenom = "Drew", Nom = "McCain", DateNaissance = new DateTime(1992, 6, 18), VilleNaissance = "Albany", PaysOrigine = "États-Unis" },
                new JoueurBd { Id = 6, Prenom = "John", Nom = "Harris", DateNaissance = new DateTime(2000, 9, 10), VilleNaissance = "Chico", PaysOrigine = "États-Unis" },
                new JoueurBd { Id = 7, Prenom = "Phil", Nom = "Rodgers", DateNaissance = new DateTime(1996, 12, 21), VilleNaissance = "Calgary", PaysOrigine = "Canada" },
                new JoueurBd { Id = 8, Prenom = "Ted", Nom = "Rodriguez", DateNaissance = new DateTime(1992, 10, 21), VilleNaissance = "Regina", PaysOrigine = "Canada" },
                new JoueurBd { Id = 9, Prenom = "Patrice", Nom = "Lemieux", DateNaissance = new DateTime(1998, 4, 21), VilleNaissance = "Chibougamau", PaysOrigine = "Canada" },
                new JoueurBd { Id = 10, Prenom = "Maurice", Nom = "Béliveau", DateNaissance = new DateTime(1997, 6, 1), VilleNaissance = "Beauceville", PaysOrigine = "Canada" },
                new JoueurBd { Id = 11, Prenom = "Andrew", Nom = "Cruz", DateNaissance = new DateTime(1997, 7, 30), VilleNaissance = "Dallas", PaysOrigine = "États-Unis" },
                new JoueurBd { Id = 12, Prenom = "Chris", Nom = "Trout", DateNaissance = new DateTime(1991, 8, 20), VilleNaissance = "Eau Claire", PaysOrigine = "États-Unis" },
                new JoueurBd { Id = 13, Prenom = "Sergei", Nom = "Datzyuk", DateNaissance = new DateTime(1992, 9, 6), VilleNaissance = "Eau Claire", PaysOrigine = "États-Unis" }
                );

            modelBuilder.Entity<EquipeJoueurBd>()
                .HasData(
                new EquipeJoueurBd { Id = 1, EquipeId = 1, JoueurId = 1, NoDossard = 23, DateDebutAvecEquipe = new DateTime(2008, 9, 30), DateFinAvecEquipe = null },
                new EquipeJoueurBd { Id = 2, EquipeId = 1, JoueurId = 2, NoDossard = 24, DateDebutAvecEquipe = new DateTime(2016, 9, 30), DateFinAvecEquipe = null },
                new EquipeJoueurBd { Id = 3, EquipeId = 1, JoueurId = 3, NoDossard = 25, DateDebutAvecEquipe = new DateTime(2017, 9, 30), DateFinAvecEquipe = null },
                new EquipeJoueurBd { Id = 4, EquipeId = 1, JoueurId = 4, NoDossard = 26, DateDebutAvecEquipe = new DateTime(2013, 9, 30), DateFinAvecEquipe = null },
                new EquipeJoueurBd { Id = 5, EquipeId = 2, JoueurId = 5, NoDossard = 27, DateDebutAvecEquipe = new DateTime(2014, 9, 30), DateFinAvecEquipe = null },
                new EquipeJoueurBd { Id = 6, EquipeId = 2, JoueurId = 6, NoDossard = 28, DateDebutAvecEquipe = new DateTime(2020, 11, 30), DateFinAvecEquipe = null },
                new EquipeJoueurBd { Id = 7, EquipeId = 2, JoueurId = 7, NoDossard = 29, DateDebutAvecEquipe = new DateTime(2018, 1, 15), DateFinAvecEquipe = null },
                new EquipeJoueurBd { Id = 8, EquipeId = 2, JoueurId = 8, NoDossard = 30, DateDebutAvecEquipe = new DateTime(2010, 9, 30), DateFinAvecEquipe = null },
                new EquipeJoueurBd { Id = 9, EquipeId = 3, JoueurId = 9, NoDossard = 31, DateDebutAvecEquipe = new DateTime(2018, 4, 20), DateFinAvecEquipe = null },
                new EquipeJoueurBd { Id = 10, EquipeId = 3, JoueurId = 10, NoDossard = 32, DateDebutAvecEquipe = new DateTime(2018, 2, 13), DateFinAvecEquipe = null },
                new EquipeJoueurBd { Id = 11, EquipeId = 3, JoueurId = 11, NoDossard = 33, DateDebutAvecEquipe = new DateTime(2018, 10, 30), DateFinAvecEquipe = null },
                new EquipeJoueurBd { Id = 12, EquipeId = 4, JoueurId = 12, NoDossard = 34, DateDebutAvecEquipe = new DateTime(2011, 9, 10), DateFinAvecEquipe = null },
                new EquipeJoueurBd { Id = 13, EquipeId = 4, JoueurId = 13, NoDossard = 35, DateDebutAvecEquipe = new DateTime(2012, 8, 20), DateFinAvecEquipe = null }
                );

            modelBuilder.Entity<StatsJoueurBd>()
                .HasData(
                new StatsJoueurBd { EquipeId = 1, JoueurId = 1, AnneeStats = 2020, NbButs = 10, NbPasses = 20, NbPoints = 30, NbPartiesJouees = 25, NbMinutesPenalites = 15, PlusseMoins = 5, ButsAlloues = 0, TirsAlloues = 0, Victoires = 0, Defaites = 0, DefaitesEnProlongation = 0, Nulles = 0, MinutesJouees = 500 },
                new StatsJoueurBd { EquipeId = 1, JoueurId = 2, AnneeStats = 2020, NbButs = 15, NbPasses = 10, NbPoints = 25, NbPartiesJouees = 25, NbMinutesPenalites = 51, PlusseMoins = -2, ButsAlloues = 0, TirsAlloues = 0, Victoires = 0, Defaites = 0, DefaitesEnProlongation = 0, Nulles = 0, MinutesJouees = 500 },
                new StatsJoueurBd { EquipeId = 1, JoueurId = 3, AnneeStats = 2020, NbButs = 5, NbPasses = 24, NbPoints = 29, NbPartiesJouees = 25, NbMinutesPenalites = 35, PlusseMoins = 25, ButsAlloues = 0, TirsAlloues = 0, Victoires = 0, Defaites = 0, DefaitesEnProlongation = 0, Nulles = 0, MinutesJouees = 500 },
                new StatsJoueurBd { EquipeId = 1, JoueurId = 4, AnneeStats = 2020, NbButs = 0, NbPasses = 0, NbPoints = 0, NbPartiesJouees = 25, NbMinutesPenalites = 4, PlusseMoins = 0, ButsAlloues = 53, TirsAlloues = 564, Victoires = 9, Defaites = 2, DefaitesEnProlongation = 6, Nulles = 0, MinutesJouees = 1500 },
                new StatsJoueurBd { EquipeId = 1, JoueurId = 1, AnneeStats = 2019, NbButs = 1910, NbPasses = 20, NbPoints = 1930, NbPartiesJouees = 82, NbMinutesPenalites = 15, PlusseMoins = 5, ButsAlloues = 0, TirsAlloues = 0, Victoires = 0, Defaites = 0, DefaitesEnProlongation = 0, Nulles = 0, MinutesJouees = 500 },
                new StatsJoueurBd { EquipeId = 1, JoueurId = 2, AnneeStats = 2019, NbButs = 1915, NbPasses = 10, NbPoints = 1925, NbPartiesJouees = 82, NbMinutesPenalites = 51, PlusseMoins = -2, ButsAlloues = 0, TirsAlloues = 0, Victoires = 0, Defaites = 0, DefaitesEnProlongation = 0, Nulles = 0, MinutesJouees = 500 },
                new StatsJoueurBd { EquipeId = 1, JoueurId = 3, AnneeStats = 2019, NbButs = 1905, NbPasses = 24, NbPoints = 1929, NbPartiesJouees = 82, NbMinutesPenalites = 35, PlusseMoins = 25, ButsAlloues = 0, TirsAlloues = 0, Victoires = 0, Defaites = 0, DefaitesEnProlongation = 0, Nulles = 0, MinutesJouees = 500 },
                new StatsJoueurBd { EquipeId = 1, JoueurId = 4, AnneeStats = 2019, NbButs = 1900, NbPasses = 0, NbPoints = 1900, NbPartiesJouees = 82, NbMinutesPenalites = 4, PlusseMoins = 0, ButsAlloues = 53, TirsAlloues = 564, Victoires = 9, Defaites = 2, DefaitesEnProlongation = 6, Nulles = 0, MinutesJouees = 1500 },
                new StatsJoueurBd { EquipeId = 1, JoueurId = 1, AnneeStats = 2018, NbButs = 1810, NbPasses = 20, NbPoints = 1830, NbPartiesJouees = 65, NbMinutesPenalites = 15, PlusseMoins = 5, ButsAlloues = 0, TirsAlloues = 0, Victoires = 0, Defaites = 0, DefaitesEnProlongation = 0, Nulles = 0, MinutesJouees = 500 },
                new StatsJoueurBd { EquipeId = 1, JoueurId = 2, AnneeStats = 2018, NbButs = 1815, NbPasses = 10, NbPoints = 1825, NbPartiesJouees = 65, NbMinutesPenalites = 51, PlusseMoins = -2, ButsAlloues = 0, TirsAlloues = 0, Victoires = 0, Defaites = 0, DefaitesEnProlongation = 0, Nulles = 0, MinutesJouees = 500 },
                new StatsJoueurBd { EquipeId = 1, JoueurId = 3, AnneeStats = 2018, NbButs = 1805, NbPasses = 24, NbPoints = 1829, NbPartiesJouees = 65, NbMinutesPenalites = 35, PlusseMoins = 25, ButsAlloues = 0, TirsAlloues = 0, Victoires = 0, Defaites = 0, DefaitesEnProlongation = 0, Nulles = 0, MinutesJouees = 500 },
                new StatsJoueurBd { EquipeId = 1, JoueurId = 4, AnneeStats = 2018, NbButs = 1800, NbPasses = 0, NbPoints = 1800, NbPartiesJouees = 65, NbMinutesPenalites = 4, PlusseMoins = 0, ButsAlloues = 53, TirsAlloues = 564, Victoires = 9, Defaites = 2, DefaitesEnProlongation = 6, Nulles = 0, MinutesJouees = 1500 }
                );

            modelBuilder.Entity<StatsEquipeBd>()
                .HasData(
                    new StatsEquipeBd { AnneeStats = 2023, EquipeId = 1, NbPartiesJouees = 82, NbVictoires = 60, NbDefaites = 10, NbDefProlo = 12, NbButsPour = 499, NbButsContre = 199 }
                );

            modelBuilder.Entity<ParametresBd>()
                .HasData(
                    new ParametresBd { nom = "nombrePartiesJouees", valeur = "82", dateDebut = new DateTime(2021, 8, 1), dateFin = null },
                    new ParametresBd { nom = "nombrePartiesJouees", valeur = "56", dateDebut = new DateTime(2020, 8, 1), dateFin = new DateTime(2021, 7, 31)},
                    new ParametresBd { nom = "nombrePartiesJouees", valeur = "71", dateDebut = new DateTime(2019, 8, 1), dateFin = new DateTime(2020, 7, 31)},
                    new ParametresBd { nom = "nombrePartiesJouees", valeur = "82", dateDebut = new DateTime(2013, 8, 1), dateFin = new DateTime(2019, 7, 31)},
                    new ParametresBd { nom = "nombrePartiesJouees", valeur = "48", dateDebut = new DateTime(2012, 8, 1), dateFin = new DateTime(2013, 7, 31)},
                    new ParametresBd { nom = "nombrePartiesJouees", valeur = "82", dateDebut = new DateTime(2005, 8, 1), dateFin = new DateTime(2012, 7, 31)},
                    new ParametresBd { nom = "nombrePartiesJouees", valeur = "0", dateDebut = new DateTime(2004, 8, 1), dateFin = new DateTime(2005, 7, 31)},
                    new ParametresBd { nom = "nombrePartiesJouees", valeur = "82", dateDebut = new DateTime(1995, 8, 1), dateFin = new DateTime(2004, 7, 31)},
                    new ParametresBd { nom = "AjoutSteve", valeur = "ma valeur", dateDebut = new DateTime(2020, 1 ,1), dateFin = null }
                );
            
            modelBuilder.Entity<AnneeStatsBd>()
                .HasData(
                    new AnneeStatsBd { AnneeStats = 2024, DescnCourte = "2024/2025", DescnLongue = "Représente la saison 2024/2025" },
                    new AnneeStatsBd { AnneeStats = 2023, DescnCourte = "2023/2024", DescnLongue = "Représente la saison 2023/2024" },
                    new AnneeStatsBd { AnneeStats = 2022, DescnCourte = "2022/2023", DescnLongue = "Représente la saison 2022/2023" },
                    new AnneeStatsBd { AnneeStats = 2021, DescnCourte = "2021/2022", DescnLongue = "Représente la saison 2021/2022" },
                    new AnneeStatsBd { AnneeStats = 2020, DescnCourte = "2020/2021", DescnLongue = "Représente la saison 2020/2021" },
                    new AnneeStatsBd { AnneeStats = 2019, DescnCourte = "2019/2020", DescnLongue = "Représente la saison 2019/2020" },
                    new AnneeStatsBd { AnneeStats = 2018, DescnCourte = "2018/2019", DescnLongue = "Représente la saison 2018/2019" },
                    new AnneeStatsBd { AnneeStats = 2017, DescnCourte = "2017/2018", DescnLongue = "Représente la saison 2017/2018" }
                );

            modelBuilder.Entity<CalendrierBd>()
                .HasData(
                    new CalendrierBd { IdPartie = 1, IdEquipeHote = 1, IdEquipeVisiteuse = 2, AnneeStats = 2024, DatePartieJouee = new DateTime(2024, 10, 5, 20, 0, 0)}
                );

            modelBuilder.Entity<TypePenalitesBd>()
                .HasData(
                    new TypePenalitesBd { IdTypePenalite = 1, DescriptionPenalite = "Mineure", NbreMinutesPenalitesPourCetteInfraction = 2 },
                    new TypePenalitesBd { IdTypePenalite = 2, DescriptionPenalite = "Majeure", NbreMinutesPenalitesPourCetteInfraction = 5 },
                    new TypePenalitesBd { IdTypePenalite = 3, DescriptionPenalite = "Inconduite de partie", NbreMinutesPenalitesPourCetteInfraction = 10 }
                );
        }
    }
}
