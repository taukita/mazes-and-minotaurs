using MazesAndMinotaurs.Ui;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SFML.Window.Keyboard;
using MazesAndMinotaurs.Core;
using MazesAndMinotaurs.Ui.Events;
using SFML.Window;

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

		public Level Level { get; set; }

		protected override void Drawing(ITerminal<Glyph, Color> terminal)
		{
			terminal = new TerminalWithOffset(terminal, Left, Top);
			terminal.DrawWalls(Level.Crates.Select(c => Tuple.Create(c.X, c.Y)), CrateGlyph, CrateForeground, CrateBackground);
			terminal.DrawWalls(Level.Targets, TargetGlyph, TargetForeground, TargetBackground);
			terminal.DrawWalls(Level.Walls, WallGlyph, WallForeground, WallBackground);
			terminal.Draw(Level.PlayerX, Level.PlayerY, PlayerGlyph, PlayerForeground, PlayerBackground);
		}
	}
}
