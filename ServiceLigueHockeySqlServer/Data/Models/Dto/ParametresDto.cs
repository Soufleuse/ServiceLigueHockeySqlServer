namespace ServiceLigueHockeySqlServer.Data.Models.Dto
{
    /// <summary>
    /// Classe représentant les paramètres applicatifs.
    /// </summary>
    public class ParametresDto
    {
        public string nom { get; set; } = string.Empty;
        
        public string valeur { get; set; } = string.Empty;
        
        public DateTime dateDebut { get; set; } = DateTime.MinValue;
        
        public Nullable<DateTime> dateFin { get; set; } = null;
    }
}