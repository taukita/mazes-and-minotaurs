using MazesAndMinotaurs.Core;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
	internal class TerminalWithOffset : AbstractTerminal<Glyph, Color>
	{
		private readonly ITerminal<Glyph, Color> _terminal;
		private readonly int _offsetX;
		private readonly int _offsetY;

		public TerminalWithOffset(ITerminal<Glyph, Color> terminal, int offsetX, int offsetY)
		{
			_terminal = terminal;
			_offsetX = offsetX;
			_offsetY = offsetY;
		}

		public override void Clear(int x, int y)
		{
			_terminal.Clear(x + _offsetX, y + _offsetY);
		}

		public override void Draw(int x, int y, Glyph glyph, Color foreground, Color background)
		{
			_terminal.Draw(x + _offsetX, y + _offsetY, glyph, foreground, background);
		}
	}
}
