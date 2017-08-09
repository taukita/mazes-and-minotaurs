using MazesAndMinotaurs.Ui;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SFML.Window.Keyboard;
using MazesAndMinotaurs.Core;

namespace Sokoban
{
	internal class GameControl : Control<Glyph, Color, Key>
	{
		private static readonly Glyph CrateGlyph = new Glyph(15, 15);
		private static readonly Color CrateForeground = new Color(158, 134, 100);
		private static readonly Color CrateBackground = Color.Black;

		private static readonly Glyph TargetGlyph = new Glyph(9, 15);
		private static readonly Color TargetForeground = new Color(255, 0, 191);
		private static readonly Color TargetBackground = Color.Black;

		private static readonly Glyph WallGlyph = new Glyph(8, 0);
		private static readonly Color WallForeground = new Color(128, 102, 64);
		private static readonly Color WallBackground = new Color(191, 191, 191);

		private static readonly Glyph PlayerGlyph = new Glyph(2, 0);
		private static readonly Color PlayerForeground = new Color(255, 102, 102);
		private static readonly Color PlayerBackground = Color.Black;

		private IEnumerable<Tuple<int, int>> _crates;
		private IEnumerable<Tuple<int, int>> _targets;
		private IEnumerable<Tuple<int, int>> _walls;
		private int _playerX;
		private int _playerY;

		public GameControl()
		{
			_crates = new List<Tuple<int, int>>
			{
				Tuple.Create(3, 1),
				Tuple.Create(3, 5),
			};
			_targets = new HashSet<Tuple<int, int>>
			{
				Tuple.Create(1, 1),
				Tuple.Create(1, 5),
			};
			var walls = new HashSet<Tuple<int, int>>();
			foreach (var wall in Enumerable.Range(0, 7).Select(n => Tuple.Create(0, n)))
			{
				walls.Add(wall);
			}
			foreach (var wall in Enumerable.Range(0, 7).Select(n => Tuple.Create(6, n)))
			{
				walls.Add(wall);
			}
			foreach (var wall in Enumerable.Range(0, 7).Select(n => Tuple.Create(n, 0)))
			{
				walls.Add(wall);
			}
			foreach (var wall in Enumerable.Range(0, 7).Select(n => Tuple.Create(n, 6)))
			{
				walls.Add(wall);
			}
			_walls = walls;
			walls.Add(Tuple.Create(2, 2));
			walls.Add(Tuple.Create(2, 4));
			walls.Add(Tuple.Create(4, 2));
			walls.Add(Tuple.Create(4, 4));
			_playerX = 3;
			_playerY = 3;			
		}

		protected override void Drawing(ITerminal<Glyph, Color> terminal)
		{
			terminal = new TerminalWithOffset(terminal, Left, Top);
			terminal.DrawWalls(_crates, CrateGlyph, CrateForeground, CrateBackground);
			terminal.DrawWalls(_targets, TargetGlyph, TargetForeground, TargetBackground);
			terminal.DrawWalls(_walls, WallGlyph, WallForeground, WallBackground);
			terminal.Draw(_playerX, _playerY, PlayerGlyph, PlayerForeground, PlayerBackground);
		}
	}
}
