using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using ServiceLigueHockeySqlServer.Data.Models;

namespace ServiceLigueHockeySqlServer.ImportationCalendrier
{
    /// <summary>
    /// Classe pour désérialiser le JSON
    /// </summary>
    public class CalendrierJson
    {
        [JsonPropertyName("idPartie")]
        public int IdPartie { get; set; }
        
        [JsonPropertyName("datePartieJouee")]
        public DateTime DatePartieJouee { get; set; }
        
        [JsonPropertyName("anneeStats")]
        public short AnneeStats { get; set; }
        
        [JsonPropertyName("nbreButsComptesParHote")]
        public short? NbreButsComptesParHote { get; set; }
        
        [JsonPropertyName("nbreButsComptesParVisiteur")]
        public short? NbreButsComptesParVisiteur { get; set; }
        
        [JsonPropertyName("aFiniEnProlongation")]
        public char? AFiniEnProlongation { get; set; }
        
        [JsonPropertyName("aFiniEnTirDeBarrage")]
        public char? AFiniEnTirDeBarrage { get; set; }
        
        [JsonPropertyName("estUnePartieDeSerie")]
        public char EstUnePartieDeSerie { get; set; } = 'N';
        
        [JsonPropertyName("estUnePartiePresaison")]
        public char EstUnePartiePresaison { get; set; } = 'N';
        
        [JsonPropertyName("estUnePartieSaisonReguliere")]
        public char EstUnePartieSaisonReguliere { get; set; } = 'O';
        
        [JsonPropertyName("sommairePartie")]
        public string SommairePartie { get; set; } = string.Empty;
        
        [JsonPropertyName("idEquipeHote")]
        public int IdEquipeHote { get; set; }
        
        [JsonPropertyName("idEquipeVisiteuse")]
        public int IdEquipeVisiteuse { get; set; }
    }

    public class ImporteurCalendrier
    {
        private readonly DbContext _context;

        public ImporteurCalendrier(DbContext context)
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

            List<CalendrierJson>? parties;
            
            try
            {
                parties = JsonSerializer.Deserialize<List<CalendrierJson>>(jsonContent, options);
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
                var partie = new CalendrierBd
                {
                    IdPartie = partieJson.IdPartie,
                    DatePartieJouee = partieJson.DatePartieJouee,
                    AnneeStats = partieJson.AnneeStats,
                    NbreButsComptesParHote = partieJson.NbreButsComptesParHote,
                    NbreButsComptesParVisiteur = partieJson.NbreButsComptesParVisiteur,
                    AFiniEnProlongation = partieJson.AFiniEnProlongation,
                    AFiniEnTirDeBarrage = partieJson.AFiniEnTirDeBarrage,
                    EstUnePartieDeSerie = partieJson.EstUnePartieDeSerie,
                    EstUnePartiePresaison = partieJson.EstUnePartiePresaison,
                    EstUnePartieSaisonReguliere = partieJson.EstUnePartieSaisonReguliere,
                    SommairePartie = partieJson.SommairePartie,
                    IdEquipeHote = partieJson.IdEquipeHote,
                    IdEquipeVisiteuse = partieJson.IdEquipeVisiteuse
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

            var parties = JsonSerializer.Deserialize<List<CalendrierJson>>(jsonContent, options);
            
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
                    var partie = new CalendrierBd
                    {
                        IdPartie = partieJson.IdPartie,
                        DatePartieJouee = partieJson.DatePartieJouee,
                        AnneeStats = partieJson.AnneeStats,
                        NbreButsComptesParHote = partieJson.NbreButsComptesParHote,
                        NbreButsComptesParVisiteur = partieJson.NbreButsComptesParVisiteur,
                        AFiniEnProlongation = partieJson.AFiniEnProlongation,
                        AFiniEnTirDeBarrage = partieJson.AFiniEnTirDeBarrage,
                        EstUnePartieDeSerie = partieJson.EstUnePartieDeSerie,
                        EstUnePartiePresaison = partieJson.EstUnePartiePresaison,
                        EstUnePartieSaisonReguliere = partieJson.EstUnePartieSaisonReguliere,
                        SommairePartie = partieJson.SommairePartie,
                        IdEquipeHote = partieJson.IdEquipeHote,
                        IdEquipeVisiteuse = partieJson.IdEquipeVisiteuse
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
