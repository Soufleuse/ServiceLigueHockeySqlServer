using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceLigueHockeySqlServer.Data.Models
{
    [Table("Conference")]
    public class ConferenceBd
    {
        public int Id { get; set; } = int.MinValue;

        public string NomConference { get; set; } = default!;

        public int AnneeDebut { get; set; } = default!;

        public int? AnneeFin { get; set; } = null;

        public int? EstDevenueConference { get; set; } = null;

        public virtual ICollection<DivisionBd> listeDivision { get; set; } = default!;
    }
}