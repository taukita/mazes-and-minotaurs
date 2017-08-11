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

		public abstract void Clear(int x, int y);

		public void Draw(int x, int y, TGlyph glyph)
		{
			Draw(x, y, glyph, Foreground, Background);
		}

		public void Draw(int x, int y, TGlyph glyph, TTerminalColor foreground)
		{
			Draw(x, y, glyph, foreground, Background);
		}

		public void Draw(int x, int y, TGlyph glyph, TTerminalColor foreground, TTerminalColor background)
		{
			var colorAwareGlyph = glyph as IColorAwareGlyph<TTerminalColor>;
			if (colorAwareGlyph == null)
			{
				Drawing(x, y, glyph, foreground, background);
			}
			else
			{
				Drawing(x, y, glyph, colorAwareGlyph.Foreground, colorAwareGlyph.Background);
			}
		}

		protected abstract void Drawing(int x, int y, TGlyph glyph, TTerminalColor foreground, TTerminalColor background);
	}
}
