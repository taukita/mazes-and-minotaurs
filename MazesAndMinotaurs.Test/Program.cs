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
		private class Terminal : ITerminal<char, ConsoleColor>
		{
			public ConsoleColor Background { get; set; }

			public ConsoleColor Foreground { get; set; }

			public int OffsetX { get; set; }

			public int OffsetY { get; set; }

			public void Draw(int x, int y, char glyph)
			{
				Draw(x, y, glyph, Foreground, Background);
			}

			public void Draw(int x, int y, char glyph, ConsoleColor foreground)
			{
				Draw(x, y, glyph, foreground, Background);
			}

			public void Draw(int x, int y, char glyph, ConsoleColor foreground, ConsoleColor background)
			{
				Console.SetCursorPosition(OffsetX + x, OffsetY + y);
				Console.BackgroundColor = background;
				Console.ForegroundColor = foreground;
				Console.Write(glyph);
			}
		}

		private class Game
		{
			private int _playerX = 1;
			private int _playerY = 1;
			private IEnumerable<Tuple<int, int>> _walls;
			private ITerminal<char, ConsoleColor> _terminal;

			public Game(IEnumerable<Tuple<int, int>> walls, ITerminal<char, ConsoleColor> terminal)
			{
				_walls = walls;
				_terminal = terminal;
				var position = Tuple.Create(1, 1);
				if (_walls.Any(w => w.Equals(position)))
				{
					throw new ArgumentException(nameof(walls));
				}
				Draw();
			}

			public void NotifyKeyPressed(ConsoleKey key)
			{
				switch(key)
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
				_terminal.Draw(_playerX, _playerY, '@', ConsoleColor.Red);
				_terminal.DrawWalls(_walls, '#', ConsoleColor.Black, ConsoleColor.White);
			}

			private void TryChangePlayerPosition(int x, int y)
			{
				var position = Tuple.Create(x, y);
				if (!_walls.Any(w => w.Equals(position)))
				{
					_terminal.Draw(_playerX, _playerY, ' ');
					_playerX = x;
					_playerY = y;
					Draw();
				}
			}
		}

		static void Main(string[] args)
		{
			Console.CursorVisible = false;

			var t = new Terminal { OffsetX = 1, OffsetY = 1 };
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
