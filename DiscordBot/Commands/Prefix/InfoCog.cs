using Discord.Commands;
using SocialDeductionSystem.DiscordBot.Commands.Common;

namespace SocialDeductionSystem.DiscordBot.Commands.Prefix;

public class InfoCog : ModuleBase<SocketCommandContext>
{
    [Command("ping")]
    [Summary("Replies with Pong!")]
    public async Task PingAsync()
    {
        await ReplyAsync(PingHelper.GetPingMessage(Context.Client));
    }
}