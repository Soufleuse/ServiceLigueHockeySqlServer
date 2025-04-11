using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceLigueHockeySqlServer.Data.Models {

    [Table("AnneeStats")]
    public class AnneeStatsBd {
        public short AnneeStats { get; set; } = 1850;

        // Sert pour l'affichage dans les Combo boxes
        [MaxLength(10)]
        public string DescnCourte { get; set; } = string.Empty;

        [MaxLength(200)]
        public string DescnLongue { get; set; } = string.Empty;

        public virtual ICollection<CalendrierBd> listeCalendrier { get; set; } = default!;
    }
}