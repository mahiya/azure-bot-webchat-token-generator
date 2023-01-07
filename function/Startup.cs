using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AzureBotTokenGenerator
{
    class Startup : FunctionsStartup
    {
        public IConfiguration Configuration { get; }

        public Startup()
        {
            var config = new ConfigurationBuilder().AddEnvironmentVariables();
            Configuration = config.Build();
        }

        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHttpClient();
        }
    }
}
