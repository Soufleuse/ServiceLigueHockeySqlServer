using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceLigueHockeySqlServer.Data.Models
{
    /// <summary>
    /// Classe représentant une statistique d'équipe
    /// </summary>
    [Table("StatsEquipe")]
    public class StatsEquipeBd
    {
        public short AnneeStats { get; set; }

        public short NbPartiesJouees { get; set; } = default;

        public short NbVictoires { get; set; } = default;

        public short NbDefaites { get; set; } = default;

        public short NbDefProlo { get; set; } = default;

        public short NbButsPour { get; set; } = 0;

        public short NbButsContre { get; set; } = 0;

        public int EquipeId { get; set; }
        public virtual EquipeBd Equipe { get; set; } = default!;
    }
}
