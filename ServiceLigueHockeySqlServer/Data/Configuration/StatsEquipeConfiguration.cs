using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceLigueHockeySqlServer.Data.Models;

namespace ServiceLigueHockeySqlServer.Data.Configuration
{
    public class StatsEquipeConfiguration : IEntityTypeConfiguration<StatsEquipeBd>
    {
        public void Configure(EntityTypeBuilder<StatsEquipeBd> builder)
        {
            builder.ToTable("StatsEquipe");
            builder.HasKey(o => new {o.EquipeId, o.AnneeStats});
            builder.Property(c => c.AnneeStats).IsRequired();
            builder.HasOne("Equipe")
                   .WithMany("listeStatsEquipe")
                   .HasForeignKey("EquipeId");

            builder.HasData(
                    new StatsEquipeBd { EquipeId = 1, AnneeStats = 2023, NbPartiesJouees = 82, NbVictoires = 50, NbDefaites = 20, NbDefProlo = 12, NbButsPour = 380, NbButsContre = 267 },
                    new StatsEquipeBd { EquipeId = 2, AnneeStats = 2023, NbPartiesJouees = 82, NbVictoires = 45, NbDefaites = 26, NbDefProlo = 11, NbButsPour = 315, NbButsContre = 287 },
                    new StatsEquipeBd { EquipeId = 3, AnneeStats = 2023, NbPartiesJouees = 82, NbVictoires = 44, NbDefaites = 30, NbDefProlo = 8, NbButsPour = 300, NbButsContre = 307 },
                    new StatsEquipeBd { EquipeId = 4, AnneeStats = 2023, NbPartiesJouees = 82, NbVictoires = 34, NbDefaites = 40, NbDefProlo = 8, NbButsPour = 280, NbButsContre = 337 },
                    new StatsEquipeBd { EquipeId = 1, AnneeStats = 2022, NbPartiesJouees = 82, NbVictoires = 50, NbDefaites = 20, NbDefProlo = 12, NbButsPour = 380, NbButsContre = 267 },
                    new StatsEquipeBd { EquipeId = 2, AnneeStats = 2022, NbPartiesJouees = 82, NbVictoires = 45, NbDefaites = 26, NbDefProlo = 11, NbButsPour = 315, NbButsContre = 287 },
                    new StatsEquipeBd { EquipeId = 3, AnneeStats = 2022, NbPartiesJouees = 82, NbVictoires = 44, NbDefaites = 30, NbDefProlo = 8, NbButsPour = 300, NbButsContre = 307 },
                    new StatsEquipeBd { EquipeId = 4, AnneeStats = 2022, NbPartiesJouees = 82, NbVictoires = 34, NbDefaites = 40, NbDefProlo = 8, NbButsPour = 280, NbButsContre = 337 },
                    new StatsEquipeBd { EquipeId = 1, AnneeStats = 2021, NbPartiesJouees = 82, NbVictoires = 50, NbDefaites = 20, NbDefProlo = 12, NbButsPour = 380, NbButsContre = 267 },
                    new StatsEquipeBd { EquipeId = 2, AnneeStats = 2021, NbPartiesJouees = 82, NbVictoires = 45, NbDefaites = 26, NbDefProlo = 11, NbButsPour = 315, NbButsContre = 287 },
                    new StatsEquipeBd { EquipeId = 3, AnneeStats = 2021, NbPartiesJouees = 82, NbVictoires = 44, NbDefaites = 30, NbDefProlo = 8, NbButsPour = 300, NbButsContre = 307 },
                    new StatsEquipeBd { EquipeId = 4, AnneeStats = 2021, NbPartiesJouees = 82, NbVictoires = 34, NbDefaites = 40, NbDefProlo = 8, NbButsPour = 280, NbButsContre = 337 },
                    new StatsEquipeBd { EquipeId = 1, AnneeStats = 2020, NbPartiesJouees = 82, NbVictoires = 50, NbDefaites = 20, NbDefProlo = 12, NbButsPour = 380, NbButsContre = 267 },
                    new StatsEquipeBd { EquipeId = 2, AnneeStats = 2020, NbPartiesJouees = 82, NbVictoires = 45, NbDefaites = 26, NbDefProlo = 11, NbButsPour = 315, NbButsContre = 287 },
                    new StatsEquipeBd { EquipeId = 3, AnneeStats = 2020, NbPartiesJouees = 82, NbVictoires = 44, NbDefaites = 30, NbDefProlo = 8, NbButsPour = 300, NbButsContre = 307 },
                    new StatsEquipeBd { EquipeId = 4, AnneeStats = 2020, NbPartiesJouees = 82, NbVictoires = 34, NbDefaites = 40, NbDefProlo = 8, NbButsPour = 280, NbButsContre = 337 },
                    new StatsEquipeBd { EquipeId = 1, AnneeStats = 2019, NbPartiesJouees = 82, NbVictoires = 43, NbDefaites = 29, NbDefProlo = 10, NbButsPour = 330, NbButsContre = 290 },
                    new StatsEquipeBd { EquipeId = 2, AnneeStats = 2019, NbPartiesJouees = 82, NbVictoires = 48, NbDefaites = 21, NbDefProlo = 13, NbButsPour = 345, NbButsContre = 255 },
                    new StatsEquipeBd { EquipeId = 3, AnneeStats = 2019, NbPartiesJouees = 82, NbVictoires = 46, NbDefaites = 26, NbDefProlo = 10, NbButsPour = 320, NbButsContre = 295 },
                    new StatsEquipeBd { EquipeId = 4, AnneeStats = 2019, NbPartiesJouees = 82, NbVictoires = 38, NbDefaites = 33, NbDefProlo = 11, NbButsPour = 311, NbButsContre = 307 },
                    new StatsEquipeBd { EquipeId = 1, AnneeStats = 2018, NbPartiesJouees = 82, NbVictoires = 33, NbDefaites = 34, NbDefProlo = 15, NbButsPour = 310, NbButsContre = 312 },
                    new StatsEquipeBd { EquipeId = 2, AnneeStats = 2018, NbPartiesJouees = 82, NbVictoires = 45, NbDefaites = 23, NbDefProlo = 14, NbButsPour = 340, NbButsContre = 275 },
                    new StatsEquipeBd { EquipeId = 3, AnneeStats = 2018, NbPartiesJouees = 82, NbVictoires = 47, NbDefaites = 26, NbDefProlo = 9, NbButsPour = 340, NbButsContre = 298 },
                    new StatsEquipeBd { EquipeId = 4, AnneeStats = 2018, NbPartiesJouees = 82, NbVictoires = 41, NbDefaites = 31, NbDefProlo = 10, NbButsPour = 341, NbButsContre = 280 }
                );
        }
    }
}