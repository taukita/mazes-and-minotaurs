using System;
using System.Text;
using MazesAndMinotaurs.Core;

namespace Sokoban.Core.Tests
{
	internal class TestTerminal : ITerminal<char, object>
	{
		private readonly char[,] _chars;
		private readonly char _empty;
		private readonly int _height;
		private readonly int _width;

		public TestTerminal(int width, int height, char empty = ' ')
		{
			_width = width;
			_height = height;
			_chars = new char[width, height];
			_empty = empty;
			for (var i = 0; i < width; i++)
			for (var j = 0; j < height; j++)
				_chars[i, j] = empty;
		}

		public object Background { get; set; }
		public object Foreground { get; set; }

		public void Clear(int x, int y)
		{
			_chars[x, y] = _empty;
		}

		public void Draw(int x, int y, char glyph)
		{
			_chars[x, y] = glyph;
		}

		public void Draw(int x, int y, char glyph, object foreground)
		{
			_chars[x, y] = glyph;
		}

		public void Draw(int x, int y, char glyph, object foreground, object background)
		{
			_chars[x, y] = glyph;
		}

		public override string ToString()
		{
			var sb = new StringBuilder((_width + 2) * _height);
			for (var y = 0; y < _height; y++)
			{
				for (var x = 0; x < _width; x++)
					sb.Append(_chars[x, y]);
				if (y != _height - 1)
					sb.Append(Environment.NewLine);
			}
			return sb.ToString();
		}
	}
}