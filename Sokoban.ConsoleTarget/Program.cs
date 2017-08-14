using System;
using MazesAndMinotaurs.ConsoleTarget;
using MazesAndMinotaurs.Core;
using Sokoban.Core;

namespace Sokoban.ConsoleTarget
{
	internal class Program
	{
		// ReSharper disable once UnusedParameter.Local
		private static void Main(string[] args)
		{
			Console.CursorVisible = false;
			var root = new GameControl<char, ConsoleColor, ConsoleKey>(new ConsoleGlyphProvider(), new ConsoleColorProvider(),
				Console.WindowHeight, Console.WindowWidth);
			var terminal = new BufferTerminal<char, ConsoleColor>(new ConsoleTerminal());
			root.KeyboardAdapter = new KeyboardAdapter();
			root.IsFocused = true;

			while (root.IsFocused)
			{
				root.Draw(terminal);
				terminal.Flush();
				Console.SetCursorPosition(0, 0);
				root.NotifyKeyboardInput(Console.ReadKey(true).Key);
			}
		}
	}
}