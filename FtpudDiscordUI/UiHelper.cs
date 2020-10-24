using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace FtpudDiscordUI
{
    public class UiHelper
    {
        private readonly List<UiPage> _pagesList = new List<UiPage>();

        private DiscordSocketClient _client;
        
        public UiHelper(DiscordSocketClient client)
        {
            client.ReactionAdded += ClientOnReactionToggle;
            client.ReactionRemoved += ClientOnReactionToggle;
            _client = client;
        }

        public async Task DisplayPage(UiPage page, IMessageChannel targetChannel)
        {
            if (!_pagesList.Contains(page))
            {
                _pagesList.Add(page);
                var msg = await targetChannel.SendMessageAsync("Loading...");
                await page.Display(msg);
            }
        }

        public async Task ClosePage(UiPage page)
        {
            _pagesList.Remove(page);
            await page.Close();
        }

        async Task OnClick(SocketReaction reaction)
        {
            if (reaction.UserId != _client.CurrentUser.Id)
            {
                var id = reaction.MessageId;
                var page = _pagesList.FirstOrDefault(p => p.IsCurrentMessage(id));
                if (page != null)
                {
                    await page.HandleEvents(reaction);
                }
            }
        }

        private Task ClientOnReactionToggle(Cacheable<IUserMessage, ulong> cacheable, ISocketMessageChannel messageChannel,
            SocketReaction reaction) => OnClick(reaction);
        
    }
}