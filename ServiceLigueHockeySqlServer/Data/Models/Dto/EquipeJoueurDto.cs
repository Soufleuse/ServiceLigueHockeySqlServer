using System;
using System.Collections.Generic;

namespace ServiceLigueHockeySqlServer.Data.Models.Dto
{
    public class EquipeJoueurDto
    {
        public int Id { get; set; }
        
        public int JoueurId { get; set; }

        public int EquipeId { get; set; }
        
        public string PrenomNomJoueur { get; set; } = string.Empty;

        public DateTime DateDebutAvecEquipe { get; set; }
        
        public DateTime? DateFinAvecEquipe { get; set; }
        
        public short NoDossard { get; set; }
    }
}