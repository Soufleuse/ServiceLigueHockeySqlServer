using System.Text.Json;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Serilog;
using ServiceLigueHockeySqlServer.Data;

namespace ServiceLigueHockerSqlServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string monAllowSpecificOrigin = "monAllowSpecificOrigin";
            
            var builder = WebApplication.CreateBuilder(args);
            MergeJsonFiles();

            // Maintenant, utiliser le fichier fusionné
            builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true);

            // Configuration Serilog
            builder.Host.UseSerilog((context, configuration) =>
            {
                configuration
                    .ReadFrom.Configuration(context.Configuration)
                    .WriteTo.MSSqlServer(
                        connectionString: context.Configuration.GetConnectionString("sqlServerConnection"),
                        sinkOptions: new Serilog.Sinks.MSSqlServer.MSSqlServerSinkOptions
                        {
                            TableName = "TraceApplicative",
                            AutoCreateSqlTable = false
                        }
                    );
            });

            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ServiceLigueHockeyContext>(options => {
                var connectionString = builder.Configuration.GetConnectionString("sqlServerConnection");
                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new System.Exception("La chaine de connexion est vide.");
                }

                options.UseSqlServer(connectionString);
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: monAllowSpecificOrigin,
                    builder =>
                    {
                        Func<string, bool> isMonOrigineAllowed = str => { return true; };
                        builder.AllowAnyHeader()
                               .AllowAnyMethod()
                               .WithOrigins("http://localhost:12080", "https://localhost:12080", "http://127.0.0.1:12080", "https://127.0.0.1:12080",
                                            "http://localhost:12081", "https://localhost:12081", "http://127.0.0.1:12081", "https://127.0.0.1:12081");
                    });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                app.UseHttpsRedirection();
            }

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseCors(monAllowSpecificOrigin);

            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            // Middleware Serilog pour les requêtes HTTP
            app.UseSerilogRequestLogging();

            // Appliquer les migrations automatiquement
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ServiceLigueHockeyContext>();
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

                // Créer la table de logs si elle n'existe pas
                try
                {
                    var createTableSql = @"
                    IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'TraceApplicative')
                    BEGIN
                        CREATE TABLE TraceApplicative (
                            Id INT IDENTITY(1,1) PRIMARY KEY,
                            Message nvarchar(4000),
                            MessageTemplate nvarchar(4000),
                            Level nvarchar(100),
                            TimeStamp datetime2 NOT NULL,
                            Exception nvarchar(4000),
                            Properties nvarchar(4000)
                        );
                    END";
                    
                    context.Database.ExecuteSqlRaw(createTableSql);
                    logger.LogInformation("Table TraceApplicative vérifiée/créée");
                }
                catch (Exception ex)
                {
                    logger.LogWarning($"Erreur lors de la création de TraceApplicative: {ex.Message}");
                }

                var retryCount = 0;
                var maxRetries = 10;

                while (retryCount < maxRetries)
                {
                    try
                    {
                        logger.LogInformation("Vérification des migrations en attente...");
                        var pendingMigrations = context.Database.GetPendingMigrations();

                        if (pendingMigrations.Any())
                        {
                            logger.LogInformation("Tentative d'application des migrations...");
                            context.Database.Migrate(); // Équivalent à "dotnet ef database update"
                            logger.LogInformation("Migrations appliquées avec succès");
                        }
                        else
                        {
                            logger.LogInformation("Aucune migration en attente");
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        retryCount++;
                        logger.LogWarning($"Tentative {retryCount}/{maxRetries} échouée: {ex.Message}");

                        if (retryCount == maxRetries)
                        {
                            logger.LogError(ex, "Impossible d'appliquer les migrations après {MaxRetries} tentatives", maxRetries);
                            throw;
                        }

                        Task.Delay(TimeSpan.FromSeconds(5)).Wait();
                    }
                }
            }

            app.Run();
        }
        
        static void MergeJsonFiles()
        {
            try
            {
                // Chemins des fichiers
                string appSettingsTemplatePath = "appsettings.template.json";
                string appSettingsPath = "appsettings.json";
                string secretsPath = "messecrets.json";

                // Vérifier si les fichiers existent
                if (!File.Exists(appSettingsTemplatePath))
                {
                    Console.WriteLine("Fichier appsettings.appSettingsTemplatePath.json introuvable");
                    return;
                }

                if (!File.Exists(secretsPath))
                {
                    Console.WriteLine("Fichier messecrets.json introuvable - utilisation du appsettings.json original");
                    if (!File.Exists(appSettingsPath))
                    {
                        File.Copy(appSettingsTemplatePath, appSettingsPath);
                    }
                    return;
                }

                // Lire les contenus
                string appSettingsTemplateContent = File.ReadAllText(appSettingsTemplatePath);
                string secretsContent = File.ReadAllText(secretsPath);

                // Parser les JSON
                var appSettingsJson = JsonDocument.Parse(appSettingsTemplateContent);
                var secretsJson = JsonDocument.Parse(secretsContent);

                // Créer un nouveau dictionnaire pour la fusion
                var mergedConfig = new Dictionary<string, object?>();

                // Ajouter d'abord les paramètres de base
                AddJsonToDictionary(appSettingsJson.RootElement, mergedConfig);

                // Ensuite, ajouter/écraser avec les secrets (priorité plus élevée)
                AddJsonToDictionary(secretsJson.RootElement, mergedConfig);

                // Sérialiser le résultat fusionné
                string mergedJson = JsonSerializer.Serialize(mergedConfig, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                // Écrire le fichier fusionné
                File.WriteAllText(appSettingsPath, mergedJson);

                Console.WriteLine("Fichiers JSON fusionnés avec succès");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la fusion des fichiers JSON : {ex.Message}");
            }
        }

        static void AddJsonToDictionary(JsonElement element, Dictionary<string, object?> dictionary)
        {
            foreach (var property in element.EnumerateObject())
            {
                string key = property.Name;
                JsonElement value = property.Value;
                
                switch (value.ValueKind)
                {
                    case JsonValueKind.Object:
                        var nestedDict = new Dictionary<string, object?>();
                        AddJsonToDictionary(value, nestedDict);
                        dictionary[key] = nestedDict;
                        break;
                    case JsonValueKind.Array:
                        var array = new List<object?>();
                        foreach (var item in value.EnumerateArray())
                        {
                            array.Add(GetJsonValue(item));
                        }
                        dictionary[key] = array;
                        break;
                    default:
                        dictionary[key] = GetJsonValue(value);
                        break;
                }
            }
        }

        static object? GetJsonValue(JsonElement element)
        {
            return element.ValueKind switch
            {
                JsonValueKind.String => element.GetString(),
                JsonValueKind.Number => element.TryGetInt32(out int i) ? i : element.GetDouble(),
                JsonValueKind.True => true,
                JsonValueKind.False => false,
                JsonValueKind.Null => null,
                _ => element.ToString()
            };
        }
    }
}
