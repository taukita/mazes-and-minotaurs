using System.Collections.Generic;
using System.IO;
using MazesAndMinotaurs.Ui;
using MazesAndMinotaurs.Ui.Controls;
using MazesAndMinotaurs.Ui.Controls.Containers;

namespace Sokoban.Core
{
	public class GameControl<TGlyph, TColor, TKey> : Pages<TGlyph, TColor, TKey>
	{
		private const bool EnableTestLevel = true;

		private const int MainMenuPage = 0;
		private const int NewGamePage = 1;
		private const int LocalMenuPage = 2;
		private const int SaveMenuPage = 3;
		private const int LoadMenuPage = 4;

		private readonly IColorProvider<TColor> _colorProvider;
		private readonly IGlyphProvider<TGlyph> _glyphProvider;
		private readonly int _heightInGlyphs;
		private readonly int _widthInGlyphs;
		private LevelControl<TGlyph, TColor, TKey> _levelControl;
		private Label<TGlyph, TColor, TKey> _levelCompleteMessageLabel;
		private readonly LevelProvider _levelProvider = new LevelProvider();
		private int _previousPage;

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
			Controls.Add(CreateSaveMenu());
			Controls.Add(CreateLoadMenu());

			OnPageChanged += (pages, args) =>
			{
				_previousPage = args.OldValue;
			};
		}

		private Control<TGlyph, TColor, TKey> CreateMainMenu()
		{
			var menu = Menu();
			var newGameItem = menu.AddItem(_glyphProvider.FromString("New game"));
			var loadGameItem = menu.AddItem(_glyphProvider.FromString("Load game"));
			var exitItem = menu.AddItem(_glyphProvider.FromString("Exit game"));
			menu.OnSelect += (s, item) =>
			{
				if (item == newGameItem)
					StartLevel(EnableTestLevel ? -1 : 0);
				else if (item == loadGameItem)
					Page = LoadMenuPage;
				else if (item == exitItem)
					IsFocused = false;
			};

			var border = Boder(20, 10, "Main menu", menu);

			var label = new Label<TGlyph, TColor, TKey>
			{
				ColorTheme = _colorProvider.SokobanLabelColorTheme,
				Text = _glyphProvider.FromString("SOKOBAN"),
				Delimiter = _glyphProvider.Delimiter,
				Height = 3,
				Width = 7
			};

			var panel = Panel(label, border);
			panel.OnFocusChanged += (s, e) =>
			{
				if (e.NewValue)
				{
					border.IsFocused = true;
					e.Handled = true;
				}
			};
			return panel;
		}

		private Control<TGlyph, TColor, TKey> CreateGame()
		{
			_levelControl = new LevelControl<TGlyph, TColor, TKey>(_glyphProvider, _colorProvider)
			{
				Left = 1,
				Top = 1
			};
			_levelControl.OnKeyboardInput += (s, e) =>
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
					else if (KeyboardAdapter.IsEscape(e.Input))
					{
						e.Handled = true;
						Page = LocalMenuPage;
					}
					else if (KeyboardAdapter.IsBackspace(e.Input))
					{
						e.Handled = true;
						_levelControl.Level.Undo();
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
			return Panel(_levelControl, _levelCompleteMessageLabel);
		}

		private Control<TGlyph, TColor, TKey> CreateLocalMenu()
		{
			var menu = Menu();
			var continueItem = menu.AddItem(_glyphProvider.FromString("Continue"));
			var restartItem = menu.AddItem(_glyphProvider.FromString("Restart"));
			var saveGameItem = menu.AddItem(_glyphProvider.FromString("Save game"));
			var loadGameItem = menu.AddItem(_glyphProvider.FromString("Load game"));
			var mainMenuItem = menu.AddItem(_glyphProvider.FromString("Main menu"));
			menu.OnSelect += (s, item) =>
				{
					if (item == continueItem)
						Page = NewGamePage;
					else if (item == restartItem)
						StartLevel(_levelControl.Level.Index);
					else if (item == saveGameItem)
						Page = SaveMenuPage;
					else if (item == loadGameItem)
						Page = LoadMenuPage;
					else if (item == mainMenuItem)
						Page = MainMenuPage;
				};

			var border = Boder(20, 10, "Local menu", menu);

			var label = new Label<TGlyph, TColor, TKey>
			{
				ColorTheme = _colorProvider.SokobanLabelColorTheme,
				Text = _glyphProvider.FromString("SOKOBAN"),
				Delimiter = _glyphProvider.Delimiter,
				Height = 3,
				Width = 7
			};

			var panel = Panel(label, border);
			panel.OnFocusChanged += (s, e) =>
			{
				if (e.NewValue)
				{
					border.IsFocused = true;
					e.Handled = true;
				}
			};
			return panel;
		}

		private Control<TGlyph, TColor, TKey> CreateSaveMenu()
		{
			var menu = Menu();
			var items = new List<object>(10);
			for (var i = 0; i < items.Capacity; i++)
			{
				items.Add(menu.AddItem(_glyphProvider.FromString($"Slot {i}")));
			}
			menu.OnSelect += (m, item) =>
			{
				var index = items.IndexOf(item);
				_levelControl.Level.Save($"save{index}.sav");
				Page = LocalMenuPage;
			};
			menu.OnKeyboardInput += (control, args) =>
			{
				if (KeyboardAdapter.IsEscape(args.Input))
				{
					Page = LocalMenuPage;
				}
			};

			return Panel(Boder(20, 12, "Save menu", menu));
		}

		private Control<TGlyph, TColor, TKey> CreateLoadMenu()
		{
			var menu = Menu();
			var items = new List<object>(10);
			for (var i = 0; i < items.Capacity; i++)
			{
				items.Add(menu.AddItem(_glyphProvider.FromString($"Slot {i}")));
			}
			menu.OnSelect += (m, item) =>
			{
				var index = items.IndexOf(item);
				if (File.Exists($"save{index}.sav"))
				{
					_levelControl.Level = Level.Load($"save{index}.sav");
					Page = NewGamePage;
				}
			};
			menu.OnKeyboardInput += (control, args) =>
			{
				if (KeyboardAdapter.IsEscape(args.Input))
				{
					Page = _previousPage;
				}
			};

			return Panel(Boder(20, 12, "Load menu", menu));
		}

		private Menu<TGlyph, TColor, TKey> Menu()
		{
			return new Menu<TGlyph, TColor, TKey>
			{
				EllipsisGlyph = _glyphProvider.EllipsisGlyph,
				SelectionGlyph = _glyphProvider.SelectionGlyph
			};
		}

		private Border<TGlyph, TColor, TKey> Boder(int width, int height, string title, params Control<TGlyph, TColor, TKey>[] controls)
		{
			var border = new Border<TGlyph, TColor, TKey>
			{
				BorderTheme = _glyphProvider.MainMenuBorderTheme,
				ColorTheme = _colorProvider.MainMenuColorTheme,
				Width = width,
				Height = height,
				Title = _glyphProvider.FromString(title),
				Ellipsis = _glyphProvider.EllipsisGlyph
			};
			foreach (var control in controls)
			{
				border.Controls.Add(control);
			}
			return border;
		}

		private Panel<TGlyph, TColor, TKey> Panel(params Control<TGlyph, TColor, TKey>[] controls)
		{
			var panel = new Panel<TGlyph, TColor, TKey>
			{
				Height = _heightInGlyphs,
				Vertical = true,
				Width = _widthInGlyphs
			};
			foreach (var control in controls)
			{
				panel.Controls.Add(control);
			}
			return panel;
		}

		private void StartLevel(int index)
		{
			_levelControl.Level = _levelProvider.GetLevel(index);
			_levelCompleteMessageLabel.Text = _glyphProvider.FromString("");
			Page = NewGamePage;
		}
	}
}