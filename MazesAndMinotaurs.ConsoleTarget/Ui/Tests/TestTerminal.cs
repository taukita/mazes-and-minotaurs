using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazesAndMinotaurs.Core;

namespace MazesAndMinotaurs.ConsoleTarget.Ui.Tests
{
	internal class TestTerminal : ITerminal<char, ConsoleColor>
	{
		private readonly int _width;
		private readonly int _height;
		private readonly char[,] _chars;
		private readonly char _empty;

		public TestTerminal(int width, int height, char empty = ' ')
		{
			_width = width;
			_height = height;
			_chars = new char[width, height];
			_empty = empty;
			for (var i = 0; i < width; i++)
			{
				for (var j = 0; j < height; j++)
				{
					_chars[i, j] = empty;
				}
			}
		}

		public ConsoleColor Background { get; set; }
		public ConsoleColor Foreground { get; set; }

		public void Clear(int x, int y)
		{
			_chars[x, y] = _empty;
		}

		public void Draw(int x, int y, char glyph)
		{
			_chars[x, y] = glyph;
		}

		public void Draw(int x, int y, char glyph, ConsoleColor foreground)
		{
			_chars[x, y] = glyph;
		}

		public void Draw(int x, int y, char glyph, ConsoleColor foreground, ConsoleColor background)
		{
			_chars[x, y] = glyph;
		}

		public override string ToString()
		{
			var sb = new StringBuilder((_width + 2)*_height);
			for (var y = 0; y < _height; y++)
			{
				for (var x = 0; x < _width; x++)
				{
					sb.Append(_chars[x, y]);
				}
				if (y != _height - 1)
				{
					sb.Append(Environment.NewLine);
				}
			}
			return sb.ToString();
		}
	}
}
