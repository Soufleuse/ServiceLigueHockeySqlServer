using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceLigueHockeySqlServer.Data.Models;

namespace ServiceLigueHockeySqlServer.Data.Configuration
{
    public class EquipeJoueurConfiguration : IEntityTypeConfiguration<EquipeJoueurBd>
    {
        public void Configure(EntityTypeBuilder<EquipeJoueurBd> builder)
        {
            builder.ToTable("EquipeJoueur");
            builder.HasKey(o => new { o.EquipeId, o.JoueurId, o.DateDebutAvecEquipe });
            builder.HasOne(p => p.Joueur)
                   .WithMany(p => p.listeEquipeJoueur)
                   .HasForeignKey(p => p.JoueurId );
            builder.HasOne("Equipe")
                   .WithMany("listeEquipeJoueur")
                   .HasForeignKey("EquipeId");

            builder.HasData(
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
        }
    }
}