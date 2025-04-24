using Discord;
using Discord.WebSocket;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using SocialDeductionSystem.DiscordBot.Services;
using SocialDeductionSystem.DiscordBot.Workers;
using SocialDeductionSystem.Persistence.Infrastructure;

namespace SocialDeductionSystem.DiscordBot;

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
        CreateDatabase(host);
        await host.RunAsync();
    }

    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, builder) =>
            {
                if (context.HostingEnvironment.IsDevelopment())
                {
                    builder.AddUserSecrets<Program>();
                }
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
                
                var connectionString = configuration["Database:ConnectionString"];
                if (string.IsNullOrWhiteSpace(connectionString))
                {
                    throw new InvalidOperationException("Database connection string is not set.");
                }
                
                services.AddDbContext<DatabaseContext>(options =>
                {
                    options.UseSqlite(connectionString);

                    if (hostContext.HostingEnvironment.IsDevelopment())
                    {
                        options.EnableSensitiveDataLogging();
                        options.EnableDetailedErrors();
                    }
                });
            });

    private static void CreateDatabase(IHost host)
    {
        using var scope = host.Services.CreateScope();
        try
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
            dbContext.Database.EnsureCreated();
            Log.Information("Database created or already exists.");
        }
        catch (Exception e)
        {
            Log.Error(e, "An error occurred while creating the database.");
        }
    }
}
