using System;

namespace ServiceLigueHockeySqlServer.Data.Models.Dto
{
    /// <summary>
    /// Classe représentant un alignement (roster) d'une équipe
    /// </summary>
    public class AlignementDto
    {
        public int Id { get; set; }

        public DateTime DateDebut { get; set; } = DateTime.Now;

        public DateTime? DateFin { get; set; } = null;
        
        public int EquipeId { get; set; }
        public EquipeDto equipe { get; set; } = default!;

        public int JoueurId { get; set; }
        public JoueurDto joueur { get; set; } = default!;
    }
}