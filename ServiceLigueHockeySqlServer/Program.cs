using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Serilog;
using ServiceLigueHockeySqlServer.Data;

var builder = WebApplication.CreateBuilder(args);

string monAllowSpecificOrigin = "monAllowSpecificOrigin";

// Maintenant, utiliser le fichier fusionné
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true);

// Configuration Serilog
builder.Host.UseSerilog((context, configuration) =>
{
    configuration
        .ReadFrom.Configuration(context.Configuration)
        .Enrich.FromLogContext()
        .Enrich.WithProperty("Application", "ServiceLigueHockeySqlServer");
});

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// D�but d�commenter pour d�ployer sur Ubuntu Server
// Laisser comment� pour tester en debug
/*builder.WebHost.ConfigureKestrel(serverOption =>
{
    serverOption.Listen(IPAddress.Parse("10.0.0.5"), 5000);
    serverOption.Listen(IPAddress.Parse("127.0.0.1"), 5000);
});*/
// Fin d�commenter pour d�ployer sur Ubuntu Server

builder.Services.AddDbContext<ServiceLigueHockeyContext>(options => {
    //var connectionString =  builder.Configuration.GetConnectionString("mysqlConnection");
    var connectionString = builder.Configuration.GetConnectionString("sqlServerConnection");
    //var connectionString = builder.Configuration.GetConnectionString("winServer2022Connection");
    if (string.IsNullOrEmpty(connectionString))
    {
        throw new System.Exception("La chaine de connexion est vide.");
    }

    //options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 30)));
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
            //.SetIsOriginAllowed(isMonOrigineAllowed)
            //.AllowCredentials()
            //builder.WithOrigins("http://localhost:4900", "https://localhost:4900", "https://localhost:7166", "https://127.0.0.1:4900");
            //builder.WithHeaders("Content-Type");
            //builder.WithMethods("*");
            //builder.WithMethods("POST","GET","PUT","OPTIONS");
        });

    /*options.AddDefaultPolicy(builder => {
        builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost");
    });*/
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

