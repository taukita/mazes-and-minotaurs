using MazesAndMinotaurs.ConsoleTarget;
using MazesAndMinotaurs.ConsoleTarget.Ui;
using MazesAndMinotaurs.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazesAndMinotaurs.ConsoleTarget.Ui.Events;

namespace MazesAndMinotaurs.Test
{
	class Program
	{
		private class GameControl : Control
		{
			private int _playerX = 1;
			private int _playerY = 1;
			private IEnumerable<Tuple<int, int>> _walls;
			private HashSet<Tuple<int, int>> _knownPoints = new HashSet<Tuple<int, int>>();
			private WallsControl _wallsControl;

			public GameControl(IEnumerable<Tuple<int, int>> walls)
			{
				_walls = walls;
				_wallsControl = new WallsControl(_walls);
				_wallsControl.WallsTheme = WallsTheme.Box;
				var position = Tuple.Create(1, 1);

				if (_walls.Any(w => w.Equals(position)))
				{
					throw new ArgumentException(nameof(walls));
				}

				Visit();
			}

			protected override void Drawing(ITerminal<char, ConsoleColor> terminal)
			{
				terminal.FillRectangle(0, 0, 21, 21, ' ', ConsoleColor.White, ConsoleColor.Black);
				_wallsControl.Draw(terminal);
				for (var x = 0; x < 21; x++)
				{
					for (var y = 0; y < 21; y++)
					{
						if (!_knownPoints.Any(p => p.Equals(Tuple.Create(x, y))))
						{
							terminal.Draw(x, y, '░', ConsoleColor.White, ConsoleColor.Black);
						}
					}
				}
				terminal.Draw(_playerX, _playerY, '@', ConsoleColor.Red);
			}

			protected override void KeyPressed(KeyPressedEventArgs args)
			{
				switch (args.Key)
				{
					case ConsoleKey.UpArrow:
						TryChangePlayerPosition(_playerX, _playerY - 1);
						break;
					case ConsoleKey.LeftArrow:
						TryChangePlayerPosition(_playerX - 1, _playerY);
						break;
					case ConsoleKey.DownArrow:
						TryChangePlayerPosition(_playerX, _playerY + 1);
						break;
					case ConsoleKey.RightArrow:
						TryChangePlayerPosition(_playerX + 1, _playerY);
						break;
				}
			}

			private char GetGlyph(int x, int y)
			{
				bool up = _walls.Any(w => w.Equals(Tuple.Create(x, y - 1)));
				bool left = _walls.Any(w => w.Equals(Tuple.Create(x - 1, y)));
				bool down = _walls.Any(w => w.Equals(Tuple.Create(x, y + 1)));
				bool right = _walls.Any(w => w.Equals(Tuple.Create(x + 1, y)));
				return WallsTheme.Box[up, left, down, right];
			}

			private void TryChangePlayerPosition(int x, int y)
			{
				var position = Tuple.Create(x, y);
				if (!_walls.Any(w => w.Equals(position)))
				{
					_playerX = x;
					_playerY = y;
					Visit();
				}
			}

			private void Visit()
			{
				for (var x = _playerX - 1; x <= _playerX + 1; x++)
				{
					for (var y = _playerY - 1; y <= _playerY + 1; y++)
					{
						_knownPoints.Add(Tuple.Create(x, y));
					}
				}
			}
		}

		static void Main(string[] args)
		{
			Console.CursorVisible = false;

			var viewer = new ItemsViewerControl();
			viewer.Width = Console.WindowWidth;
			viewer.Height = Console.WindowHeight;

			viewer.AddItem("Сектоид", @"Маленький, трусливый, абсолютно голый.
Подвергается постоянным унижениям мутонов. Не может постоять за себя, поэтому отправляется на самые зашкварные миссии.
Псиотнические способности использует для того, чтоб узнать у начальства пароль к вай-фаю.");
			viewer.AddItem("Флоатер", @"Житель степей родной планеты Эчпочман. Из-за своей тупости долгое время считался животным и не подлежал призыву.
В армии мутоны ради смеха засунули ему в задницу двигатель от мобильного пылесоса. С тех пор флоатеры научились летать и полюбили самые дальние углы верхних этажей различных зданий.");

			var app = new App(viewer);

			app.Run();
		}

		static void Main1(string[] args)
		{
			Console.CursorVisible = false;

			var mainMenu = new MenuControl(new List<string> { "Играть", "Тестировать", "Сетка", "Выйти" });

			var border = new BorderControl(mainMenu)
				{
					Left = 1,
					Top = 1,
					Width = 50,
					Height = 10,
					BorderTheme = BorderTheme.Box
				};

			var pages = new PagesContainerControl();

			mainMenu.OnSelect += (m, i) =>
				{
					switch (i)
					{
						case "Играть":
							pages.Page = 1;
							break;
						case "Выйти":
							pages.IsFocused = false;
							break;
						case "Тестировать":
							pages.Page = 2;
							break;
						case "Сетка":
							pages.Page = 3;
							break;
					}
				};

			var game = new GameControl(Generators.RacketMazeGenerator.Generate(10, 10));
			game.OnKeyPressed += (g, a) =>
				{
					if (a.Key == ConsoleKey.Escape)
					{
						pages.Page = 0;
					}
				};

			var canvas = CreateTestCanvas();
			canvas.OnKeyPressed += (g, a) =>
				{
					if (a.Key == ConsoleKey.Escape)
					{
						pages.Page = 0;
					}
				};

			var grid = CreateTestGrid();
			grid.OnKeyPressed += (g, a) =>
				{
					if (a.Key == ConsoleKey.Escape)
					{
						pages.Page = 0;
					}
				};

			pages.Controls.Add(border);
			pages.Controls.Add(game);
			pages.Controls.Add(canvas);
			pages.Controls.Add(grid);

			var app = new App(pages);

			app.Run();
		}

		private static Control CreateTestCanvas()
		{
			var canvas = new CanvasContainerControl();			

			foreach(var control in TestControls())
			{
				canvas.Controls.Add(control);
			}

			return canvas;
		}

		private static Control CreateTestGrid()
		{
			var grid = new GridContainerControl();
			grid.Width = 30;
			grid.Height = 20;

			grid.Columns.Add(10);
			grid.Columns.Add(10);
			grid.Columns.Add(10);

			grid.Rows.Add(10);
			grid.Rows.Add(10);

			foreach (var control in TestControls())
			{
				grid.Controls.Add(control);
			}

			return grid;
		}

		private static IEnumerable<Control> TestControls()
		{
			var colorTheme = ColorTheme.Create(ConsoleColor.Black, ConsoleColor.White, ConsoleColor.White, ConsoleColor.Black);

			var menu1 = new BorderControl(new MenuControl(new List<string> { "item 1", "item 2" }))
			{
				Left = 25,
				Top = 1,
				Width = 10,
				Height = 10,
				BorderTheme = BorderTheme.Box,
				ColorTheme = colorTheme
			};

			yield return menu1;

			var menu2 = new BorderControl(new MenuControl(new List<string> { "item 1", "item 2", "item 3" }))
			{
				Left = 36,
				Top = 1,
				Width = 10,
				Height = 10,
				BorderTheme = BorderTheme.Box,
				ColorTheme = colorTheme
			};

			yield return menu2;

			yield return new BorderControl { Left = 1, Top = 1, Width = 3, Height = 3, ColorTheme = colorTheme };
			yield return new BorderControl { Left = 1, Top = 5, Width = 3, Height = 3, ColorTheme = colorTheme };
			yield return new BorderControl { Left = 5, Top = 5, Width = 3, Height = 3, ColorTheme = colorTheme };
			yield return new BorderControl { Left = 5, Top = 1, Width = 3, Height = 3, ColorTheme = colorTheme };
		}
	}
}

