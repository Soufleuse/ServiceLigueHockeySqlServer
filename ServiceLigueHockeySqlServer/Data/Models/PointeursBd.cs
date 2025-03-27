using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceLigueHockeySqlServer.Data.Models
{
    /// <summary>
    /// Classe représentant une partie jouée entre deux équipe à une date donnée
    /// </summary>
    [Table("Pointeurs")]
    public class PointeursBd
    {
        public TimeSpan MomentDuButMarque { get; set; } = TimeSpan.Zero;
        public int IdJoueurButMarque { get; set; } = default;
        public int? IdJoueurPremiereAssistance { get; set; } = null;
        public int? IdJoueurSecondeAssistance { get; set; } = null;
        
        public int IdPartie { get; set; }
        public virtual CalendrierBd MonCalendrier { get; set; } = default!;
    }
}
