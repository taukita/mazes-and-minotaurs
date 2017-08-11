using MazesAndMinotaurs.Ui;
using MazesAndMinotaurs.Ui.Controls;
using MazesAndMinotaurs.Ui.Controls.Containers;

namespace Sokoban.Core
{
	public class GameControl<TGlyph, TColor, TKey> : Pages<TGlyph, TColor, TKey>
	{
		private const int MainMenuPage = 0;
		private const int NewGamePage = 1;
		private const int LocalMenuPage = 2;

		private readonly IColorProvider<TColor> _colorProvider;
		private readonly IGlyphProvider<TGlyph> _glyphProvider;
		private readonly int _heightInGlyphs;
		private readonly int _widthInGlyphs;
		private LevelControl<TGlyph, TColor, TKey> _levelControl;
		private Label<TGlyph, TColor, TKey> _levelCompleteMessageLabel;
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
			Controls.Add(CreateLocalMenu());
		}

		private Control<TGlyph, TColor, TKey> CreateMainMenu()
		{
			var menu = new Menu<TGlyph, TColor, TKey>
				{
					EllipsisGlyph = _glyphProvider.EllipsisGlyph,
					SelectionGlyph = _glyphProvider.SelectionGlyph
				};
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
			_levelControl.OnKeyPressed += (s, e) =>
				{
					if (_levelControl.Level.IsCompleted)
					{
						e.Handled = true;
						var index = _levelControl.Level.Index + 1;
						if (_levelProvider.Count == index)
							Page = MainMenuPage;
						else
							StartLevel(index);
					}
					if (KeyboardAdapter.IsEscape(e.Key))
					{
						e.Handled = true;
						Page = LocalMenuPage;
					}
				};
			_levelControl.OnLevelCompleted += s =>
				{
					_levelCompleteMessageLabel.Text = _glyphProvider.FromString("Congratulations, you have completed the Level!");
				};
			_levelCompleteMessageLabel = new Label<TGlyph, TColor, TKey>
				{
					ColorTheme = _colorProvider.SokobanLabelColorTheme,
					Delimiter = _glyphProvider.Delimiter,
					Height = 1,
					Width = 46
				};

			var panel = new Panel<TGlyph, TColor, TKey>
			{
				Height = _heightInGlyphs,
				Vertical = true,
				Width = _widthInGlyphs
			};
			panel.Controls.Add(_levelControl);
			panel.Controls.Add(_levelCompleteMessageLabel);

			return panel;
		}

		private Control<TGlyph, TColor, TKey> CreateLocalMenu()
		{
			var menu = new Menu<TGlyph, TColor, TKey>
				{
					EllipsisGlyph = _glyphProvider.EllipsisGlyph,
					SelectionGlyph = _glyphProvider.SelectionGlyph
				};
			var continueItem = menu.AddItem(_glyphProvider.FromString("Continue"));
			var restartItem = menu.AddItem(_glyphProvider.FromString("Restart"));
			menu.AddItem(_glyphProvider.FromString("Save game"));
			menu.AddItem(_glyphProvider.FromString("Load game"));
			var mainMenuItem = menu.AddItem(_glyphProvider.FromString("Main menu"));
			menu.OnSelect += (s, item) =>
				{
					if (item == continueItem)
						Page = NewGamePage;
					else if (item == restartItem)
						StartLevel(_levelControl.Level.Index);
					else if (item == mainMenuItem)
						Page = MainMenuPage;
				};

			var border = new Border<TGlyph, TColor, TKey>();
			border.Controls.Add(menu);
			border.BorderTheme = _glyphProvider.MainMenuBorderTheme;
			border.ColorTheme = _colorProvider.MainMenuColorTheme;
			border.Left = 1;
			border.Top = 1;
			border.Width = 20;
			border.Height = 10;
			border.Title = _glyphProvider.FromString("Local menu");

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

		private void StartLevel(int index)
		{
			_levelControl.Level = _levelProvider.GetLevel(index);
			_levelCompleteMessageLabel.Text = _glyphProvider.FromString("");
			Page = NewGamePage;
		}

		private void NewGame()
		{
			//_levelControl.Level = _levelProvider.GetLevel(0);
			_levelControl.Level = LevelProvider.TestLevel;
			_levelCompleteMessageLabel.Text = _glyphProvider.FromString("");
			Page = NewGamePage;
		}
	}
}