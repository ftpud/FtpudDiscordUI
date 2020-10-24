using System;
using Discord;

namespace FtpudDiscordUI.Elements
{
    public class CheckBox : TextView
    {

        private const string CheckedEmote = "\u2705";
        private const string UncheckedEmote = "\u274E";
        
        public bool Checked { get; set; } = false;
        
        public CheckBox(String text) : base(text)
        { }

        public override void CreateElementView(EmbedBuilder builder)
        {
            builder.Description += $"{(Checked?CheckedEmote:UncheckedEmote)} {Text} \n";
        }
    }
}