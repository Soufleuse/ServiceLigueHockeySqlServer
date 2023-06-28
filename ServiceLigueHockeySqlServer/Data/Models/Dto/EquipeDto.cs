using System;
using System.Collections.Generic;

namespace ServiceLigueHockey.Data.Models.Dto
{
    /// <summary>
    /// Classe représentant une équipe
    /// </summary>
    public class EquipeDto
    {
        public int Id { get; set; }

        public string NomEquipe { get; set; } = default!;

        public string Ville { get; set; } = default!;

        public Int32 AnneeDebut { get; set; }

        public Int32? AnneeFin { get; set; }

        public int? EstDevenueEquipe { get; set; }

        //public virtual ICollection<EquipeJoueurBd> listeEquipeJoueur { get; set; } = default!;
    }
}
