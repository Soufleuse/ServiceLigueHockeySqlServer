using System;
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
    }
}
