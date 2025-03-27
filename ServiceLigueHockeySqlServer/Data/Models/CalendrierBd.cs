using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceLigueHockeySqlServer.Data.Models
{
    /// <summary>
    /// Classe représentant une partie jouée entre deux équipe à une date donnée
    /// </summary>
    [Table("Calendrier")]
    public class CalendrierBd
    {
        // IdPartie sera la primary key utilisée pour lire la feuille de pointage
        // au lieu d'avoir le triplet id equipe hote - id equipe visiteur - date de
        // la partie, même si les deux informations sont uniques.
        public int IdPartie { get; set; } = default;
        // Le timestamp complet sera utilisé pour noter la date ET l'heure de la partie.
        public DateTime DatePartieJouee { get; set; } = default;
        public short? NbreButsComptesParHote { get; set; } = null;
        public short? NbreButsComptesParVisiteur { get; set; } = null;
        public char? AFiniEnProlongation { get; set; } = null;
        public char? AFiniEnTirDeBarrage { get; set; } = null;
        public char EstUnePartieDeSerie { get; set; } = 'N';
        public char EstUnePartiePresaison { get; set; } = 'N';
        public char EstUnePartieSaisonReguliere { get; set; } = 'O';
        public string SommairePartie { get; set; } = string.Empty;
        
        public virtual ICollection<PointeursBd> listePointeurs { get; set; } = default!;
        public virtual ICollection<PenalitesBd> listePenalites { get; set; } = default!;
        
        public int IdEquipeHote { get; set; } = default;
        public virtual EquipeBd EquipeHote { get; set; } = default!;
        public int IdEquipeVisiteuse { get; set; } = default;
        public virtual EquipeBd EquipeVisiteuse { get; set; } = default!;
    }
}
