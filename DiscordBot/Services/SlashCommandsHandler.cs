using System.Reflection;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;
using SocialDeductionSystem.DiscordBot.Interfaces;

namespace SocialDeductionSystem.DiscordBot.Services;

public class SlashCommandsHandler : ICommandRegistrant, ICommandHandler<SocketInteraction>
{
    private readonly DiscordSocketClient _client;
    private readonly IServiceProvider _services;
    private readonly ILogger<SlashCommandsHandler> _logger;
    private readonly InteractionService _interactionService;
    
    public SlashCommandsHandler(
        DiscordSocketClient client,
        IServiceProvider services,
        ILogger<SlashCommandsHandler> logger)
    {
        _client = client;
        _services = services;
        _logger = logger;
        
        var interactionServiceConfig = new InteractionServiceConfig
        {
            LogLevel = LogSeverity.Info,
            DefaultRunMode = RunMode.Async,
            ThrowOnError = true,
        };
        
        _interactionService = new InteractionService(client, interactionServiceConfig);
        
        _client.Ready += RegisterCommands;
        _client.InteractionCreated += HandleCommand;
    }

    public async Task RegisterCommands()
    {
        try
        {
            _logger.LogInformation("Discovering Interaction Modules...");

            await _interactionService.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
            
            _logger.LogInformation("Registering Interaction commands...");
            
            // Register globally (or guild-specific for testing)
            // ulong testGuildId = GUILD_ID;
            // await _interactionService.RegisterCommandsToGuildAsync(testGuildId, true);
            await _interactionService.RegisterCommandsGloballyAsync(true);

            _logger.LogInformation("Interaction commands registered.");
        } catch (Exception ex)
        {
            _logger.LogError(ex, "Error registering slash commands");
        }
    }
    
    public async Task HandleCommand(SocketInteraction socketInput)
    {
        try
        {
            var ctx = new SocketInteractionContext(_client, socketInput);
            var result = await _interactionService.ExecuteCommandAsync(ctx, _services);
            
            if (!result.IsSuccess)
            {
                _logger.LogError(
                    "Interaction Error 2ND LAYER: {Error} | Command: {CommandName} | User: {User}", 
                    result.ErrorReason,
                    socketInput switch
                    {
                        SocketSlashCommand s => s.CommandName, 
                        SocketMessageComponent mc => mc.Data.CustomId, _ => "N/A"
                    },
                    ctx.User.Username
                    );
                await RespondError(socketInput, result.ErrorReason);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Interaction Error 1ST LAYER: {Error} | Command: {CommandName} | User: {User}", 
                ex.Message,
                socketInput switch
                {
                    SocketSlashCommand s => s.CommandName, 
                    SocketMessageComponent mc => mc.Data.CustomId, _ => "N/A"
                },
                socketInput.User.Username
            );
            await RespondError(socketInput, "An internal error occurred. Please try again later.");
        }
    }
    
    private async Task RespondError(
        SocketInteraction socketInput,
        string errorMessage
        )
    {
        var embed = new EmbedBuilder()
            .WithTitle("Error")
            .WithDescription(errorMessage)
            .WithColor(Color.Red)
            .Build();

        if (socketInput.HasResponded) 
            await socketInput.FollowupAsync(embed: embed);
        else if (!socketInput.HasResponded)  // Performing check again in case of unfortunate timing
            try { await socketInput.RespondAsync(embed: embed); } catch { }  // Ignore
            
    }
}