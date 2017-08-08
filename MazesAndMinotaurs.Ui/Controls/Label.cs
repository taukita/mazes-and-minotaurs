using System.Collections.Generic;
using System.Linq;
using MazesAndMinotaurs.Core;
using MazesAndMinotaurs.Ui.Adapters;

namespace MazesAndMinotaurs.Ui.Controls
{
	public class Label<TGlyph, TColor, TKey> : Control<TGlyph, TColor, TKey>
	{
		public Label(IKeyboardAdapter<TKey> keyboardAdapter) : base(keyboardAdapter)
		{
		}

		public IEnumerable<TGlyph> Delimiter { get; set; }
		public IEnumerable<TGlyph> Text { get; set; }

		protected override void Drawing(ITerminal<TGlyph, TColor> terminal)
		{
			var y = 0;
			var lines = Text.Split(Delimiter.ToArray());
			foreach (var line in lines)
			{
				var parts = line.Split(Width);
				foreach (var part in parts)
				{
					terminal.DrawString(Left, Top + y, part, ColorTheme.Foreground, ColorTheme.Background);
					y++;
					if (y >= Height)
					{
						break;
					}
				}
				if (y >= Height)
				{
					break;
				}
			}
		}
	}
}
