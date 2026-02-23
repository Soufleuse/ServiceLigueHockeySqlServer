using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceLigueHockeySqlServer.Data.Models;

namespace ServiceLigueHockeySqlServer.Data.Configuration
{
    public class StatsJoueurConfiguration : IEntityTypeConfiguration<StatsJoueurBd>
    {
        public void Configure(EntityTypeBuilder<StatsJoueurBd> builder)
        {
            // Création des tables
            builder.ToTable("StatsJoueur");
            builder.HasKey(o => new { o.JoueurId, o.EquipeId, o.AnneeStats });
            builder.Property(c => c.AnneeStats).IsRequired();
            builder.HasOne("Joueur")
                   .WithMany("listeStatsJoueur")
                   .HasForeignKey("JoueurId");
            
            // Ajout de données
            builder.HasData(
                    new StatsJoueurBd { EquipeId = 1, JoueurId = 1, AnneeStats = 2023, NbButs = 10, NbPasses = 20, NbPoints = 30, NbPartiesJouees = 25, NbMinutesPenalites = 15, PlusseMoins = 5, ButsAlloues = 0, TirsAlloues = 0, Victoires = 0, Defaites = 0, DefaitesEnProlongation = 0, Nulles = 0, MinutesJouees = 500 },
                    new StatsJoueurBd { EquipeId = 1, JoueurId = 2, AnneeStats = 2023, NbButs = 15, NbPasses = 10, NbPoints = 25, NbPartiesJouees = 25, NbMinutesPenalites = 51, PlusseMoins = -2, ButsAlloues = 0, TirsAlloues = 0, Victoires = 0, Defaites = 0, DefaitesEnProlongation = 0, Nulles = 0, MinutesJouees = 500 },
                    new StatsJoueurBd { EquipeId = 1, JoueurId = 3, AnneeStats = 2023, NbButs = 5, NbPasses = 24, NbPoints = 29, NbPartiesJouees = 25, NbMinutesPenalites = 35, PlusseMoins = 25, ButsAlloues = 0, TirsAlloues = 0, Victoires = 0, Defaites = 0, DefaitesEnProlongation = 0, Nulles = 0, MinutesJouees = 500 },
                    new StatsJoueurBd { EquipeId = 1, JoueurId = 4, AnneeStats = 2023, NbButs = 0, NbPasses = 0, NbPoints = 0, NbPartiesJouees = 25, NbMinutesPenalites = 4, PlusseMoins = 0, ButsAlloues = 53, TirsAlloues = 564, Victoires = 9, Defaites = 2, DefaitesEnProlongation = 6, Nulles = 0, MinutesJouees = 1500 },
                    new StatsJoueurBd { EquipeId = 1, JoueurId = 1, AnneeStats = 2022, NbButs = 10, NbPasses = 20, NbPoints = 30, NbPartiesJouees = 25, NbMinutesPenalites = 15, PlusseMoins = 5, ButsAlloues = 0, TirsAlloues = 0, Victoires = 0, Defaites = 0, DefaitesEnProlongation = 0, Nulles = 0, MinutesJouees = 500 },
                    new StatsJoueurBd { EquipeId = 1, JoueurId = 2, AnneeStats = 2022, NbButs = 15, NbPasses = 10, NbPoints = 25, NbPartiesJouees = 25, NbMinutesPenalites = 51, PlusseMoins = -2, ButsAlloues = 0, TirsAlloues = 0, Victoires = 0, Defaites = 0, DefaitesEnProlongation = 0, Nulles = 0, MinutesJouees = 500 },
                    new StatsJoueurBd { EquipeId = 1, JoueurId = 3, AnneeStats = 2022, NbButs = 5, NbPasses = 24, NbPoints = 29, NbPartiesJouees = 25, NbMinutesPenalites = 35, PlusseMoins = 25, ButsAlloues = 0, TirsAlloues = 0, Victoires = 0, Defaites = 0, DefaitesEnProlongation = 0, Nulles = 0, MinutesJouees = 500 },
                    new StatsJoueurBd { EquipeId = 1, JoueurId = 4, AnneeStats = 2022, NbButs = 0, NbPasses = 0, NbPoints = 0, NbPartiesJouees = 25, NbMinutesPenalites = 4, PlusseMoins = 0, ButsAlloues = 53, TirsAlloues = 564, Victoires = 9, Defaites = 2, DefaitesEnProlongation = 6, Nulles = 0, MinutesJouees = 1500 },
                    new StatsJoueurBd { EquipeId = 1, JoueurId = 1, AnneeStats = 2021, NbButs = 10, NbPasses = 20, NbPoints = 30, NbPartiesJouees = 25, NbMinutesPenalites = 15, PlusseMoins = 5, ButsAlloues = 0, TirsAlloues = 0, Victoires = 0, Defaites = 0, DefaitesEnProlongation = 0, Nulles = 0, MinutesJouees = 500 },
                    new StatsJoueurBd { EquipeId = 1, JoueurId = 2, AnneeStats = 2021, NbButs = 15, NbPasses = 10, NbPoints = 25, NbPartiesJouees = 25, NbMinutesPenalites = 51, PlusseMoins = -2, ButsAlloues = 0, TirsAlloues = 0, Victoires = 0, Defaites = 0, DefaitesEnProlongation = 0, Nulles = 0, MinutesJouees = 500 },
                    new StatsJoueurBd { EquipeId = 1, JoueurId = 3, AnneeStats = 2021, NbButs = 5, NbPasses = 24, NbPoints = 29, NbPartiesJouees = 25, NbMinutesPenalites = 35, PlusseMoins = 25, ButsAlloues = 0, TirsAlloues = 0, Victoires = 0, Defaites = 0, DefaitesEnProlongation = 0, Nulles = 0, MinutesJouees = 500 },
                    new StatsJoueurBd { EquipeId = 1, JoueurId = 4, AnneeStats = 2021, NbButs = 0, NbPasses = 0, NbPoints = 0, NbPartiesJouees = 25, NbMinutesPenalites = 4, PlusseMoins = 0, ButsAlloues = 53, TirsAlloues = 564, Victoires = 9, Defaites = 2, DefaitesEnProlongation = 6, Nulles = 0, MinutesJouees = 1500 },
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
        }
    }
}