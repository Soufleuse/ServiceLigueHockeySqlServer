using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceLigueHockeySqlServer.Data.Models {

    [Table("AnneeStats")]
    public class AnneeStatsBd {
        public short AnneeStats { get; set; } = 1850;

        // Sert pour l'affichage dans les Combo boxes
        public string DescnCourte { get; set; } = string.Empty;

        public string DescnLongue { get; set; } = string.Empty;

        public virtual ICollection<CalendrierBd> ListeParties { get; set; } = default!;
    }
}