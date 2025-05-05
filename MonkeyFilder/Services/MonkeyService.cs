using System.Net.Http.Json;

namespace MonkeyFinder.Services;

public class MonkeyService
{
    HttpClient httpClient;
    public MonkeyService()
    {
        httpClient = new HttpClient();
    }

    List<Monkey> monkeysList = new();
    public async Task<List<Monkey>> GetMonkeys()
    {
        if(monkeysList?.Count > 0)
            return monkeysList;

        var url = "https://montemagno.com/monkeys.json";

        var reaponse = await httpClient.GetAsync(url);

        if (reaponse.IsSuccessStatusCode)
        {
            monkeysList = await reaponse.Content.ReadFromJsonAsync<List<Monkey>>();
        }

        using var stream = await FileSystem.OpenAppPackageFileAsync("monkeydata.json");
        using var reader = new StreamReader(stream);
        var contents = await reader.ReadToEndAsync();
        monkeysList = JsonSerializer.Deserialize<List<Monkey>>(contents);

        return monkeysList;
    }
}
