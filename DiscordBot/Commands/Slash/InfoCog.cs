using Discord.Commands;
using Discord.Interactions;

namespace DiscordBot.Commands.Slash;

public class InfoCog : InteractionModuleBase<SocketInteractionContext>
{
    [SlashCommand("ping", "Replies with Pong!")]
    public async Task PingAsync()
    {
        await RespondAsync($"Pong {Context.Client.Latency}ms");
    }
}