using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.AI;
using Azure.AI.Inference;
using Azure;

namespace YourApp.AI
{
    public class Program
    {
#if DEBUG
        public static async Task Main(string[] args)
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .AddUserSecrets<Program>()
                .Build();

            var host = CreateHostBuilder(args, config).Build();

            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var chatService = services.GetRequiredService<ChatService>();

            while (true)
            {
                Console.WriteLine("Enter your prompt (or type 'exit' to quit):");
                var prompt = Console.ReadLine();

                if (string.Equals(prompt, "exit", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }

                var response = await chatService.GetResponseAsync(prompt);
                Console.WriteLine($"AI Response: {response}");
            }
        }
#endif

        public static IHostBuilder CreateHostBuilder(string[] args, IConfigurationRoot config) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    var endpoint = new Uri(config["ChatService:Endpoint"]);
                    var model = config["ChatService:Model"];
                    var token = config["GH_TOKEN"];

                    IChatClient client = new ChatCompletionsClient(endpoint, new AzureKeyCredential(token))
                        .AsChatClient(model);

                    services.AddSingleton(client);
                    services.AddTransient<ChatService>();
                });
    }
}
