using Microsoft.AspNetCore.HttpOverrides;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ServiceLigueHockey.Data;

var builder = WebApplication.CreateBuilder(args);

string monAllowSpecificOrigin = "monAllowSpecificOrigin";

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Début décommenter pour déployer sur Ubuntu Server
// Laisser commenté pour tester en debug
/*builder.WebHost.ConfigureKestrel(serverOption =>
{
    serverOption.Listen(IPAddress.Parse("10.0.0.5"), 5000);
    serverOption.Listen(IPAddress.Parse("127.0.0.1"), 5000);
});*/
// Fin décommenter pour déployer sur Ubuntu Server

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

builder.Services.AddCors(options => {
    options.AddPolicy(name: monAllowSpecificOrigin,
        builder => {
            builder.WithOrigins("http://localhost:4900", "https://localhost:4900", "https://localhost:7166", "https://127.0.0.1:4900");
            builder.WithHeaders("Content-Type");
            builder.WithMethods("*");
            //builder.WithMethods("POST","GET","PUT","OPTIONS");
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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

