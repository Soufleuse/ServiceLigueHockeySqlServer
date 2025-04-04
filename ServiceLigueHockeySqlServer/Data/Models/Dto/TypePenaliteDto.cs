using System;
using System.Collections.Generic;

namespace ServiceLigueHockeySqlServer.Data.Models.Dto {
    
    public class TypePenalitesBd
    {
        public short IdTypePenalite { get; set; } = default;
        public int NbreMinutesPenalitesPourCetteInfraction { get; set; } = 2;
    }
}