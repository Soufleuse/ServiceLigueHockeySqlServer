using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceLigueHockeySqlServer.Data.Models
{
    [Table("EquipeJoueur")]
    public class EquipeJoueurBd
    {
        public int Id { get; set; }
        
        //[ForeignKey("Joueur")]
        public int JoueurId { get; set; }
        public virtual JoueurBd Joueur { get; set; } = default!;

        //[ForeignKey("Equipe")]
        public int EquipeId { get; set; }
        public virtual EquipeBd Equipe { get; set; } = default!;

        public DateTime DateDebutAvecEquipe { get; set; }
        
        public DateTime? DateFinAvecEquipe { get; set; }
        
        public short NoDossard { get; set; }
    }
}
