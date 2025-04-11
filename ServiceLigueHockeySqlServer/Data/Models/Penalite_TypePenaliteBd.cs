using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceLigueHockeySqlServer.Data.Models {

    /// <summary>
    /// Classe représentant une partie jouée entre deux équipe à une date donnée
    /// </summary>
    [Table("Penalite_TypePenalite")]
    public class Penalite_TypePenaliteBd {
        public int IdPenalite { get; set; } = default;
        public short IdTypePenalite { get; set; } = default;
        public virtual TypePenalitesBd typePenalitesParent { get; set; } = default!;
        public TimeSpan MomentDelaPenalite { get; set; } = TimeSpan.Zero;
        public int IdJoueurPenalise { get; set; } = default;
        public virtual PenalitesBd penaliteParent { get; set; } = default!;
    }
}
