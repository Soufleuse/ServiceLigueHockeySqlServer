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
                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new System.Exception("La chaine de connexion est vide.");
                }

                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ServiceLigueHockeyContext).Assembly);
        }
    }
}
