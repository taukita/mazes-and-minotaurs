using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazesAndMinotaurs.Core;

namespace MazesAndMinotaurs.ConsoleTarget.Ui.Tests
{
	internal class DummyTerminal : ITerminal<char, ConsoleColor>
	{
		public static ITerminal<char, ConsoleColor> Instance = new DummyTerminal();

		private DummyTerminal()
		{
		}

		public ConsoleColor Background { get; set; }
		public ConsoleColor Foreground { get; set; }

		public void Clear(int x, int y)
		{
		}

		public void Draw(int x, int y, char glyph)
		{
		}

		public void Draw(int x, int y, char glyph, ConsoleColor foreground)
		{
		}

		public void Draw(int x, int y, char glyph, ConsoleColor foreground, ConsoleColor background)
		{
		}
	}
}
