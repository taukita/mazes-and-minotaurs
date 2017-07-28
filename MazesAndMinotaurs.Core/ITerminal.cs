using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazesAndMinotaurs.Core
{
	public interface ITerminal<TGlyph, TTerminalColor>
	{
		TTerminalColor Background { get; set; }
		TTerminalColor Foreground { get; set; }

		void Clear(int x, int y);
		void Draw(int x, int y, TGlyph glyph);
		void Draw(int x, int y, TGlyph glyph, TTerminalColor foreground);
		void Draw(int x, int y, TGlyph glyph, TTerminalColor foreground, TTerminalColor background);
	}
}
