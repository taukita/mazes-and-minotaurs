using System;
using MazesAndMinotaurs.Ui;
using SFML.Graphics;
using System.Linq;
using static SFML.Window.Keyboard;
using MazesAndMinotaurs.Core;
using MazesAndMinotaurs.Ui.Events;

namespace Sokoban
{
	internal class SfmlSokoban : Control<Glyph, Color, Key>
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

		public event Action<SfmlSokoban> OnLevelCompleted;

		public Level Level { get; set; }

		protected override void Drawing(ITerminal<Glyph, Color> terminal)
		{
			terminal = new TerminalWithOffset(terminal, Left, Top);
			terminal.DrawWalls(Level.Targets, TargetGlyph, TargetForeground, TargetBackground);
			foreach (var crate in Level.Crates)
			{
				var over = Level.Targets.Any(t => t.Item1 == crate.X && t.Item2 == crate.Y);
				terminal.Draw(crate.X, crate.Y, CrateGlyph, over ? TargetForeground : CrateForeground, CrateBackground);

			}
			terminal.DrawWalls(Level.Walls, WallGlyph, WallForeground, WallBackground);
			terminal.Draw(Level.PlayerX, Level.PlayerY, PlayerGlyph, PlayerForeground, PlayerBackground);
		}

		protected override void KeyPressed(KeyPressedEventArgs<Key> args)
		{
			var moved = false;

			if (KeyboardAdapter.IsUp(args.Key))
			{
				moved = Level.TryMoveUp();
			}
			else if (KeyboardAdapter.IsLeft(args.Key))
			{
				moved = Level.TryMoveLeft();
			}
			else if (KeyboardAdapter.IsDown(args.Key))
			{
				moved = Level.TryMoveDown();
			}
			else if (KeyboardAdapter.IsRight(args.Key))
			{
				moved = Level.TryMoveRight();
			}

			if (moved && Level.IsCompleted)
			{
				OnLevelCompleted?.Invoke(this);
			}
		}
	}
}
