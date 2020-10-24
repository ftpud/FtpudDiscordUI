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

        public UiHelper(DiscordSocketClient client)
        {
            client.ReactionAdded += ClientOnReactionToggle;
            client.ReactionRemoved += ClientOnReactionToggle;
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

        async Task OnClick(SocketReaction arg3)
        {
            if (arg3.UserId != 769294625818148874)
            {
                var id = arg3.MessageId;
                var page = _pagesList.FirstOrDefault(p => p.IsCurrentMessage(id));
                if (page != null)
                {
                    await page.HandleEvents(arg3);
                }
            }
        }

        private Task ClientOnReactionToggle(Cacheable<IUserMessage, ulong> arg1, ISocketMessageChannel arg2, SocketReaction arg3)
        {
            OnClick(arg3).Wait();
            return Task.CompletedTask;
        }
        
        
    }
}