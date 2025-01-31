﻿using MazesAndMinotaurs.Core;

namespace Sokoban.Core
{
	internal class TerminalWithOffset<TGlyph, TColor> : AbstractTerminal<TGlyph, TColor>
	{
		private readonly int _offsetX;
		private readonly int _offsetY;
		private readonly ITerminal<TGlyph, TColor> _terminal;

		public TerminalWithOffset(ITerminal<TGlyph, TColor> terminal, int offsetX, int offsetY)
		{
			_terminal = terminal;
			_offsetX = offsetX;
			_offsetY = offsetY;
		}

		public override void Clear(int x, int y)
		{
			_terminal.Clear(x + _offsetX, y + _offsetY);
		}

		protected override void Drawing(int x, int y, TGlyph glyph, TColor foreground, TColor background)
		{
			_terminal.Draw(x + _offsetX, y + _offsetY, glyph, foreground, background);
		}
	}
}