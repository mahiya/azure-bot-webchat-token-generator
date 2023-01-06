using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AzureBotTokenGenerator
{
    public class Function
    {
        readonly HttpClient _client;

        public Function(IConfiguration config, IHttpClientFactory httpClientFactory)
        {
            var key = config["AZURE_BOT_SERVICE_WEB_CHAT_KEY"];
            _client = httpClientFactory.CreateClient();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", key);
        }

        [FunctionName("GenerateBotToken")]
        public async Task<IActionResult> RunAsync([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req)
        {
            var url = "https://directline.botframework.com/v3/directline/tokens/generate";
            var userId = $"dl_{Guid.NewGuid()}";
            var body = new { user = new { id = userId } };
            var content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");
            var resp = await _client.PostAsync(url, content);
            var token = await resp.Content.ReadFromJsonAsync<GeneratedToken>();
            return new OkObjectResult(new { userId, token = token.Token });
        }
    }

    class GeneratedToken
    {
        [JsonPropertyName("conversationId")]
        public string ConversationId { get; set; }

        [JsonPropertyName("token")]
        public string Token { get; set; }

        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }
    }
}
