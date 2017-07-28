using MazesAndMinotaurs.ConsoleTarget;
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
		private class Game
		{
			private int _playerX = 1;
			private int _playerY = 1;
			private IEnumerable<Tuple<int, int>> _walls;
			private BufferTerminal<char, ConsoleColor> _terminal;
			private HashSet<Tuple<int, int>> _knownPoints = new HashSet<Tuple<int, int>>();

			private Menu<string> _menu;

			public Game(IEnumerable<Tuple<int, int>> walls, ITerminal<char, ConsoleColor> terminal)
			{
				_walls = walls;
				_terminal = new BufferTerminal<char, ConsoleColor>(terminal);
				var position = Tuple.Create(1, 1);

				if (_walls.Any(w => w.Equals(position)))
				{
					throw new ArgumentException(nameof(walls));
				}

				_menu = new Menu<string>(new[] {"item 1", "item 2", "item 3"}, _terminal)
					{
						Left = 30,
						Top = 0,
						Width = 12
					};

				Visit();
				Draw();
			}

			public void NotifyKeyPressed(ConsoleKey key)
			{
				switch (key)
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

			private void Draw()
			{
				_terminal.FillRectangle(0, 0, 21, 21, '░', ConsoleColor.White, ConsoleColor.Black);
				foreach (var point in _knownPoints)
				{
					if (_walls.Any(w => w.Equals(point)))
					{
						_terminal.Draw(point.Item1, point.Item2, '#', ConsoleColor.Black, ConsoleColor.White);
					}
					else
					{
						_terminal.Draw(point.Item1, point.Item2, ' ');
					}
				}
				_terminal.Draw(_playerX, _playerY, '@', ConsoleColor.Red);

				_menu.Draw();

				_terminal.Flush();
			}

			private void TryChangePlayerPosition(int x, int y)
			{
				var position = Tuple.Create(x, y);
				if (!_walls.Any(w => w.Equals(position)))
				{
					_terminal.Draw(_playerX, _playerY, ' ');
					_playerX = x;
					_playerY = y;
					Visit();
					Draw();
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

			var t = new ConsoleTerminal { OffsetX = 1, OffsetY = 1 };
			var walls = Generators.RacketMazeGenerator.Generate(10, 10);
			var game = new Game(walls, t);
			ConsoleKey k;
			while ((k = Console.ReadKey(true).Key) != ConsoleKey.Escape)
			{
				game.NotifyKeyPressed(k);
			}
		}
	}
}

