using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazesAndMinotaurs.Core;

namespace MazesAndMinotaurs.ConsoleTarget.Ui
{
	public class WallsControl : Control
	{
		private readonly IEnumerable<Tuple<int, int>> _walls;

		public WallsControl(IEnumerable<Tuple<int, int>> walls)
		{
			_walls = walls;
		}

		public WallsTheme WallsTheme { get; set; } = WallsTheme.Default;

		protected override void Drawing(ITerminal<char, ConsoleColor> terminal)
		{
			foreach (var wall in _walls)
			{
				terminal.Draw(wall.Item1, wall.Item2, GetGlyph(wall.Item1, wall.Item2), ColorTheme.Foreground, ColorTheme.Background);
			}
		}

		private char GetGlyph(int x, int y)
		{
			bool up = _walls.Any(w => w.Equals(Tuple.Create(x, y - 1)));
			bool left = _walls.Any(w => w.Equals(Tuple.Create(x - 1, y)));
			bool down = _walls.Any(w => w.Equals(Tuple.Create(x, y + 1)));
			bool right = _walls.Any(w => w.Equals(Tuple.Create(x + 1, y)));
			return WallsTheme[up, left, down, right];
		}
	}
}
