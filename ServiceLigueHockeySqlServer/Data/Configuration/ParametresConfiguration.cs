using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceLigueHockeySqlServer.Data.Models;

namespace ServiceLigueHockeySqlServer.Data.Configuration
{
    public class ParametresConfiguration : IEntityTypeConfiguration<ParametresBd>
    {
        public void Configure(EntityTypeBuilder<ParametresBd> builder)
        {
            builder.ToTable("Parametres");
            builder.HasKey(c => new { c.nom, c.dateDebut });
            builder.Property(c => c.nom).IsRequired();
            builder.Property(c => c.valeur).IsRequired().HasMaxLength(200);
            builder.Property(c => c.dateDebut).IsRequired();
            builder.Property(c => c.dateFin);
            
            builder.HasData(
                    new ParametresBd { nom = "nombrePartiesJouees", valeur = "82", dateDebut = new DateTime(2021, 8, 1), dateFin = null },
                    new ParametresBd { nom = "nombrePartiesJouees", valeur = "56", dateDebut = new DateTime(2020, 8, 1), dateFin = new DateTime(2021, 7, 31)},
                    new ParametresBd { nom = "nombrePartiesJouees", valeur = "71", dateDebut = new DateTime(2019, 8, 1), dateFin = new DateTime(2020, 7, 31)},
                    new ParametresBd { nom = "nombrePartiesJouees", valeur = "82", dateDebut = new DateTime(2013, 8, 1), dateFin = new DateTime(2019, 7, 31)},
                    new ParametresBd { nom = "nombrePartiesJouees", valeur = "48", dateDebut = new DateTime(2012, 8, 1), dateFin = new DateTime(2013, 7, 31)},
                    new ParametresBd { nom = "nombrePartiesJouees", valeur = "82", dateDebut = new DateTime(2005, 8, 1), dateFin = new DateTime(2012, 7, 31)},
                    new ParametresBd { nom = "nombrePartiesJouees", valeur = "0", dateDebut = new DateTime(2004, 8, 1), dateFin = new DateTime(2005, 7, 31)},
                    new ParametresBd { nom = "nombrePartiesJouees", valeur = "82", dateDebut = new DateTime(1995, 8, 1), dateFin = new DateTime(2004, 7, 31)},
                    new ParametresBd { nom = "AjoutSteve", valeur = "ma valeur", dateDebut = new DateTime(2020, 1 ,1), dateFin = null }
                );

        }
    }
}