using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceLigueHockey.Data.Models
{
    /// <summary>
    /// Classe représentant une équipe
    /// </summary>
    [Table("Equipe")]
    public class EquipeBd
    {
        public int Id { get; set; }

        public string NomEquipe { get; set; } = default!;

        public string Ville { get; set; } = default!;

        public Int32 AnneeDebut { get; set; }

        public Int32? AnneeFin { get; set; }

        public int? EstDevenueEquipe { get; set; }

        public virtual ICollection<EquipeJoueurBd> listeEquipeJoueur { get; set; } = default!;
    }
}
