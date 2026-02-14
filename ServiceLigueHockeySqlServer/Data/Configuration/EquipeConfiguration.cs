using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceLigueHockeySqlServer.Data.Models;

namespace ServiceLigueHockeySqlServer.Data.Configuration
{
    public class EquipeConfiguration : IEntityTypeConfiguration<EquipeBd>
    {
        public void Configure(EntityTypeBuilder<EquipeBd> builder)
        {
            builder.ToTable("Equipe");
            builder.HasKey(c => c.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.NomEquipe).HasMaxLength(50);
            builder.Property(x => x.Ville).HasMaxLength(50);
            builder.HasIndex(x => new { x.NomEquipe, x.Ville }).IsUnique();
            builder.HasMany(d => d.listeEquipeJoueur)
                   .WithOne(s => s.Equipe);
            builder.HasOne(x => x.division)
                   .WithMany(y => y.listeEquipeBd)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasData(
                new EquipeBd { Id = 1, NomEquipe = "Canadiensssss", Ville = "Mourial", AnneeDebut = 1989, DivisionId = 1 },
                new EquipeBd { Id = 2, NomEquipe = "Bruns", Ville = "Albany", AnneeDebut = 1984, DivisionId = 1 },
                new EquipeBd { Id = 3, NomEquipe = "Harfangs", Ville = "Hartford", AnneeDebut = 1976, DivisionId = 1 },
                new EquipeBd { Id = 4, NomEquipe = "Boulettes", Ville = "Victoriaville", AnneeDebut = 1999, DivisionId = 1 },
                new EquipeBd { Id = 5, NomEquipe = "Rocher", Ville = "Percé", AnneeDebut = 2001, DivisionId = 1 },
                new EquipeBd { Id = 6, NomEquipe = "Pierre", Ville = "Rochester", AnneeDebut = 1986, DivisionId = 1 }
                );
        }
    }
}