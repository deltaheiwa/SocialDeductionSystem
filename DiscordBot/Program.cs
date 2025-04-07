using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordBot.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

using DiscordBot.Workers;

namespace DiscordBot;

internal class Program
{
    private static void Main(string[] args)
    {
        MainAsync(args).GetAwaiter().GetResult();
    }
    
    private static async Task MainAsync(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        Log.Information("Host built successfully — Serilog should be working now.");
        await host.RunAsync();
    }

    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, builder) =>
            {
                builder.AddUserSecrets<Program>();
                builder.AddJsonFile("appsettings.json", true, true);
                builder.AddJsonFile(
                    $"appsettings.{context.HostingEnvironment.EnvironmentName}.json",
                    true,
                    true
                );
            })
            .UseSerilog((context, services, loggerConfiguration) =>
            {
                loggerConfiguration
                    .ReadFrom.Configuration(context.Configuration)
                    .ReadFrom.Services(services)
                    .Enrich.FromLogContext();
            })
            .ConfigureServices((hostContext, services) =>
            {
                IConfiguration configuration = hostContext.Configuration;

                var socketConfiguration = new DiscordSocketConfig
                {
                    GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.MessageContent,
                    LogLevel = LogSeverity.Info,
                };
                
                services.AddSingleton(socketConfiguration);
                services.AddSingleton<PrefixCommandsHandler>();
                services.AddSingleton<SlashCommandsHandler>();
                services.AddSingleton<DiscordSocketClient>();
                services.AddHostedService<DiscordBotWorker>();
            });
}
