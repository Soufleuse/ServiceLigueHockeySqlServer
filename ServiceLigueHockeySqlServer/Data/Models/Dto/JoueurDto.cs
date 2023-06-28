using System;
using System.Collections.Generic;

namespace ServiceLigueHockey.Data.Models.Dto
{
    /// <summary>
    /// Classe représentant un joueur
    /// </summary>
    public class JoueurDto
    {
        public int Id { get; set; }

        public string Prenom { get; set; } = default!;
        
        public string Nom { get; set; } = default!;
        
        public DateTime DateNaissance { get; set; }

        public string VilleNaissance { get; set; } = default!;
        
        public string PaysOrigine { get; set; } = default!;
    }
}