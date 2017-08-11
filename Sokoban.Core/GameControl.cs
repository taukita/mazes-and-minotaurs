using MazesAndMinotaurs.Ui;
using MazesAndMinotaurs.Ui.Controls;
using MazesAndMinotaurs.Ui.Controls.Containers;

namespace Sokoban.Core
{
	public class GameControl<TGlyph, TColor, TKey> : Pages<TGlyph, TColor, TKey>
	{
		private readonly IColorProvider<TColor> _colorProvider;
		private readonly IGlyphProvider<TGlyph> _glyphProvider;
		private readonly int _heightInGlyphs;
		private readonly int _widthInGlyphs;
		private LevelControl<TGlyph, TColor, TKey> _levelControl;
		private readonly LevelProvider _levelProvider = new LevelProvider();

		public GameControl(IGlyphProvider<TGlyph> glyphProvider, IColorProvider<TColor> colorProvider, int heightInGlyphs,
			int widthInGlyphs)
		{
			_glyphProvider = glyphProvider;
			_colorProvider = colorProvider;
			_heightInGlyphs = heightInGlyphs;
			_widthInGlyphs = widthInGlyphs;

			Controls.Add(CreateMainMenu());
			Controls.Add(CreateGame());
		}

		private Control<TGlyph, TColor, TKey> CreateMainMenu()
		{
			var menu = new Menu<TGlyph, TColor, TKey>(_glyphProvider.EllipsisGlyph, _glyphProvider.SelectionGlyph);
			var newGameItem = menu.AddItem(_glyphProvider.FromString("New game"));
			menu.AddItem(_glyphProvider.FromString("Load game"));
			var exitItem = menu.AddItem(_glyphProvider.FromString("Exit game"));
			menu.OnSelect += (s, item) =>
			{
				if (item == newGameItem)
					NewGame();
				else if (item == exitItem)
					IsFocused = false;
			};

			var border = new Border<TGlyph, TColor, TKey>();
			border.Controls.Add(menu);
			border.BorderTheme = _glyphProvider.MainMenuBorderTheme;
			border.ColorTheme = _colorProvider.MainMenuColorTheme;
			border.Left = 1;
			border.Top = 1;
			border.Width = 20;
			border.Height = 10;
			border.Title = _glyphProvider.FromString("Main menu");

			var label = new Label<TGlyph, TColor, TKey>
			{
				ColorTheme = _colorProvider.SokobanLabelColorTheme,
				Text = _glyphProvider.FromString("SOKOBAN"),
				Delimiter = _glyphProvider.Delimiter,
				Height = 3,
				Width = 7
			};

			var panel = new Panel<TGlyph, TColor, TKey>
			{
				Height = _heightInGlyphs,
				Vertical = true,
				Width = _widthInGlyphs
			};
			panel.OnFocusChanged += (s, e) =>
			{
				if (e.NewValue)
				{
					border.IsFocused = true;
					e.Handled = true;
				}
			};
			panel.Controls.Add(label);
			panel.Controls.Add(border);

			return panel;
		}

		private Control<TGlyph, TColor, TKey> CreateGame()
		{
			_levelControl = new LevelControl<TGlyph, TColor, TKey>(_glyphProvider, _colorProvider)
			{
				Left = 1,
				Top = 1
			};
			_levelControl.OnLevelCompleted += s => { Page = 0; };

			var panel = new Panel<TGlyph, TColor, TKey>
			{
				Height = _heightInGlyphs,
				Vertical = true,
				Width = _widthInGlyphs
			};
			panel.Controls.Add(_levelControl);

			return panel;
		}

		private void NewGame()
		{
			_levelControl.Level = _levelProvider.GetLevel(0);
			Page = 1;
		}
	}
}