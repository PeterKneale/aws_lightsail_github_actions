namespace Web.Core;

public class Service
{
    private readonly HttpClient _httpClient;

    public Service(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<string>> List() =>
        (await _httpClient.GetFromJsonAsync<IEnumerable<string>>("/Database"))!;
}
