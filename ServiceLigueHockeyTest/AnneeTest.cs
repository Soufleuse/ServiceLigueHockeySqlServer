using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using ServiceLigueHockeySqlServer.Data.Models.Dto;

namespace ServiceLigueHockeyTest;

public class AnneeTest
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
    public async Task LectureListeAnnee()
    {
        HttpResponseMessage response = await _httpClient.GetAsync("api/AnneeStats");

        // Assert
        Assert.That(response.StatusCode == HttpStatusCode.OK, "Le status n'est pas ok.");

        string content = await response.Content.ReadAsStringAsync();
        Assert.That(content != null, "La réponse ne devrait pas être nulle.");
        
        // Further assertions can be made on the JSON content
        //Console.Write(content);

        Assert.Pass();
    }

    [Test, Order(2)]
    public async Task LectureAnnee()
    {
        HttpResponseMessage response = await _httpClient.GetAsync("api/AnneeStats/2017");

        // Assert
        Assert.That(response.StatusCode == HttpStatusCode.OK, "Le status n'est pas ok.");

        string content = await response.Content.ReadAsStringAsync();
        Assert.That(content != null, "La réponse ne devrait pas être nulle.");
        
        // Further assertions can be made on the JSON content
        //Console.Write(string.Format("content de LectureAnnee : {0}", content));

        Assert.Pass();
    }

    [Test, Order(2)]
    public async Task CreerAnnee_AnneeAbsente()
    {
        var anneeACreer = new AnneeStatsDto()
        {
            AnneeStats = 2999, DescnCourte = "Courte", DescnLongue = "Longue"
        };

        HttpContent pasContentDutout = JsonContent.Create<AnneeStatsDto>(
                                                          anneeACreer,
                                                          new MediaTypeHeaderValue("application/json"));
        HttpResponseMessage response = await _httpClient.PostAsync("api/AnneeStats", pasContentDutout);
        //Console.WriteLine(string.Format("response de CreerAnnee_AnneeAbsente : ", response.ToString()));
        Assert.That(response.StatusCode == HttpStatusCode.Created, "Le status n'est pas Created.");

        Assert.Pass();
    }

    [Test, Order(3)]
    public async Task CreerAnnee_AnneePresente()
    {
        var anneeACreer = new AnneeStatsDto()
        {
            AnneeStats = 2999, DescnCourte = "Courte", DescnLongue = "Longue"
        };

        HttpContent pasContentDutout = JsonContent.Create<AnneeStatsDto>(
                                                          anneeACreer,
                                                          new MediaTypeHeaderValue("application/json"));
        HttpResponseMessage response = await _httpClient.PostAsync("api/AnneeStats", pasContentDutout);
        Assert.That(response.StatusCode == HttpStatusCode.InternalServerError, "Le status n'est pas InternalServerError.");

        Assert.Pass();
    }

    [Test, Order(4)]
    public async Task SupprimerAnnee_AnneeEstPresente()
    {
        HttpResponseMessage response = await _httpClient.DeleteAsync("api/AnneeStats/2999");
        Assert.That(response.StatusCode == HttpStatusCode.NoContent, "Le status n'est pas NoContent.");
        Assert.Pass();
    }

    [Test, Order(5)]
    public async Task SupprimerAnnee_AnneeEstAbsente()
    {
        HttpResponseMessage response = await _httpClient.DeleteAsync("api/AnneeStats/2999");
        Assert.That(response.StatusCode == HttpStatusCode.NotFound, "Le status n'est pas NotFound.");
        Assert.Pass();
    }

    [TearDown]
    public void JetteMoiAuxPoubelles()
    {
        _httpClient.Dispose();
    }
}