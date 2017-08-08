using System.Collections.Generic;
using System.Linq;
using MazesAndMinotaurs.Core;
using MazesAndMinotaurs.Ui.Events;

namespace MazesAndMinotaurs.Ui.Controls.Containers
{
	public class Border<TGlyph, TColor, TKey> : Container<TGlyph, TColor, TKey>
	{
		private readonly bool _overrideThemes;

		public Border(Control<TGlyph, TColor, TKey> content = null, bool overrideThemes = true)
		{
			if (content != null)
			{
				Controls.Add(content);
			}
			_overrideThemes = overrideThemes;
		}

		public BorderTheme<TGlyph> BorderTheme { get; set; }
		public TGlyph Ellipsis { get; set; }
		public IEnumerable<TGlyph> Title { get; set; }

		protected override void Drawing(ITerminal<TGlyph, TColor> terminal)
		{
			PrepareContent();
			Drawing(terminal, Left, Top, Width, Height, BorderTheme, ColorTheme);
			if (Title?.Any() == true)
			{
				terminal.DrawString(Left + 1, Top, Title.Fit(Width - 2, Ellipsis), 
					ColorTheme.Foreground, ColorTheme.Background);
			}
			if (Controls.Any())
			{
				Controls.First().Draw(terminal);
			}
		}

		private static void Drawing(ITerminal<TGlyph, TColor> terminal, int left, int top, int width, int height,
			BorderTheme<TGlyph> borderTheme, ColorTheme<TColor> colorTheme)
		{
			terminal.Draw(left, top, borderTheme.TopLeft, colorTheme.Foreground, colorTheme.Background);
			terminal.DrawLine(left + 1, top, left + width - 2, top, borderTheme.Top, colorTheme.Foreground, colorTheme.Background);

			terminal.Draw(left + width - 1, top, borderTheme.TopRight, colorTheme.Foreground, colorTheme.Background);
			terminal.DrawLine(left + width - 1, top + 1, left + width - 1, top + height - 2, borderTheme.Right,
				colorTheme.Foreground, colorTheme.Background);

			terminal.Draw(left + width - 1, top + height - 1, borderTheme.BottomRight, colorTheme.Foreground,
				colorTheme.Background);
			terminal.DrawLine(left + width - 2, top + height - 1, left + 1, top + height - 1, borderTheme.Bottom,
				colorTheme.Foreground, colorTheme.Background);

			terminal.Draw(left, top + height - 1, borderTheme.BottomLeft, colorTheme.Foreground, colorTheme.Background);
			terminal.DrawLine(left, top + height - 2, left, top + 1, borderTheme.Left, colorTheme.Foreground,
				colorTheme.Background);
		}

		private void PrepareContent()
		{
			if (Controls.Any())
			{
				var content = Controls.First();
				content.Left = Left + 1;
				content.Top = Top + 1;
				content.Width = Width - 2;
				content.Height = Height - 2;
				if (_overrideThemes)
				{
					content.ColorTheme = ColorTheme;
				}
			}
		}
	}
}