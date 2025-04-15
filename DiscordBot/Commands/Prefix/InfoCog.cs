using Discord.Commands;
using DiscordBot.Commands.Services;

namespace DiscordBot.Commands.Prefix;

public class InfoCog : ModuleBase<SocketCommandContext>
{
    [Command("ping")]
    [Summary("Replies with Pong!")]
    public async Task PingAsync()
    {
        await ReplyAsync(PingService.GetPingMessage(Context.Client));
    }
}