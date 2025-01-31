﻿using System.Collections.Generic;
using System.Linq;
using MazesAndMinotaurs.Core;
using MazesAndMinotaurs.Ui.Adapters;

namespace MazesAndMinotaurs.Ui.Controls
{
	public class Label<TGlyph, TColor, TInput> : Control<TGlyph, TColor, TInput>
	{
		public IEnumerable<TGlyph> Delimiter { get; set; }
		public IEnumerable<TGlyph> Text { get; set; }

		protected override void Drawing(ITerminal<TGlyph, TColor> terminal)
		{
			if (Text == null)
				return;
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
