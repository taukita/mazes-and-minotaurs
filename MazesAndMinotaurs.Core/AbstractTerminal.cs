using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazesAndMinotaurs.Core
{
	public abstract class AbstractTerminal<TGlyph, TTerminalColor> : ITerminal<TGlyph, TTerminalColor>
	{
		public TTerminalColor Background { get; set; }

		public TTerminalColor Foreground { get; set; }

		public void Draw(int x, int y, TGlyph glyph)
		{
			Draw(x, y, glyph, Foreground, Background);
		}

		public void Draw(int x, int y, TGlyph glyph, TTerminalColor foreground)
		{
			Draw(x, y, glyph, foreground, Background);
		}

		public abstract void Draw(int x, int y, TGlyph glyph, TTerminalColor foreground, TTerminalColor background);
	}
}
