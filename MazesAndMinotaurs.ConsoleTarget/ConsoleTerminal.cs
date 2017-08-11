using MazesAndMinotaurs.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazesAndMinotaurs.ConsoleTarget
{
	public class ConsoleTerminal : AbstractTerminal<char, ConsoleColor>
	{
		public int OffsetX { get; set; }

		public int OffsetY { get; set; }

		public override void Clear(int x, int y)
		{
			Console.SetCursorPosition(OffsetX + x, OffsetY + y);
			Console.BackgroundColor = Background;
			Console.ForegroundColor = Foreground;
			Console.Write(' ');
		}

		protected override void Drawing(int x, int y, char glyph, ConsoleColor foreground, ConsoleColor background)
		{
			Console.SetCursorPosition(OffsetX + x, OffsetY + y);
			Console.BackgroundColor = background;
			Console.ForegroundColor = foreground;
			Console.Write(glyph);
		}
	}
}
