using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceLigueHockeySqlServer.Data.Models
{
    /// <summary>
    /// Classe représentant une partie jouée entre deux équipe à une date donnée
    /// </summary>
    [Table("Penalites")]
    public class PenalitesBd
    {
        public TimeSpan MomentDelaPenalite { get; set; } = TimeSpan.Zero;

        public int IdJoueurPenalise { get; set; } = default;
        public virtual JoueurBd joueurPenalise { get; set; } = default!;

        // Oui oui, un joueur peut avoir plus qu'une pénalité au même moment.
        public virtual ICollection<Penalite_TypePenaliteBd> listePenalites { get; set; } = default!;
        
        public int IdPartie { get; set; }
        public virtual CalendrierBd MonCalendrier { get; set; } = default!;
    }
}
