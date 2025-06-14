using System.Reflection;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SocialDeductionSystem.DiscordBot.Interfaces;

namespace SocialDeductionSystem.DiscordBot.Services.Handlers;

public class PrefixCommandsHandler : ICommandRegistrant, ICommandHandler<SocketMessage>
{
    private readonly DiscordSocketClient _client;
    private readonly IServiceProvider _services;
    private readonly ILogger<PrefixCommandsHandler> _logger;
    private readonly CommandService _commands;
    private readonly IConfiguration _configuration;
    private readonly string _prefix;
    private readonly string _defaultPrefix = "m.";
    
    public PrefixCommandsHandler(
        DiscordSocketClient client,
        IServiceProvider services,
        ILogger<PrefixCommandsHandler> logger,
        IConfiguration configuration)
    {
        _client = client;
        _services = services;
        _logger = logger;
        
        var commandServiceConfig = new CommandServiceConfig
        {
            LogLevel = LogSeverity.Info,
            DefaultRunMode = RunMode.Async,
            ThrowOnError = true,
            CaseSensitiveCommands = false,
        };

        _commands = new CommandService(commandServiceConfig);
        _configuration = configuration;
        
        _prefix = _configuration["BotSettings:Prefix"] ?? Environment.GetEnvironmentVariable("DISCORD_BOT_PREFIX") ?? ".";

        _client.Ready += RegisterCommands;
        _client.MessageReceived += HandleCommand;
    }
    
    public async Task RegisterCommands()
    {
        _client.Ready -= RegisterCommands;
        _logger.LogInformation("Registering prefix commands...");
        await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
        _logger.LogInformation("Prefix commands registered.");
    }

    public async Task HandleCommand(SocketMessage socketInput)
    {
        if (socketInput is not SocketUserMessage userMessage || socketInput.Author.IsBot)
            return;

        var argPos = 0;

        if (userMessage.HasStringPrefix(_prefix, ref argPos) || userMessage.HasStringPrefix(_defaultPrefix, ref argPos))
        {
            var context = new SocketCommandContext(_client, userMessage);
            var result = await _commands.ExecuteAsync(context, argPos, _services);
            if (!result.IsSuccess)
            {
                await context.Channel.SendMessageAsync(result.ErrorReason);
                _logger.LogWarning("Command execution failed: {Error}", result.ErrorReason);
            }
        }
    }
}