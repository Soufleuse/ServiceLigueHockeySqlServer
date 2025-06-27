using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceLigueHockeySqlServer.Data.Models
{
    /// <summary>
    /// Classe représentant une division
    /// </summary>
    [Table("Division")]
    public class DivisionBd
    {
        public int Id { get; set; } = int.MinValue;

        public string NomDivision { get; set; } = default!;

        public int AnneeDebut { get; set; } = int.MinValue;

        public int? AnneeFin { get; set; } = null;

        public int AppartientAConference { get; set; } = int.MinValue;

        public ConferenceBd ConferenceParent { get; set; } = default!;

        public virtual ICollection<EquipeBd> listeEquipeBd { get; set; } = default!;
    }
}