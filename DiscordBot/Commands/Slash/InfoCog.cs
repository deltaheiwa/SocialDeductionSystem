using Discord.Commands;
using Discord.Interactions;
using DiscordBot.Commands.Services;

namespace DiscordBot.Commands.Slash;

public class InfoCog : InteractionModuleBase<SocketInteractionContext>
{
    [SlashCommand("ping", "Replies with Pong!")]
    public async Task PingAsync()
    {
        await RespondAsync(PingService.GetPingMessage(Context.Client));
    }
}