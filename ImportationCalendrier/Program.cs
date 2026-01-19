using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ServiceLigueHockeySqlServer.Data;
using ServiceLigueHockeySqlServer.ImportationCalendrier;
using ServiceLigueHockeySqlServer.ImportationStatsEquipe;

namespace ServiceLigueHockeySqlServer.ImportationDonnees
{
    // Classe principale pour le projet console
    public class ProgramImport
    {
        public static async Task Main(string[] args)
        {
            if (!args.Any())
            {
                Console.WriteLine("Ça prend un paramètre au programme.\n");
                return;
            }

            Console.WriteLine("=== Importation de données de " + args[0] + " ===\n");

            // Configuration
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("ImportationCalendrier/appsettings.json", optional: false);

            IConfiguration config = builder.Build();
            string connectionString = config.GetConnectionString("sqlServerConnection") 
                ?? throw new InvalidOperationException("Connection string manquante");

            // Configuration du DbContext
            var optionsBuilder = new DbContextOptionsBuilder<ServiceLigueHockeyContext>();
            optionsBuilder.UseSqlServer(connectionString);

            int retour = int.MinValue;
            // Utilisation du contexte
            using (var context = new ServiceLigueHockeyContext(config))
            {
                switch (args[0])
                {
                    case "Calendrier":
                        retour = await ImporterCalendrier(context);
                        break;
                    case "StatsEquipe":
                        retour = await ImporterStatsEquipe(context);
                        break;
                    default:
                        break;
                }
            }

            if(retour == 0)
            {
                Console.WriteLine("Importation effectuée avec succès");
            }
            else
            {
                Console.WriteLine("Erreur lors de l'importation");
            }

            Console.WriteLine("\nAppuyez sur une touche pour quitter...");
            //Console.ReadKey();
        }

        private static async Task<int> ImporterCalendrier(ServiceLigueHockeyContext context)
        {
            // Chemin du fichier JSON
            string cheminFichier = "ImportationCalendrier/donneesJson/Calendrier_test.json";

            var importeur = new ImporteurCalendrier(context);

            if (!File.Exists(cheminFichier))
            {
                Console.WriteLine($"Erreur: Le fichier '{cheminFichier}' n'existe pas.");
                Console.WriteLine("Usage: dotnet run [chemin_fichier.json]");
                return 1;
            }

            try
            {
                Console.WriteLine($"Lecture du fichier: {cheminFichier}");
                
                // Choix de la méthode d'import selon la taille
                var fileInfo = new FileInfo(cheminFichier);
                int nbImportes;

                if (fileInfo.Length > 1024 * 1024) // Si > 1 MB
                {
                    Console.WriteLine("Fichier volumineux détecté. Import par lot...");
                    nbImportes = await importeur.ImporterParLot(cheminFichier, 100);
                }
                else
                {
                    nbImportes = await importeur.ImporterDepuisJson(cheminFichier);
                }

                Console.WriteLine($"\n✓ Importation terminée avec succès!");
                Console.WriteLine($"  {nbImportes} partie(s) ajoutée(s) à la base de données.");
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"✗ Erreur: {ex.Message}");
                return 1;
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"✗ Erreur de désérialisation: {ex.Message}");
                return 1;
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"✗ Erreur de base de données: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"  Détails: {ex.InnerException.Message}");
                }
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Erreur inattendue: {ex.Message}");
                Console.WriteLine($"  Stack trace: {ex.StackTrace}");
                return 1;
            }

            return 0;
        }

        private static async Task<int> ImporterStatsEquipe(ServiceLigueHockeyContext context)
        {
            // Chemin du fichier JSON
            string cheminFichier = "ImportationCalendrier/donneesJson/StatsEquipe_test.json";

            var importeur = new ImporteurStatsEquipe(context);

            if (!File.Exists(cheminFichier))
            {
                Console.WriteLine($"Erreur: Le fichier '{cheminFichier}' n'existe pas.");
                Console.WriteLine("Usage: dotnet run [chemin_fichier.json]");
                return 1;
            }

            try
            {
                Console.WriteLine($"Lecture du fichier: {cheminFichier}");
                
                // Choix de la méthode d'import selon la taille
                var fileInfo = new FileInfo(cheminFichier);
                int nbImportes;

                if (fileInfo.Length > 1024 * 1024) // Si > 1 MB
                {
                    Console.WriteLine("Fichier volumineux détecté. Import par lot...");
                    nbImportes = await importeur.ImporterParLot(cheminFichier, 100);
                }
                else
                {
                    nbImportes = await importeur.ImporterDepuisJson(cheminFichier);
                }

                Console.WriteLine($"\n✓ Importation terminée avec succès!");
                Console.WriteLine($"  {nbImportes} partie(s) ajoutée(s) à la base de données.");
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"✗ Erreur: {ex.Message}");
                return 1;
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"✗ Erreur de désérialisation: {ex.Message}");
                return 1;
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"✗ Erreur de base de données: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"  Détails: {ex.InnerException.Message}");
                }
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Erreur inattendue: {ex.Message}");
                Console.WriteLine($"  Stack trace: {ex.StackTrace}");
                return 1;
            }

            return 0;
        }
    }
}