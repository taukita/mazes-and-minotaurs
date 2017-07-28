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

		public override void Draw(ITerminal<char, ConsoleColor> terminal)
		{
			terminal.DrawRectangle(Left, Top, Width, Height, Glyph,
				IsFocused ? FocusedForeground : Foreground,
				IsFocused ? FocusedBackground : Background);
		}
	}
}
