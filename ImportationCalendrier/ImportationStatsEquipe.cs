using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using ServiceLigueHockeySqlServer.Data.Models;

namespace ServiceLigueHockeySqlServer.ImportationStatsEquipe
{
    /// <summary>
    /// Classe pour désérialiser le JSON
    /// </summary>
    public class StatistiquesEquipeJson
    {
        [JsonPropertyName("anneeStats")]
        public short AnneeStats { get; set; }

        [JsonPropertyName("nbPartiesJouees")]
        public short NbPartiesJouees { get; set; } = default;

        [JsonPropertyName("nbVictoires")]
        public short NbVictoires { get; set; } = default;

        [JsonPropertyName("nbDefaites")]
        public short NbDefaites { get; set; } = default;

        [JsonPropertyName("nbDefProlo")]
        public short NbDefProlo { get; set; } = default;

        [JsonPropertyName("nbButsPour")]
        public short NbButsPour { get; set; } = 0;

        [JsonPropertyName("nbButsContre")]
        public short NbButsContre { get; set; } = 0;

        [JsonPropertyName("equipeId")]
        public int EquipeId { get; set; }
    }

    public class ImporteurStatsEquipe
    {
        private readonly DbContext _context;

        public ImporteurStatsEquipe(DbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Importe les parties depuis un fichier JSON
        /// </summary>
        public async Task<int> ImporterDepuisJson(string cheminFichier)
        {
            if (!File.Exists(cheminFichier))
            {
                throw new FileNotFoundException($"Le fichier {cheminFichier} n'existe pas.");
            }

            // Lire le contenu du fichier JSON
            string jsonContent = await File.ReadAllTextAsync(cheminFichier);
            
            // Désérialiser le JSON
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                AllowTrailingCommas = true
            };

            List<StatistiquesEquipeJson>? parties;
            
            try
            {
                parties = JsonSerializer.Deserialize<List<StatistiquesEquipeJson>>(jsonContent, options);
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException($"Erreur lors de la désérialisation du JSON: {ex.Message}", ex);
            }

            if (parties == null || parties.Count == 0)
            {
                Console.WriteLine("Aucune partie à importer.");
                return 0;
            }

            // Convertir et insérer les données
            int compteur = 0;
            foreach (var partieJson in parties)
            {
                var partie = new StatsEquipeBd
                {
                    AnneeStats = partieJson.AnneeStats,
                    NbPartiesJouees = partieJson.NbPartiesJouees,
                    NbVictoires = partieJson.NbVictoires,
                    NbDefaites = partieJson.NbDefaites,
                    NbDefProlo = partieJson.NbDefProlo,
                    NbButsPour = partieJson.NbButsPour,
                    NbButsContre = partieJson.NbButsContre,
                    EquipeId = partieJson.EquipeId
                };

                _context.Add(partie);
                compteur++;
            }

            // Sauvegarder toutes les modifications
            await _context.SaveChangesAsync();
            
            Console.WriteLine($"{compteur} partie(s) importée(s) avec succès.");
            return compteur;
        }

        /// <summary>
        /// Méthode alternative pour import par lot (plus performant pour gros volumes)
        /// </summary>
        public async Task<int> ImporterParLot(string cheminFichier, int tailleLot = 100)
        {
            if (!File.Exists(cheminFichier))
            {
                throw new FileNotFoundException($"Le fichier {cheminFichier} n'existe pas.");
            }

            string jsonContent = await File.ReadAllTextAsync(cheminFichier);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                AllowTrailingCommas = true
            };

            var parties = JsonSerializer.Deserialize<List<StatsEquipeBd>>(jsonContent, options);
            
            if (parties == null || parties.Count == 0)
            {
                Console.WriteLine("Aucune partie à importer.");
                return 0;
            }

            int total = 0;
            for (int i = 0; i < parties.Count; i += tailleLot)
            {
                var lot = parties.Skip(i).Take(tailleLot);
                
                foreach (var partieJson in lot)
                {
                    var partie = new StatsEquipeBd
                    {
                        AnneeStats = partieJson.AnneeStats,
                        NbPartiesJouees = partieJson.NbPartiesJouees,
                        NbVictoires = partieJson.NbVictoires,
                        NbDefaites = partieJson.NbDefaites,
                        NbDefProlo = partieJson.NbDefProlo,
                        NbButsPour = partieJson.NbButsPour,
                        NbButsContre = partieJson.NbButsContre,
                        EquipeId = partieJson.EquipeId
                    };

                    _context.Add(partie);
                    total++;
                }

                await _context.SaveChangesAsync();
                Console.WriteLine($"Lot {i / tailleLot + 1} : {lot.Count()} partie(s) importée(s).");
            }

            Console.WriteLine($"Total : {total} partie(s) importée(s) avec succès.");
            return total;
        }
    }
}
