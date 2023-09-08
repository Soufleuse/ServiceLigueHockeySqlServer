using ServiceLigueHockeySqlServer.Data.Models;

namespace ServiceLigueHockeySqlServer.Data.Models.Dto
{
    public class StatsEquipeDto
    {
        public short anneeStats { get; set; }

        public short nbPartiesJouees { get; set; } = default;

        public short nbVictoires { get; set; } = default;

        public short nbDefaites { get; set; } = default;

        public short nbDefProlo { get; set; } = default;

        public short nbButsPour { get; set; } = 0;

        public short nbButsContre { get; set; } = 0;

        public int equipeId { get; set; }
        public virtual EquipeDto equipe { get; set; } = default!;
    }
}
