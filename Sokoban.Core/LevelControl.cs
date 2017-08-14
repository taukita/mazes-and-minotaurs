using System;
using System.Linq;
using MazesAndMinotaurs.Core;
using MazesAndMinotaurs.Ui;
using MazesAndMinotaurs.Ui.Events;

namespace Sokoban.Core
{
	internal class LevelControl<TGlyph, TColor, TKey> : Control<TGlyph, TColor, TKey>
	{
		private readonly IColorProvider<TColor> _colorProvider;
		private readonly IGlyphProvider<TGlyph> _glyphProvider;
		private Level _level;

		public LevelControl(IGlyphProvider<TGlyph> glyphProvider, IColorProvider<TColor> colorProvider)
		{
			_glyphProvider = glyphProvider;
			_colorProvider = colorProvider;
		}

		public Level Level
		{
			get { return _level; }
			set
			{
				_level = value;
				Height = _level.Height;
				Width = _level.Width;
			}
		}

		public event Action<LevelControl<TGlyph, TColor, TKey>> OnLevelCompleted;

		protected override void Drawing(ITerminal<TGlyph, TColor> terminal)
		{
			terminal = new TerminalWithOffset<TGlyph, TColor>(terminal, Left, Top);
			terminal.DrawWalls(Level.Targets, _glyphProvider.TargetGlyph, _colorProvider.TargetForeground,
				_colorProvider.TargetBackground);
			foreach (var crate in Level.Crates)
			{
				var over = Level.Targets.Any(t => t.Item1 == crate.X && t.Item2 == crate.Y);
				terminal.Draw(crate.X, crate.Y, _glyphProvider.CrateGlyph,
					over ? _colorProvider.TargetForeground : _colorProvider.CrateForeground, _colorProvider.CrateBackground);
			}
			terminal.DrawWalls(Level.Walls, _glyphProvider.WallGlyph, _colorProvider.WallForeground,
				_colorProvider.WallBackground);
			terminal.Draw(Level.PlayerX, Level.PlayerY, _glyphProvider.PlayerGlyph, _colorProvider.PlayerForeground,
				_colorProvider.PlayerBackground);
		}

		protected override void KeyboardInput(InputEventArgs<TKey> args)
		{
			var moved = false;

			if (KeyboardAdapter.IsUp(args.Input))
				moved = Level.TryMoveUp();
			else if (KeyboardAdapter.IsLeft(args.Input))
				moved = Level.TryMoveLeft();
			else if (KeyboardAdapter.IsDown(args.Input))
				moved = Level.TryMoveDown();
			else if (KeyboardAdapter.IsRight(args.Input))
				moved = Level.TryMoveRight();

			if (moved && Level.IsCompleted)
				OnLevelCompleted?.Invoke(this);
		}
	}
}