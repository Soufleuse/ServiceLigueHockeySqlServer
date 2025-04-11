using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceLigueHockeySqlServer.Data.Models
{
    /// <summary>
    /// Classe représentant une pénalité d'un joueur
    /// </summary>
    [Table("TypePenalites")]
    public class TypePenalitesBd
    {
        public short IdTypePenalite { get; set; } = default;
        
        public int NbreMinutesPenalitesPourCetteInfraction { get; set; } = 2;

        [MaxLength(100)]
        public string DescriptionPenalite { get; set; } = string.Empty;     // Mineur/Majeur/Inconduite de match

        public virtual ICollection<Penalite_TypePenaliteBd> listePenTypePen { get; set; } = default!;
    }
}
