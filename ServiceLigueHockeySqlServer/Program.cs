using Microsoft.AspNetCore.HttpOverrides;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ServiceLigueHockeySqlServer.Data;
using System.Reflection.Metadata.Ecma335;

var builder = WebApplication.CreateBuilder(args);

string monAllowSpecificOrigin = "monAllowSpecificOrigin";

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

builder.Services.AddCors(options => {
    options.AddPolicy(name: monAllowSpecificOrigin,
        builder => {
            Func<string, bool> isMonOrigineAllowed = str => { return true; };
            builder.AllowAnyHeader()
                   .AllowAnyMethod()
                   .WithOrigins("http://localhost:12080", "https://localhost:12080", "http://127.0.0.1:12080", "https://127.0.0.1:12080");
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

app.Run();

