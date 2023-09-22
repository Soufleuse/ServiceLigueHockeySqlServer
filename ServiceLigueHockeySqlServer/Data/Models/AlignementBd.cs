using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceLigueHockeySqlServer.Data.Models
{
    /// <summary>
    /// Classe représentant un alignement (roster) d'une équipe
    /// </summary>
    [Table("Alignement")]
    public class AlignementBd
    {
        public int Id { get; set; }

        public DateTime DateDebut { get; set; } = DateTime.Now;

        public DateTime? DateFin { get; set; } = null;
        
        public int EquipeId { get; set; }
        public EquipeBd equipe { get; set; } = default!;

        public int JoueurId { get; set; }
        public JoueurBd joueur { get; set; } = default!;
    }
}