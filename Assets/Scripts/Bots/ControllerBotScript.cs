using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

public class ControllerBotScript : IBotScript
{
    private static HttpClient httpClient = new()
    {
        BaseAddress = new Uri("https://localhost:7201"),
    };

    public ControllerBotScript() { }

    public BotCommands GetCommands(BotInput botinput)
    {
        var colliders = new List<Collider2D>();
        BotCommands botCommands = new();

        return botCommands;
    }

    private static async Task<BotCommands> PostAsync(HttpClient httpClient, BotInput botInput)
    {
        using StringContent jsonContent = new(
            JsonUtility.ToJson(botInput),
        Encoding.UTF8,
        "application/json");

        using HttpResponseMessage response = await httpClient.PostAsync(
            "bot",
            jsonContent);

        response.EnsureSuccessStatusCode();

        var jsonResponse = await response.Content.ReadAsStringAsync();
        return JsonUtility.FromJson<BotCommands>(jsonResponse);
    }
}