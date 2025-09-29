using System;
using System.Collections.Generic;

namespace ServiceLigueHockeySqlServer.Data.Models.Dto
{
    public class DivisionDto
    {
        public int Id { get; set; } = int.MinValue;

        public string NomDivision { get; set; } = default!;

        public int AnneeDebut { get; set; } = int.MinValue;

        public int? AnneeFin { get; set; } = null;

        public int ConferenceId { get; set; } = int.MinValue;

        public virtual ConferenceDto Conference { get; set; } = default!;
    }
}