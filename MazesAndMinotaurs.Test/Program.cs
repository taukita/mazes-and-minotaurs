using MazesAndMinotaurs.ConsoleTarget;
using MazesAndMinotaurs.ConsoleTarget.Ui;
using MazesAndMinotaurs.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

			public GameControl(IEnumerable<Tuple<int, int>> walls)
			{
				_walls = walls;
				var position = Tuple.Create(1, 1);

				if (_walls.Any(w => w.Equals(position)))
				{
					throw new ArgumentException(nameof(walls));
				}

				Visit();
			}

			protected override void Drawing(ITerminal<char, ConsoleColor> terminal)
			{
				terminal.FillRectangle(0, 0, 21, 21, '░', ConsoleColor.White, ConsoleColor.Black);
				foreach (var point in _knownPoints)
				{
					if (_walls.Any(w => w.Equals(point)))
					{
						terminal.Draw(point.Item1, point.Item2, '#', ConsoleColor.Black, ConsoleColor.White);
					}
					else
					{
						terminal.Draw(point.Item1, point.Item2, ' ');
					}
				}
				terminal.Draw(_playerX, _playerY, '@', ConsoleColor.Red);
			}

			public override KeyPressedResult NotifyKeyPressed(ConsoleKey key)
			{
				switch (key)
				{
					case ConsoleKey.UpArrow:
						TryChangePlayerPosition(_playerX, _playerY - 1);
						return KeyPressedResult.DoNothing;
					case ConsoleKey.LeftArrow:
						TryChangePlayerPosition(_playerX - 1, _playerY);
						return KeyPressedResult.DoNothing;
					case ConsoleKey.DownArrow:
						TryChangePlayerPosition(_playerX, _playerY + 1);
						return KeyPressedResult.DoNothing;
					case ConsoleKey.RightArrow:
						TryChangePlayerPosition(_playerX + 1, _playerY);
						return KeyPressedResult.DoNothing;
					case ConsoleKey.Escape:
						return KeyPressedResult.Prev;
					default:
						return base.NotifyKeyPressed(key);
				}
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

			var mainMenu = new MenuControl(new List<string> {"Играть", "Тестировать", "Выйти"})
				{
					Left = 1,
					Top = 1,
					Width = 50,
					Height = 10
				};

			var pages = new PagesContainerControl();

			mainMenu.OnSelect += (m, i) =>
				{
					switch (i)
					{
						case "Играть":
							return KeyPressedResult.Next;
						case "Выйти":
							return KeyPressedResult.Unfocus;
						case "Тестировать":
							pages.Page = 2;
							return KeyPressedResult.DoNothing;
						default:
							return KeyPressedResult.DoNothing;
					}
				};
			
			pages.Controls.Add(mainMenu);
			pages.Controls.Add(new GameControl(Generators.RacketMazeGenerator.Generate(10, 10)));
			pages.Controls.Add(CreateTestCanvas());

			var app = new App(pages);

			app.Run();
		}

		private static Control CreateTestCanvas()
		{
			var menu1 = new MenuControl(new List<string> {"item 1", "item 2"})
				{
					Left = 25,
					Top = 1,
					Width = 10,
					Height = 10
				};

			var menu2 = new MenuControl(new List<string> {"item 1", "item 2", "item 3"})
				{
					Left = 36,
					Top = 1,
					Width = 10,
					Height = 10
				};

			var canvas = new CanvasContainerControl();
			canvas.Controls.Add(new BorderControl {Left = 1, Top = 1, Width = 3, Height = 3});
			canvas.Controls.Add(new BorderControl {Left = 1, Top = 5, Width = 3, Height = 3});
			canvas.Controls.Add(new BorderControl {Left = 5, Top = 5, Width = 3, Height = 3});
			canvas.Controls.Add(new BorderControl {Left = 5, Top = 1, Width = 3, Height = 3});
			canvas.Controls.Add(menu1);
			canvas.Controls.Add(menu2);

			return canvas;
		}
	}
}

