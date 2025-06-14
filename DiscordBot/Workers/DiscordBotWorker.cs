using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SocialDeductionSystem.DiscordBot.Services;
using SocialDeductionSystem.DiscordBot.Services.Handlers;

namespace SocialDeductionSystem.DiscordBot.Workers;

public class DiscordBotWorker : BackgroundService
{
    private readonly DiscordSocketClient _client;
    private readonly IConfiguration _configuration;
    private readonly ILogger<DiscordBotWorker> _logger;
    private readonly PrefixCommandsHandler _prefixCommandHandler;
    private readonly SlashCommandsHandler _slashCommandHandler;

    public DiscordBotWorker(
        DiscordSocketClient client, 
        IConfiguration configuration,
        ILogger<DiscordBotWorker> logger,
        PrefixCommandsHandler prefixCommandHandler,
        SlashCommandsHandler slashCommandHandler)
    {
        _client = client;
        _configuration = configuration;
        _logger = logger;
        _prefixCommandHandler = prefixCommandHandler;
        _slashCommandHandler = slashCommandHandler;
        
        _logger.LogInformation("DiscordBotWorker constructed");
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _client.Log += LogAsync;
        _client.Ready += ReadyAsync;
        
        var token = _configuration["DiscordToken"] ?? Environment.GetEnvironmentVariable("DISCORD_BOT_TOKEN");

        if (string.IsNullOrWhiteSpace(token))
        {
            _logger.LogCritical("Token not found. Please set the DISCORD_BOT_TOKEN environment variable or add DiscordToken to user secrets.");
            return;
        }
        
        await _client.LoginAsync(TokenType.Bot, token);
        await _client.StartAsync();

        await Task.Delay(Timeout.Infinite, stoppingToken); // Keep the bot running until cancelled
    }
    
    private Task LogAsync(LogMessage log)
    {
        var level = log.Severity switch
        {
            LogSeverity.Critical => LogLevel.Critical,
            LogSeverity.Error => LogLevel.Error,
            LogSeverity.Warning => LogLevel.Warning,
            LogSeverity.Info => LogLevel.Information,
            LogSeverity.Verbose => LogLevel.Debug,
            LogSeverity.Debug => LogLevel.Trace,
            _ => LogLevel.Information
        };

        _logger.Log(level, log.Exception, "[{Source}] {Message}", log.Source, log.Message);
        return Task.CompletedTask;
    }
    
    private Task ReadyAsync()
    {
        LogAsync(new LogMessage(LogSeverity.Info, "DiscordBot", 
            $"Client Ready! Logged in as \"{_client.CurrentUser.Username}#{_client.CurrentUser.Discriminator}\""));
        return Task.CompletedTask;
    }
}