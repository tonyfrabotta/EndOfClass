using System.Net.Http.Headers;

namespace DemoApi.Adapters;

public class DeveloperApiAdapter
{
    /*
     * One of these PER external API you are going to call.
     * (One of these per authority (http://api.github.com/, api.progressive.com)
     */

    private readonly HttpClient _httpClient;

    public DeveloperApiAdapter(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "demo-api");
        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json", 1));
    }


    public async Task<OnCallDeveloperResponse?> GetOnCallDeveloperAsync()
    {
        var response = await _httpClient.GetAsync("/");
        response.EnsureSuccessStatusCode(); // Throw a grenade if it isn't > 299


        var developer = await response.Content.ReadFromJsonAsync<OnCallDeveloperResponse>();

        return developer;
    }
}



public class OnCallDeveloperResponse
{
    public string name
    {
        get; set;
    }
    public string email
    {
        get; set;
    }
    public string phoneNumber
    {
        get; set;
    }
}
