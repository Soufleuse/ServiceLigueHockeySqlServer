using System;
using System.Collections.Generic;

namespace ServiceLigueHockey.Data.Models.Dto
{
    public class StatsJoueurDto
    {
        public short AnneeStats { get; set; }
        public short NbPartiesJouees { get; set; }
        public short NbButs { get; set; }
        public short NbPasses { get; set; }
        public short NbPoints { get; set; }
        public short NbMinutesPenalites { get; set; }
        public short PlusseMoins { get; set; }

        // Partie pour les gardiens
        public short Victoires { get; set; }
        public short Defaites { get; set; }
        public short Nulles { get; set; }
        public short DefaitesEnProlongation { get; set; }
        public int ButsAlloues { get; set; }
        public int TirsAlloues { get; set; }
        public double MinutesJouees { get; set; }

        public int JoueurId { get; set; }
        public virtual JoueurDto Joueur { get; set; } = default!;

        // Pour garder un historique des stats d'un joueur qui se serait fait échanger durant l'année.
        public int EquipeId { get; set; }
        public virtual EquipeDto Equipe { get; set; } = default!;
    }
}