using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceLigueHockeySqlServer.Data.Models;

namespace ServiceLigueHockeySqlServer.Data.Configuration
{
    public class PointeursConfiguration : IEntityTypeConfiguration<PointeursBd>
    {
        public void Configure(EntityTypeBuilder<PointeursBd> builder)
        {
            builder.ToTable("FeuillePointage");
            builder.HasKey(u => new { u.IdPartie, u.MomentDuButMarque });
            builder.HasOne("MonCalendrier")
                   .WithMany("listePointeurs")
                   .HasForeignKey("IdPartie");
        }
    }
}