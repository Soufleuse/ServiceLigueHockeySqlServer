using System;

namespace ServiceLigueHockeySqlServer.Data.Models.Dto {
    
    public class TypePenalitesDto
    {
        public short IdTypePenalite { get; set; } = default;
        public int NbreMinutesPenalitesPourCetteInfraction { get; set; } = 2;
        public string DescriptionPenalite { get; set; } = string.Empty;     // Mineur/Majeur/Inconduite de match
    }
}