using Discord;

namespace FtpudDiscordUI.Elements
{
    public class HeaderText : TextView
    {
        public HeaderText(string text) : base(text)
        { }

        public override void CreateElementView(EmbedBuilder builder)
        {
            builder.Title = Text;
        }
    }
}