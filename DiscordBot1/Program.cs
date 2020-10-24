using System;
using System.IO;
using System.Threading.Tasks;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using FtpudDiscordUI;
using FtpudDiscordUI.Elements;


namespace DiscordBot1
{
    class TestPage : UiPage
    {
        public TestPage(UiHelper helper)
        {
            var checkBox = new CheckBox("Pisjun enabled");
            var label = new TextView("Текст текст текст");
            var label2 = new TextView("----------");
            var labelHeader = new HeaderText("Заголовок");
            var list = new ListView(
                new string[] {"Да", "Ыыы", "Писюн"}
            );
            var button = new SimpleButton("\u2B07",  async () =>
            {
                if(list.Index < 2) list.Index++;
               await UpdateView();
            });
            var button2 = new SimpleButton("\u2B06",  async () =>
            {
                if(list.Index > 0) list.Index--;
                await UpdateView();
            });
            var button3 = new SimpleButton("\uD83C\uDD97",  async () =>
            {
                await Root.Channel.SendMessageAsync(list.Value);
                if (list.Index == 2)
                {
                    checkBox.Checked = !checkBox.Checked;
                    await UpdateView();
                }
                
                if (list.Index == 1)
                {
                    await helper.ClosePage(this);
                    await helper.DisplayPage(new TestPage2(helper), Root.Channel);
                }
            });
            AddElement(label);
            AddElement(label2);
            AddElement(list);
            AddElement(button2);
            AddElement(button);
            AddElement(button3);
            AddElement(labelHeader);
            AddElement(checkBox);
        }
    }
    
    class TestPage2 : UiPage
    {
        public TestPage2(UiHelper helper)
        {
            TextView label = new TextView("Текст текст текст");
            TextView label2 = new TextView("----------");
            HeaderText labelHeader = new HeaderText("ЫЫЫЫЫЫ");
            SimpleButton button3 = new SimpleButton("\u274E",  async () =>
            {
                await helper.ClosePage(this);
            });
            AddElement(label);
            AddElement(label2);
            AddElement(button3);
            AddElement(labelHeader);
        }
    }

    internal class Program
    {
        private DiscordSocketClient _client;
        private UiHelper ui;
        
        public static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        private async Task MainAsync()
        {
            var token = File.ReadAllText("token.txt");
            _client = new DiscordSocketClient();
            ui = new UiHelper(_client);
            
            _client.Log += ClientOnLog;
            _client.MessageReceived += ClientOnMessageReceived;
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            await Task.Delay(-1);
        }


        private async Task ClientOnMessageReceived(SocketMessage arg)
        {
            Console.WriteLine(arg.ToString());
            if (arg.Content.Contains( "init" ))
            {
                RestUserMessage msg = await arg.Channel.SendMessageAsync("Hi " + arg.Channel.Id);
                
                var g = _client.Guilds;
                
                //await ui.DisplayPage(new TestPage(), msg.Channel);
            }
        }

        private async Task ClientOnLog(LogMessage arg)
        {
            Console.WriteLine(arg.ToString());
            if (arg.ToString().Contains("Ready"))
            {
                var channel = _client.GetGuild(769296012585467935).GetTextChannel(769296012585467938);
                await ui.DisplayPage(new TestPage(ui), channel);
            }
        }

        
    }
}

// Emoji unicodes:
// https://apps.timwhitlock.info/emoji/tables/unicode
// https://www.fileformat.info/info/emoji/list.htm