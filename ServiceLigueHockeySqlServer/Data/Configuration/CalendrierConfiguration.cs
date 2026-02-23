using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceLigueHockeySqlServer.Data.Models;

namespace ServiceLigueHockeySqlServer.Data.Configuration
{
    public class CalendrierConfiguration : IEntityTypeConfiguration<CalendrierBd>
    {
        public void Configure(EntityTypeBuilder<CalendrierBd> builder)
        {
            builder.ToTable("Calendrier");
            builder.HasKey(x => x.IdPartie);
            builder.Property(e => e.IdPartie).ValueGeneratedNever();
            builder.HasOne(x => x.zeAnnee)
                   .WithMany(d => d.ListeParties)
                   .HasForeignKey(e => e.AnneeStats)
                   .OnDelete(DeleteBehavior.NoAction);
            builder.HasIndex(u => new { u.IdEquipeHote, u.IdEquipeVisiteuse, u.DatePartieJouee })
                   .IsUnique();
            builder.HasOne(x => x.EquipeHote)
                   .WithMany(e => e.listeEquipeHote)
                   .HasForeignKey(x => x.IdEquipeHote)
                   .HasPrincipalKey(e => e.Id)
                   .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(g => g.EquipeVisiteuse)
                   .WithMany(h => h.listeEquipeVisiteur)
                   .HasForeignKey(i => i.IdEquipeVisiteuse)
                   .HasPrincipalKey(e => e.Id)
                   .OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(p => p.listePointeurs)
                   .WithOne(q => q.MonCalendrier)
                   .OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(z => z.listePenalites)
                   .WithOne(y => y.MonCalendrier)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasData(
                    new CalendrierBd { IdPartie = 1, IdEquipeHote = 1, IdEquipeVisiteuse = 2, AnneeStats = 2024, DatePartieJouee = new DateTime(2024, 10, 5, 20, 0, 0)}
                );
        }
    }
}