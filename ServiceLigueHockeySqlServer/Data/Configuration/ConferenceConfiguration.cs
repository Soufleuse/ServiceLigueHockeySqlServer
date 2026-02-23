using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceLigueHockeySqlServer.Data.Models;

namespace ServiceLigueHockeySqlServer.Data.Configuration
{
    public class ConferenceConfiguration : IEntityTypeConfiguration<ConferenceBd>
    {
        public void Configure(EntityTypeBuilder<ConferenceBd> builder)
        {
            builder.ToTable("Conference");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedNever();
            builder.Property(x => x.NomConference).HasMaxLength(25);
            builder.HasIndex(x => new { x.NomConference, x.AnneeDebut }).IsUnique();

            builder.HasData(
                new ConferenceBd { Id = 1, NomConference = "Est", AnneeDebut = 1994 },
                new ConferenceBd { Id = 2, NomConference = "Ouest", AnneeDebut = 1994 }
            );
        }
    }
}