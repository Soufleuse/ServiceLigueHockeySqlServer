using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceLigueHockeySqlServer.Data.Models;

namespace ServiceLigueHockeySqlServer.Data.Configuration
{
    public class JoueurConfiguration : IEntityTypeConfiguration<JoueurBd>
    {
        public void Configure(EntityTypeBuilder<JoueurBd> builder)
        {
            builder.ToTable("Joueur");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).IsRequired().ValueGeneratedNever();
            builder.Property(c => c.Prenom).HasMaxLength(50);
            builder.Property(c => c.Nom).HasMaxLength(50);
            builder.Property(c => c.VilleNaissance).HasMaxLength(50);
            builder.Property(c => c.PaysOrigine).HasMaxLength(50);
            builder.HasMany("listeEquipeJoueur")
                   .WithOne("Joueur")
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasData(
                new JoueurBd { Id = 1, Prenom = "Jack", Nom = "Tremblay", DateNaissance = new DateTime(1988, 10, 20), VilleNaissance = "Lévis", PaysOrigine = "Canada" },
                new JoueurBd { Id = 2, Prenom = "Simon", Nom = "Lajeunesse", DateNaissance = new DateTime(1996, 1, 25), VilleNaissance = "St-Stanislas", PaysOrigine = "Canada" },
                new JoueurBd { Id = 3, Prenom = "Mathieu", Nom = "Grandpré", DateNaissance = new DateTime(1995, 3, 5), VilleNaissance = "Val d\'or", PaysOrigine = "Canada" },
                new JoueurBd { Id = 4, Prenom = "Ryan", Nom = "Callahan", DateNaissance = new DateTime(1991, 3, 15), VilleNaissance = "London", PaysOrigine = "Canada" },
                new JoueurBd { Id = 5, Prenom = "Drew", Nom = "McCain", DateNaissance = new DateTime(1992, 6, 18), VilleNaissance = "Albany", PaysOrigine = "États-Unis" },
                new JoueurBd { Id = 6, Prenom = "John", Nom = "Harris", DateNaissance = new DateTime(2000, 9, 10), VilleNaissance = "Chico", PaysOrigine = "États-Unis" },
                new JoueurBd { Id = 7, Prenom = "Phil", Nom = "Rodgers", DateNaissance = new DateTime(1996, 12, 21), VilleNaissance = "Calgary", PaysOrigine = "Canada" },
                new JoueurBd { Id = 8, Prenom = "Ted", Nom = "Rodriguez", DateNaissance = new DateTime(1992, 10, 21), VilleNaissance = "Regina", PaysOrigine = "Canada" },
                new JoueurBd { Id = 9, Prenom = "Patrice", Nom = "Lemieux", DateNaissance = new DateTime(1998, 4, 21), VilleNaissance = "Chibougamau", PaysOrigine = "Canada" },
                new JoueurBd { Id = 10, Prenom = "Maurice", Nom = "Béliveau", DateNaissance = new DateTime(1997, 6, 1), VilleNaissance = "Beauceville", PaysOrigine = "Canada" },
                new JoueurBd { Id = 11, Prenom = "Andrew", Nom = "Cruz", DateNaissance = new DateTime(1997, 7, 30), VilleNaissance = "Dallas", PaysOrigine = "États-Unis" },
                new JoueurBd { Id = 12, Prenom = "Chris", Nom = "Trout", DateNaissance = new DateTime(1991, 8, 20), VilleNaissance = "Eau Claire", PaysOrigine = "États-Unis" },
                new JoueurBd { Id = 13, Prenom = "Sergei", Nom = "Datzyuk", DateNaissance = new DateTime(1992, 9, 6), VilleNaissance = "Eau Claire", PaysOrigine = "États-Unis" }
                );
        }
    }
}