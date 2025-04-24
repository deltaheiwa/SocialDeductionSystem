using Discord.Interactions;
using SocialDeductionSystem.DiscordBot.Commands.Services;

namespace SocialDeductionSystem.DiscordBot.Commands.Slash;

public class InfoCog : InteractionModuleBase<SocketInteractionContext>
{
    [SlashCommand("ping", "Replies with Pong!")]
    public async Task PingAsync()
    {
        await RespondAsync(PingService.GetPingMessage(Context.Client));
    }
}