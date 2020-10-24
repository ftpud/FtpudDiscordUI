using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using FtpudDiscordUI.Elements;

namespace FtpudDiscordUI
{
    public class UiPage
    {
        private readonly List<UiElement> _elements;
        protected IUserMessage Root;

        public bool IsCurrentMessage(ulong id)
        {
            return Root.Id == id;
        }

        public UiPage()
        {
            _elements = new List<UiElement>();
        }

        public void AddElement(UiElement element)
        {
            _elements.Add(element);
            element.Parent = this;
        }

        public async Task UpdateView()
        {
            
            await Root.ModifyAsync(msg =>
            {
                msg.Content = "";
                EmbedBuilder builder = new EmbedBuilder();
                _elements.ForEach(el => el.CreateElementView(builder));
                msg.Embed = builder.Build();
            });
        }
        
        protected async Task UpdateReactions(IUserMessage msg)
        {
            await Root.RemoveAllReactionsAsync();
            await msg.AddReactionsAsync(
                _elements.SelectMany(el => el.CreateElementReactions()).ToArray());
        }

        public async Task Update()
        {
            await UpdateReactions(Root);
        }

        public async Task Display(IUserMessage message)
        {
            Root = message;
            await Update();
            await UpdateView();
        }
        
        public Task HandleEvents(SocketReaction reaction)
        {
            _elements.ForEach(async e => await e.HandleEvent(reaction));
            return Task.CompletedTask;
        }

        public async Task Close()
        {
            await Root.DeleteAsync();
        }
    }
}