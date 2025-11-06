using System;
using System.Collections.Generic;

namespace ServiceLigueHockeySqlServer.Data.Models.Dto
{
    public class ConferenceDto
    {
        public int Id { get; set; } = int.MinValue;

        public string NomConference { get; set; } = default!;

        public int AnneeDebut { get; set; } = default!;

        public int? AnneeFin { get; set; } = null;

        public int? EstDevenueConference { get; set; } = null;

        public virtual ICollection<DivisionDto> listeDivision { get; set; } = default!;
    }
}