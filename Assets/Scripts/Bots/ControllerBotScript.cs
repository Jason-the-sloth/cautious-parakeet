using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Helpers;
using Unity.Serialization.Json;
using UnityEngine;

public class ControllerBotScript : IBotScript
{
    private static HttpClient httpClient = new()
    {
        BaseAddress = new Uri("https://localhost:7201"),
    };

    public ControllerBotScript() { }

    public BotCommands GetCommands(BotInput botInput)
    {
        var jsonString = JsonSerialization.ToJson(botInput);
        Debug.Log($"Controller: {jsonString}");
        using StringContent jsonContent = new(jsonString, Encoding.UTF8, "application/json");

        using HttpResponseMessage response = Task.Run(() => httpClient.PostAsync("bot", jsonContent)).GetAwaiter().GetResult();

        if (response.IsSuccessStatusCode)
        {
            var jsonResponse = Task.Run(() => response.Content.ReadAsStringAsync()).GetAwaiter().GetResult();
            return JsonSerialization.FromJson<BotCommands>(jsonResponse);
        }
        else
        {
            Debug.Log(response.Content.ReadAsStringAsync().GetAwaiter().GetResult());
        }
        return new BotCommands();
    }
}