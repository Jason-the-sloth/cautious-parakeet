using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Helpers;
using Unity.Serialization.Json;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Services
{
    public class ControlService
    {
        private static HttpClient httpClient = new()
        {
            BaseAddress = GlobalVariables.BaseAddress,
        };

        public ControlService()
        {

        }

        public void Capture(BotInput botInput, BotCommands botCommands)
        {
            Post("capture", new CaptureRequest(botInput, botCommands));
        }
        
        public BotCommands GetCommands(BotInput botInput)
        {
            var jsonResponse = Post("bot", botInput);
            return JsonSerialization.FromJson<BotCommands>(jsonResponse);
        }

        private string Post(string controller, object content)
        {
            var jsonString = JsonSerialization.ToJson(content);
            Debug.Log(jsonString);
            using StringContent jsonContent = new(jsonString, Encoding.UTF8, "application/json");
            using HttpResponseMessage response = Task.Run(() => httpClient.PostAsync(controller, jsonContent)).GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = Task.Run(() => response.Content.ReadAsStringAsync()).GetAwaiter().GetResult();
                Debug.Log(jsonResponse);
                return jsonResponse;
            }
            else
            {
                Debug.Log(response.Content.ReadAsStringAsync().GetAwaiter().GetResult());
            }
            return default;
        }
    }
}