
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using ServiceLigueHockeySqlServer.Data.Models.Dto;

namespace ServiceLigueHockeyTest;

public class EquipeTest
{
    private HttpClient _httpClient;
    
    [SetUp]
    public void Setup()
    {
        // Initialize HttpClient for the API base address
        // Use WebApplicationFactory for in-memory hosting for integration tests
        // or point to a running API URL
        _httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5245/") };
    }

    [Test, Order(1)]
    public async Task TestListerEquipes()
    {
        // Act
        HttpResponseMessage response = await _httpClient.GetAsync("api/equipe");

        // Assert
        Assert.That(response.StatusCode == HttpStatusCode.OK, "Le status n'est pas ok.");

        string content = await response.Content.ReadAsStringAsync();
        Assert.That(content != null, "La réponse ne devrait pas être nulle.");
        
        // Further assertions can be made on the JSON content
        //Console.Write(content);
        Assert.Pass();
    }

    [Test, Order(2)]
    public async Task TestLireEquipePresente()
    {
        // Act
        HttpResponseMessage response = await _httpClient.GetAsync("api/equipe/1");

        // Assert
        Assert.That(response.StatusCode == HttpStatusCode.OK, "Le status n'est pas ok.");
        
        string content = await response.Content.ReadAsStringAsync();
        Assert.That(content != null, "La réponse ne devrait pas être nulle.");
        
        // Further assertions can be made on the JSON content
        Console.Write(string.Format("content de LectureEquipe : {0}", content));

        Assert.Pass();
    }

    [Test, Order(2)]
    public async Task TestLireEquipeAbsente()
    {
        // Act
        HttpResponseMessage response = await _httpClient.GetAsync("api/equipe/8989");

        // Assert
        Assert.That(response.StatusCode == HttpStatusCode.NotFound, "J'aurais normallement dû ne rien trouver.");

        Assert.Pass();
    }

    [Test, Order(3)]
    public async Task TestModifierEquipe()
    {
        var anneeACreer = new EquipeDto()
        {
            Id = 1,
            NomEquipe = "Canadiens",
            Ville = "Montréal",
            AnneeDebut = 1909,
            AnneeFin = null,
            DivisionId = 1
        };

        HttpContent pasContentDutout = JsonContent.Create<EquipeDto>(
                                                          anneeACreer,
                                                          new MediaTypeHeaderValue("application/json"));

        // Act
        HttpResponseMessage response = await _httpClient.PutAsync("api/equipe/1", pasContentDutout);

        // Assert
        Console.WriteLine(response.StatusCode);
        Assert.That(response.StatusCode == HttpStatusCode.NoContent, "L'équipe n'a pas été modifiée.");

        Assert.Pass();
    }

    [TearDown]
    public void JetteMoiAuxPoubelles()
    {
        _httpClient.Dispose();
    }
}