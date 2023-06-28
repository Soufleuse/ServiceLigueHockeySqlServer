using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceLigueHockey.Data.Models
{
    [Table("Joueur")]
    public class JoueurBd
    {
        public int Id { get; set; }

        public string Prenom { get; set; } = default!;
        
        public string Nom { get; set; } = default!;
        
        public DateTime DateNaissance { get; set; }

        public string VilleNaissance { get; set; } = default!;
        
        public string PaysOrigine { get; set; } = default!;

        public virtual ICollection<EquipeJoueurBd> listeEquipeJoueur { get; set; } = default!;

        public virtual ICollection<StatsJoueurBd> listeStatsJoueur { get; set; } = default!;
    }
}
