using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceLigueHockeySqlServer.Data.Models;

namespace ServiceLigueHockeySqlServer.Data.Configuration
{
    public class AnneeStatsConfiguration : IEntityTypeConfiguration<AnneeStatsBd>
    {
        public void Configure(EntityTypeBuilder<AnneeStatsBd> builder)
        {
            builder.ToTable("AnneeStats");
            builder.HasKey(s => s.AnneeStats);
            builder.Property(s => s.AnneeStats).IsRequired();
            builder.Property(e => e.AnneeStats).ValueGeneratedNever();
            builder.Property(e => e.DescnCourte).HasMaxLength(10);
            builder.Property(e => e.DescnLongue).HasMaxLength(200);
            builder.HasMany(d => d.ListeParties)
                   .WithOne(d => d.zeAnnee);
            
            builder.HasData(
                    new AnneeStatsBd { AnneeStats = 2025, DescnCourte = "2025/2026", DescnLongue = "Représente la saison 2025/2026" },
                    new AnneeStatsBd { AnneeStats = 2024, DescnCourte = "2024/2025", DescnLongue = "Représente la saison 2024/2025" },
                    new AnneeStatsBd { AnneeStats = 2023, DescnCourte = "2023/2024", DescnLongue = "Représente la saison 2023/2024" },
                    new AnneeStatsBd { AnneeStats = 2022, DescnCourte = "2022/2023", DescnLongue = "Représente la saison 2022/2023" },
                    new AnneeStatsBd { AnneeStats = 2021, DescnCourte = "2021/2022", DescnLongue = "Représente la saison 2021/2022" },
                    new AnneeStatsBd { AnneeStats = 2020, DescnCourte = "2020/2021", DescnLongue = "Représente la saison 2020/2021" },
                    new AnneeStatsBd { AnneeStats = 2019, DescnCourte = "2019/2020", DescnLongue = "Représente la saison 2019/2020" },
                    new AnneeStatsBd { AnneeStats = 2018, DescnCourte = "2018/2019", DescnLongue = "Représente la saison 2018/2019" },
                    new AnneeStatsBd { AnneeStats = 2017, DescnCourte = "2017/2018", DescnLongue = "Représente la saison 2017/2018" }
                );
        }
    }
}