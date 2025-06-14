using Discord.Interactions;
using SocialDeductionSystem.DiscordBot.Commands.Common;

namespace SocialDeductionSystem.DiscordBot.Commands.Slash;

public class InfoCog : InteractionModuleBase<SocketInteractionContext>
{
    [SlashCommand("ping", "Replies with Pong!")]
    public async Task PingAsync()
    {
        await RespondAsync(PingHelper.GetPingMessage(Context.Client));
    }
}