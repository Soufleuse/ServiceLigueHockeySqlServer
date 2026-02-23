using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceLigueHockeySqlServer.Data.Models;

namespace ServiceLigueHockeySqlServer.Data.Configuration
{
    public class DivisionConfiguration : IEntityTypeConfiguration<DivisionBd>
    {
        public void Configure(EntityTypeBuilder<DivisionBd> builder)
        {
            builder.ToTable("Division");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.NomDivision).HasMaxLength(25);
            builder.HasIndex(x => new { x.NomDivision, x.AnneeDebut }).IsUnique();
            builder.HasOne(x => x.Conference)
                   .WithMany(y => y.listeDivision)
                   .HasForeignKey(z => z.ConferenceId);
            
            builder.HasData(
                new DivisionBd { Id = 1, ConferenceId = 1, NomDivision = "Atlantique", AnneeDebut = 1994 },
                new DivisionBd { Id = 2, ConferenceId = 1, NomDivision = "Métropolitaine", AnneeDebut = 1994 },
                new DivisionBd { Id = 3, ConferenceId = 2, NomDivision = "Centrale", AnneeDebut = 1994 },
                new DivisionBd { Id = 4, ConferenceId = 2, NomDivision = "Pacifique", AnneeDebut = 1994 }
            );
        }
    }
}