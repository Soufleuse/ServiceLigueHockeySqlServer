using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceLigueHockeySqlServer.Data.Models
{
    /// <summary>
    /// Classe représentant une partie jouée entre deux équipe à une date donnée
    /// </summary>
    [Table("TypePenalites")]
    public class TypePenalitesBd
    {
        // IdPartie sera la primary key utilisée pour lire la feuille de pointage
        // au lieu d'avoir le triplet id equipe hote - id equipe visiteur - date de
        // la partie, même si les deux informations sont uniques.
        public short IdTypePenalite { get; set; } = default;
        public int NbreMinutesPenalitesPourCetteInfraction { get; set; } = 2;
    }
}