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

        public DbSet<EquipeBd> equipe { get; set; } = default!;

        public DbSet<AlignementBd> alignements { get; set; } = default!;

        public DbSet<JoueurBd> joueur { get; set; } = default!;

        public DbSet<EquipeJoueurBd> equipeJoueur { get; set; } = default!;

        public DbSet<StatsJoueurBd> statsJoueurBd { get; set; } = default!;

        public DbSet<StatsEquipeBd> statsEquipe { get; set; } = default!;

        public DbSet<ParametresBd> parametres { get; set; } = default!;

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);
            
            /*configurationBuilder.Properties<DateTime>()
                .HaveConversion<DateTimeConverter>()
                .HaveColumnType("date");*/
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
            modelBuilder.Entity<EquipeBd>().ToTable("Equipe");
            modelBuilder.Entity<EquipeBd>().HasKey(c => c.Id);
            modelBuilder.Entity<EquipeBd>().Property(x => x.Id).IsRequired();
            modelBuilder.Entity<EquipeBd>().Property(x => x.NomEquipe).HasMaxLength(50);
            modelBuilder.Entity<EquipeBd>().Property(x => x.Ville).HasMaxLength(50);
            modelBuilder.Entity<EquipeBd>().HasMany("listeEquipeJoueur").WithOne("Equipe");
            modelBuilder.Entity<EquipeBd>().HasMany("listeAlignement").WithOne("equipe");

            modelBuilder.Entity<JoueurBd>().ToTable("Joueur");
            modelBuilder.Entity<JoueurBd>().HasKey(c => c.Id);
            modelBuilder.Entity<JoueurBd>().Property(c => c.Id).IsRequired();
            modelBuilder.Entity<JoueurBd>().Property(c => c.Prenom).HasMaxLength(50);
            modelBuilder.Entity<JoueurBd>().Property(c => c.Nom).HasMaxLength(50);
            modelBuilder.Entity<JoueurBd>().Property(c => c.VilleNaissance).HasMaxLength(50);
            modelBuilder.Entity<JoueurBd>().Property(c => c.PaysOrigine).HasMaxLength(50);
            modelBuilder.Entity<JoueurBd>().HasMany("listeEquipeJoueur").WithOne("Joueur");
            modelBuilder.Entity<JoueurBd>().HasMany("listeAlignement").WithOne("joueur");

            modelBuilder.Entity<EquipeJoueurBd>().ToTable("EquipeJoueur");
            modelBuilder.Entity<EquipeJoueurBd>()
                .HasKey(o => new { o.EquipeId, o.JoueurId, o.DateDebutAvecEquipe });
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
            
            modelBuilder.Entity<AlignementBd>().ToTable("Alignement");
            modelBuilder.Entity<AlignementBd>()
                .HasKey(o => new { o.Id });
            modelBuilder.Entity<AlignementBd>()
                .HasOne("equipe")
                .WithMany("listeAlignement")
                .HasForeignKey("EquipeId");
            modelBuilder.Entity<AlignementBd>()
                .HasOne("joueur")
                .WithMany("listeAlignement")
                .HasForeignKey("JoueurId");

            modelBuilder.Entity<ParametresBd>().ToTable("Parametres");
            modelBuilder.Entity<ParametresBd>().HasKey(c => new { c.nom, c.dateDebut });
            modelBuilder.Entity<ParametresBd>().Property(c => c.nom).IsRequired();
            modelBuilder.Entity<ParametresBd>().Property(c => c.valeur).IsRequired().HasMaxLength(200);
            modelBuilder.Entity<ParametresBd>().Property(c => c.dateDebut).IsRequired();
            modelBuilder.Entity<ParametresBd>().Property(c => c.dateFin);

            // Ajout de données
            modelBuilder.Entity<EquipeBd>()
                .HasData(
                new EquipeBd { Id = 1, NomEquipe = "Canadiensssss", Ville = "Mourial", AnneeDebut = 1989 },
                new EquipeBd { Id = 2, NomEquipe = "Bruns", Ville = "Albany", AnneeDebut = 1984 },
                new EquipeBd { Id = 3, NomEquipe = "Harfangs", Ville = "Hartford", AnneeDebut = 1976 },
                new EquipeBd { Id = 4, NomEquipe = "Boulettes", Ville = "Victoriaville", AnneeDebut = 1999 },
                new EquipeBd { Id = 5, NomEquipe = "Rocher", Ville = "Percé", AnneeDebut = 2001 },
                new EquipeBd { Id = 6, NomEquipe = "Pierre", Ville = "Rochester", AnneeDebut = 1986 }
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
                new EquipeJoueurBd { EquipeId = 1, JoueurId = 1, NoDossard = 23, DateDebutAvecEquipe = new DateTime(2008, 9, 30), DateFinAvecEquipe = null },
                new EquipeJoueurBd { EquipeId = 1, JoueurId = 2, NoDossard = 24, DateDebutAvecEquipe = new DateTime(2016, 9, 30), DateFinAvecEquipe = null },
                new EquipeJoueurBd { EquipeId = 1, JoueurId = 3, NoDossard = 25, DateDebutAvecEquipe = new DateTime(2017, 9, 30), DateFinAvecEquipe = null },
                new EquipeJoueurBd { EquipeId = 1, JoueurId = 4, NoDossard = 26, DateDebutAvecEquipe = new DateTime(2013, 9, 30), DateFinAvecEquipe = null },
                new EquipeJoueurBd { EquipeId = 2, JoueurId = 5, NoDossard = 27, DateDebutAvecEquipe = new DateTime(2014, 9, 30), DateFinAvecEquipe = null },
                new EquipeJoueurBd { EquipeId = 2, JoueurId = 6, NoDossard = 28, DateDebutAvecEquipe = new DateTime(2020, 11, 30), DateFinAvecEquipe = null },
                new EquipeJoueurBd { EquipeId = 2, JoueurId = 7, NoDossard = 29, DateDebutAvecEquipe = new DateTime(2018, 1, 15), DateFinAvecEquipe = null },
                new EquipeJoueurBd { EquipeId = 2, JoueurId = 8, NoDossard = 30, DateDebutAvecEquipe = new DateTime(2010, 9, 30), DateFinAvecEquipe = null },
                new EquipeJoueurBd { EquipeId = 3, JoueurId = 9, NoDossard = 31, DateDebutAvecEquipe = new DateTime(2018, 4, 20), DateFinAvecEquipe = null },
                new EquipeJoueurBd { EquipeId = 3, JoueurId = 10, NoDossard = 32, DateDebutAvecEquipe = new DateTime(2018, 2, 13), DateFinAvecEquipe = null },
                new EquipeJoueurBd { EquipeId = 3, JoueurId = 11, NoDossard = 33, DateDebutAvecEquipe = new DateTime(2018, 10, 30), DateFinAvecEquipe = null },
                new EquipeJoueurBd { EquipeId = 4, JoueurId = 12, NoDossard = 34, DateDebutAvecEquipe = new DateTime(2011, 9, 10), DateFinAvecEquipe = null },
                new EquipeJoueurBd { EquipeId = 4, JoueurId = 13, NoDossard = 35, DateDebutAvecEquipe = new DateTime(2012, 8, 20), DateFinAvecEquipe = null }
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
            
            modelBuilder.Entity<AlignementBd>()
                .HasData(
                    new AlignementBd { Id = 1, EquipeId = 1, JoueurId = 1, DateDebut = new DateTime(2016, 9, 3), DateFin = null}
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
        }
    }

    /// <summary>
    /// Convertit <see cref="DateTime" /> à <see cref="DateTime"/> et vice versa.
    /// </summary>
    /*public class DateTimeConverter : ValueConverter<DateTime, DateTime>
    {
        /// <summary>
        /// Creates a new instance of this converter.
        /// </summary>
        public DateTimeConverter() : base(
                d => d.ToDateTime(TimeOnly.MinValue),
                d => DateTime.FromDateTime(d))
        { }
    }*/
}
