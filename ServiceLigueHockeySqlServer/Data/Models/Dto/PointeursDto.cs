using System;
using System.Collections.Generic;

namespace ServiceLigueHockeySqlServer.Data.Models.Dto {
    
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