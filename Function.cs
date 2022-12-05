using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
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
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("BotConnector", key);
        }

        [FunctionName("GenerateBotToken")]
        public async Task<IActionResult> RunAsync([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req)
        {
            const string url = "https://webchat.botframework.com/api/tokens";
            var resp = await _client.GetAsync(url);
            var token = (await resp.Content.ReadAsStringAsync()).Replace("\"", "");
            return new OkObjectResult(token);
        }
    }
}
