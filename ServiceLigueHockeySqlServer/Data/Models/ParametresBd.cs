using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace ServiceLigueHockeySqlServer.Data.Models
{
    /// <summary>
    /// Classe représentant les paramètres applicatifs.
    /// </summary>
    [Table("ParametresBd")]
    public class ParametresBd
    {
        public string nom { get; set; } = string.Empty;
        
        public string valeur { get; set; } = string.Empty;
        
        public DateTime dateDebut { get; set; } = DateTime.MinValue;
        
        public Nullable<DateTime> dateFin { get; set; } = null;
    }
}
