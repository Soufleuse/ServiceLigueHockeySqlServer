using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceLigueHockeySqlServer.Data.Models
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

        public int DivisionId { get; set; } = default!;

        public virtual DivisionBd division { get; set; } = default!;

        public virtual ICollection<EquipeJoueurBd> listeEquipeJoueur { get; set; } = default!;
        public virtual ICollection<StatsEquipeBd> listeStatsEquipe { get; set; } = default!;

        public virtual ICollection<CalendrierBd> listeEquipeHote { get; set; } = default!;
        public virtual ICollection<CalendrierBd> listeEquipeVisiteur { get; set; } = default!;
    }
}
