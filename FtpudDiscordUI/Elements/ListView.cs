using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Discord;

namespace FtpudDiscordUI.Elements
{
    public class ListView : UiElement
    {
        public String Value => Index >= 0 && Index < _values.Count() ? _values.ElementAt(Index) : "";

        private readonly IReadOnlyCollection<string> _values;
        
        public int _index = 0;
        public int Index
        {
            get => _index;
            set => _index = value >= 0 && value < Size ? value : _index;
        }
        
        public int Size => _values.Count;

        public ListView(IReadOnlyCollection<String> values)
        {
            _values = values;
        }

        public override void CreateElementView(EmbedBuilder builder)
        {
            var listText = new StringBuilder();
            for (var i = 0; i < Size; i++)
            {
                if (i == Index)
                {
                    listText.Append($"**`{_values.ElementAt(i)}`**");
                }
                else
                {
                    listText.Append(_values.ElementAt(i));
                }

                listText.Append("\n");
            }
            listText.Append("\n");
            builder.Description += listText.ToString();
        }
    }
}