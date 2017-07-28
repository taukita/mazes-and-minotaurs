using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazesAndMinotaurs.Core;

namespace MazesAndMinotaurs.ConsoleTarget.Ui
{
	public class BorderControl : Control
	{
		public BorderControl()
		{
			Glyph = '#';
			Background = ConsoleColor.Black;
			Foreground = ConsoleColor.White;

			FocusedBackground = ConsoleColor.White;
			FocusedForeground = ConsoleColor.Black;
		}

		public char Glyph { get; set; }

		public ConsoleColor Background { get; set; }
		public ConsoleColor Foreground { get; set; }

		public ConsoleColor FocusedBackground { get; set; }
		public ConsoleColor FocusedForeground { get; set; }

		public BorderTheme BorderTheme { get; set; }

		public override void Draw(ITerminal<char, ConsoleColor> terminal)
		{
			var foreground = IsFocused ? FocusedForeground : Foreground;
			var background = IsFocused ? FocusedBackground: Background;
			if (BorderTheme == null)
			{
				terminal.DrawRectangle(Left, Top, Width, Height, Glyph, foreground, background);
			}
			else
			{
				terminal.Draw(Left, Top, BorderTheme.TopLeft, foreground, background);
				terminal.DrawLine(Left + 1, Top, Left + Width - 2, Top, BorderTheme.Top, foreground, background);

				terminal.Draw(Left + Width - 1, Top, BorderTheme.TopRight, foreground, background);
				terminal.DrawLine(Left + Width - 1, Top + 1, Left + Width - 1, Top + Height - 2, BorderTheme.Right, foreground, background);

				terminal.Draw(Left + Width - 1, Top + Height - 1, BorderTheme.BottomRight, foreground, background);
				terminal.DrawLine(Left + Width - 2, Top + Height - 1, Left + 1, Top + Height - 1, BorderTheme.Bottom, foreground, background);

				terminal.Draw(Left, Top + Height - 1, BorderTheme.BottomLeft, foreground, background);
				terminal.DrawLine(Left, Top + Height - 2, Left, Top + 1, BorderTheme.Left, foreground, background);
			}
		}
	}
}
