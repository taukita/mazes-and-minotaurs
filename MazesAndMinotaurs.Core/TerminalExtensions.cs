using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazesAndMinotaurs.Core
{
	public static class TerminalExtensions
	{
		public static void DrawLine<TGlyph, TTerminalColor>(this ITerminal<TGlyph, TTerminalColor> terminal,
			int x1, int y1, int x2, int y2, TGlyph glyph, TTerminalColor foreground, TTerminalColor background)
		{
			if (x1 == x2)
			{
				for (var y = Math.Min(y1, y2); y <= Math.Max(y1, y2); y++)
				{
					terminal.Draw(x1, y, glyph, foreground, background);
				}
			}
			else if (y1 == y2)
			{
				for (var x = Math.Min(x1, x2); x <= Math.Max(x1, x2); x++)
				{
					terminal.Draw(x, y1, glyph, foreground, background);
				}
			}
			else
			{
				throw new Exception("Only horizontal or vertical lines allowed now.");
			}
		}

		public static void DrawRectangle<TGlyph, TTerminalColor>(this ITerminal<TGlyph, TTerminalColor> terminal,
			int left, int top, int width, int height, TGlyph glyph, TTerminalColor foreground, TTerminalColor background)
		{
			terminal.DrawLine(left, top, left + width, top, glyph, foreground, background);
			terminal.DrawLine(left + width, top, left + width, top + height, glyph, foreground, background);
			terminal.DrawLine(left + width, top + height, left, top + height, glyph, foreground, background);
			terminal.DrawLine(left, top + height, left, top, glyph, foreground, background);
		}

		public static void DrawWalls<TGlyph, TTerminalColor>(this ITerminal<TGlyph, TTerminalColor> terminal,
			IEnumerable<Tuple<int, int>> walls, TGlyph wallGlyph, TTerminalColor foreground, TTerminalColor background)
		{
			foreach(var wall in walls)
			{
				terminal.Draw(wall.Item1, wall.Item2, wallGlyph, foreground, background);
			}
		}
	}
}
