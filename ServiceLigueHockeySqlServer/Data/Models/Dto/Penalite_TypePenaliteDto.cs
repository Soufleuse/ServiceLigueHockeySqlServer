using System;
using System.Collections.Generic;

namespace ServiceLigueHockeySqlServer.Data.Models.Dto {
    public class Penalite_TypePenaliteDto {
        public int IdPenalite { get; set; } = default;
        public short IdTypePenalite { get; set; } = default;
        //public virtual TypePenalitesDto typePenalitesParent { get; set; } = default!;
        public TimeSpan MomentDelaPenalite { get; set; } = TimeSpan.Zero;
        public int IdJoueurPenalise { get; set; } = default;
        //public virtual PenalitesDto penaliteParent { get; set; } = default!;
    }
}