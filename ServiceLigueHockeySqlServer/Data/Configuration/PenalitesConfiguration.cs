using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceLigueHockeySqlServer.Data.Models;

namespace ServiceLigueHockeySqlServer.Data.Configuration
{
    public class PenalitesConfiguration : IEntityTypeConfiguration<PenalitesBd>
    {
        public void Configure(EntityTypeBuilder<PenalitesBd> builder)
        {
            builder.ToTable("Penalites");
            builder.HasKey("MomentDelaPenalite", "IdPartie");
            builder.HasOne("MonCalendrier")
                   .WithMany("listePenalites")
                   .HasForeignKey("IdPartie");
            builder.HasOne("joueurPenalise")
                   .WithMany("listePenalites")
                   .HasForeignKey("IdJoueurPenalise");
        }
    }
}