using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using FtpudDiscordUI.Elements;

namespace FtpudDiscordUI
{
    public static class ObjectUtils {
        public static bool Compare<T>(T Object1, T object2)
        {
            //Get the type of the object
            Type type = typeof(T);

            //return false if any of the object is false
            if (object.Equals(Object1, default(T)) || object.Equals(object2, default(T)))
                return false;

            //Loop through each properties inside class and get values for the property from both the objects and compare
            foreach (System.Reflection.PropertyInfo property in type.GetProperties())
            {
                if (property.Name != "ExtensionData")
                {
                    string Object1Value = string.Empty;
                    string Object2Value = string.Empty;
                    if (type.GetProperty(property.Name).GetValue(Object1, null) != null)
                        Object1Value = type.GetProperty(property.Name).GetValue(Object1, null).ToString();
                    if (type.GetProperty(property.Name).GetValue(object2, null) != null)
                        Object2Value = type.GetProperty(property.Name).GetValue(object2, null).ToString();
                    if (Object1Value.Trim() != Object2Value.Trim())
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }

    public class UiPage
    {
        private readonly List<UiElement> _elements;
        public IUserMessage Root { get; set; }
        private Embed latestBody;

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
            EmbedBuilder builder = new EmbedBuilder();
            _elements.ForEach(el => el.CreateElementView(builder));
            Decorate(builder);
            // Check if something is changed in body before sending it to server
            // trade some cpu cycles to reduce network load
            var body = builder.Build();
            if (!ObjectUtils.Compare(body, latestBody))
            {
                try
                {
                    await Root.ModifyAsync(msg =>
                    {
                        msg.Content = "";
                        msg.Embed = body;
                    });
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                latestBody = body;
            }
            
        }

        protected virtual void Decorate(EmbedBuilder embedBuilder)
        {
            
        }

        protected async Task UpdateReactions(IUserMessage msg)
        {
            await Root.RemoveAllReactionsAsync();
            msg.AddReactionsAsync(
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
            latestBody = null;
            await Root.DeleteAsync();
        }
    }
}