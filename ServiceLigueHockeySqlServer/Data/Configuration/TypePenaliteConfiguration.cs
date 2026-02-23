using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceLigueHockeySqlServer.Data.Models;

namespace ServiceLigueHockeySqlServer.Data.Configuration
{
    public class TypePenalitesConfiguration : IEntityTypeConfiguration<TypePenalitesBd>
    {
        public void Configure(EntityTypeBuilder<TypePenalitesBd> builder)
        {
            builder.ToTable("TypePenalites");
            builder.HasKey(g => g.IdTypePenalite);
            builder.Property(e => e.IdTypePenalite).ValueGeneratedNever();
            builder.Property(e => e.DescriptionPenalite).HasMaxLength(100);
            
            builder.HasData(
                    new TypePenalitesBd { IdTypePenalite = 1, DescriptionPenalite = "Mineure", NbreMinutesPenalitesPourCetteInfraction = 2 },
                    new TypePenalitesBd { IdTypePenalite = 2, DescriptionPenalite = "Majeure", NbreMinutesPenalitesPourCetteInfraction = 5 },
                    new TypePenalitesBd { IdTypePenalite = 3, DescriptionPenalite = "Inconduite de partie", NbreMinutesPenalitesPourCetteInfraction = 10 }
                );
        }
    }
}