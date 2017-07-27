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
		public BorderControl(ITerminal<char, ConsoleColor> terminal) : base(terminal)
		{
			Glyph = '#';
			Background = ConsoleColor.Black;
			Foreground = ConsoleColor.White;
		}

		public char Glyph { get; set; }
		public ConsoleColor Background { get; set; }
		public ConsoleColor Foreground { get; set; }

		public override bool IsFocused => false;

		public override void Draw()
		{
			Terminal.DrawRectangle(Left, Top, Width, Height, Glyph, Foreground, Background);
		}

		public override void Focus()
		{
		}
	}
}
